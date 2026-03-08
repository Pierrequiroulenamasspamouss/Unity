#!/usr/bin/env python3
"""
convert_definitions.py
======================
Merges definitions.json.bak (full decompiled data) with definitions.json (current server),
normalizing field names so the game client (Definition.Deserialize) can read them correctly.

Output: definitions.json.final

HOW THE CLIENT READS FIELDS:
  - Definition.Deserialize() converts all property names to UPPERCASE internally.
  - So the JSON field name case doesn't technically matter for the base `ID` field.
  - BUT BuildingDefinitionFastConverter looks for 'type', 'TYPE', 'BuildingType', 'Type'
    by exact string match -> we need to keep the field named 'type' (lowercase).
  - AnimationDefinitions are stored as 'animationDefinitions' (lowercase) in the current
    definitions.json and that's what the client reads.

STRATEGY:
  1. Start from the BAK file (has all definitions, including missing ones).
  2. For each section, normalize mixed-case field names:
     - ANIMATIONDEFINITIONS -> animationDefinitions
     - All other fields: keep as-is (Definition.Deserialize uppercases before comparing)
  3. For any section present only in the current definitions.json, include it as-is.
  4. Write the merged result to definitions.json.final.

Usage:
  python convert_definitions.py
  (Run from C:/Unity/SERVER directory, or adjust paths below)
"""

import json
import os
import re

BAK_PATH   = r"C:\Unity\SERVER\definitions.json.bak"
CUR_PATH   = r"C:\Unity\SERVER\definitions.json"
OUT_PATH   = r"C:\Unity\SERVER\definitions.json.final"

# ---------------------------------------------------------------------------
# Field normalization map: rename specific problematic uppercase keys to the
# lowercase equivalents the C# converters look for by exact match.
# The base Definition.Deserialize() is fine with any case (it uppercases),
# but the Fast Converters do exact-match string comparisons.
# ---------------------------------------------------------------------------
RENAME_MAP = {
    "ANIMATIONDEFINITIONS": "animationDefinitions",
    "ANIMATIONDEFINITIONID": "animationDefinitionId",
    "ASPIRATIONALMESSAGE": "aspirationalMessage",
    "ASPIRATIONALMESSAGELEVELREACHED": "aspirationalMessageLevelReached",
    "BUILDINGDEFINITIONID": "buildingDefinitionId",
    "LOCATION": "location",
}

def normalize_keys(obj):
    """
    Recursively walk an object and rename any keys found in RENAME_MAP.
    All other keys are left exactly as-is (the C# reader uppercases them).
    """
    if isinstance(obj, dict):
        result = {}
        for k, v in obj.items():
            new_key = RENAME_MAP.get(k, k)
            result[new_key] = normalize_keys(v)
        return result
    elif isinstance(obj, list):
        return [normalize_keys(item) for item in obj]
    else:
        return obj


def merge_list_by_id(bak_list, cur_list, id_key=None):
    """
    Merge two lists of definition objects. The BAK entry takes priority (more data),
    but we fall back to cur for any IDs only present there.
    id_key: the field name to use as unique identifier. If None, tries 'id', 'ID', 'Id'.
    """
    def get_id(item):
        if id_key:
            return item.get(id_key)
        for k in ['id', 'ID', 'Id']:
            v = item.get(k)
            if v is not None:
                return v
        return None

    # Build lookup from BAK
    bak_by_id = {}
    bak_ordered = []
    for item in (bak_list or []):
        if not isinstance(item, dict):
            bak_ordered.append(item)
            continue
        iid = get_id(item)
        normalized = normalize_keys(item)
        if iid is not None:
            bak_by_id[iid] = normalized
        bak_ordered.append(normalized)

    # Add any cur entries whose ID is NOT already in BAK
    cur_by_id = {}
    for item in (cur_list or []):
        if not isinstance(item, dict):
            continue
        iid = get_id(item)
        if iid is not None:
            cur_by_id[iid] = item

    for iid, item in cur_by_id.items():
        if iid not in bak_by_id:
            print(f"  [MERGE] Adding cur-only entry id={iid}")
            bak_ordered.append(normalize_keys(item))

    return bak_ordered


def merge_definitions(bak, cur):
    """
    Merge the two top-level definition dicts.
    For list sections: merge by ID (BAK takes priority).
    For non-list sections: BAK takes priority; falls back to cur.
    """
    result = {}

    # Union of all top-level keys
    all_keys = set(bak.keys()) | set(cur.keys())

    for key in sorted(all_keys):
        bak_val = bak.get(key)
        cur_val = cur.get(key)

        if bak_val is None and cur_val is not None:
            # Only in current — keep as-is
            print(f"[SECTION] '{key}': only in current definitions.json, keeping as-is")
            result[key] = normalize_keys(cur_val)

        elif isinstance(bak_val, list):
            # List section: merge by ID
            cur_list = cur_val if isinstance(cur_val, list) else []
            merged = merge_list_by_id(bak_val, cur_list)
            result[key] = merged
            print(f"[SECTION] '{key}': {len(bak_val)} bak + {len(cur_list)} cur -> {len(merged)} merged")

        elif isinstance(bak_val, dict):
            # Single-object section: BAK takes priority
            result[key] = normalize_keys(bak_val)
            print(f"[SECTION] '{key}': dict object, using BAK")

        else:
            # Primitive or unknown: BAK takes priority
            result[key] = bak_val
            print(f"[SECTION] '{key}': primitive, using BAK value")

    return result


def validate_buildings(result):
    """
    Quick sanity check: make sure all buildings have a 'type' field (lowercase)
    so BuildingDefinitionFastConverter can read them.
    """
    buildings = result.get('buildingDefinitions', [])
    missing_type = []
    for b in buildings:
        if not isinstance(b, dict):
            continue
        has_type = any(k in b for k in ('type', 'TYPE', 'BuildingType', 'Type'))
        if not has_type:
            bid = b.get('id') or b.get('ID') or '?'
            missing_type.append(bid)

    if missing_type:
        print(f"\n[WARNING] {len(missing_type)} buildings have no 'type' field: {missing_type[:20]}")
        print("  These will use the fallback Decoration type in BuildingDefinitionFastConverter.")
    else:
        print(f"\n[OK] All {len(buildings)} buildings have a 'type' field.")

    return missing_type


def main():
    print("=== definitions.json Converter ===\n")

    # Load input files
    print(f"Loading BAK:  {BAK_PATH}")
    with open(BAK_PATH, 'r', encoding='utf-8') as f:
        bak = json.load(f)
    print(f"  Keys: {len(bak)}, BuildingDefs: {len(bak.get('buildingDefinitions', []))}")

    print(f"Loading CUR:  {CUR_PATH}")
    with open(CUR_PATH, 'r', encoding='utf-8') as f:
        cur = json.load(f)
    print(f"  Keys: {len(cur)}, BuildingDefs: {len(cur.get('buildingDefinitions', []))}\n")

    # Merge
    print("--- Merging sections ---")
    result = merge_definitions(bak, cur)

    # Validate
    validate_buildings(result)

    # Write output
    print(f"\nWriting: {OUT_PATH}")
    with open(OUT_PATH, 'w', encoding='utf-8') as f:
        json.dump(result, f, ensure_ascii=False, separators=(',', ':'))

    size_kb = os.path.getsize(OUT_PATH) // 1024
    print(f"Done! Output size: {size_kb} KB")
    print(f"\nNext step: copy {OUT_PATH} over {CUR_PATH} to deploy.")
    print("  copy /Y definitions.json.final definitions.json")


if __name__ == '__main__':
    main()
