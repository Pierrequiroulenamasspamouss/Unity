import json
import os
import shutil
import re

manifest_path = r"c:\Unity\DUMP\com.ea.gp.minions\files\DLC_Manifest.json"
dlc_dir = r"c:\Unity\DUMP\com.ea.gp.minions\files\dlc"
out_dir = r"c:\Unity\FRANKENSTEIN-UNITYPROJECT\Assets\StreamingAssets"

os.makedirs(out_dir, exist_ok=True)

with open(manifest_path, 'r', encoding='utf8') as f:
    data = json.load(f)

count = 0
for bundle in data.get('bundles', []):
    if bundle.get('audio'):
        name = bundle['name']
        orig = bundle['originalName']
        source = os.path.join(dlc_dir, name + ".unity3d")
        
        orig = re.sub(r'\.android\..*|\.unity3d', '', orig)
        orig = re.sub(r'^Raw_Audio_', '', orig)
        if not orig.endswith('.bank'):
            orig += '.bank'
            
        dest = os.path.join(out_dir, orig)
        
        if os.path.exists(source):
            shutil.copy2(source, dest)
            print(f"Copied {name}.unity3d -> {orig}")
            count += 1
        else:
            print(f"MISSING: {name}.unity3d -> {orig}")

print(f"Done! Copied {count} files.")
