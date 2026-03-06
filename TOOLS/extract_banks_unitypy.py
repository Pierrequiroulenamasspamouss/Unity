import json
import os
import shutil
import re
import UnityPy
from UnityPy.helpers.UnityVersion import UnityVersion

if not hasattr(UnityVersion, "from_str_orig"):
    UnityVersion.from_str_orig = UnityVersion.from_str

def custom_from_str(version: str):
    clean = re.sub(r'[a-zA-Z].*', '', version)
    try:
        return UnityVersion.from_str_orig(clean)
    except:
        return UnityVersion.from_str_orig("4.6.6")

UnityVersion.from_str = custom_from_str

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
            try:
                env = UnityPy.load(source)
                found = False
                for obj in env.objects:
                    if obj.type.name == "TextAsset":
                        text_data = obj.read()
                        
                        script_bytes = getattr(text_data, 'script', getattr(text_data, 'm_Script', None))
                        if script_bytes is None:
                            if hasattr(text_data, 'text'):
                                script_bytes = text_data.text
                        
                        if isinstance(script_bytes, str):
                            script_bytes = script_bytes.encode('utf-8', 'surrogateescape')
                        
                        if script_bytes and len(script_bytes) > 20000:
                            with open(dest, "wb") as out_f:
                                out_f.write(script_bytes)
                            print(f"Extracted {orig} (Size: {len(script_bytes)})")
                            found = True
                            count += 1
                            break
            except Exception as e:
                print(f"Failed to load {orig}: {e}")

print(f"Done! Extracted {count} actual banks to {out_dir}")
