import os
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

def extract_textasset(bundle_path, out_path):
    env = UnityPy.load(bundle_path)
    found = False
    for obj in env.objects:
        if obj.type.name == "TextAsset":
            data = obj.read()
            script_bytes = getattr(data, 'm_Script', getattr(data, 'script', None))
            if script_bytes is None:
                script_bytes = data.text.encode('utf-8') if hasattr(data, 'text') else b''
            
            with open(out_path, "wb") as f:
                f.write(script_bytes if isinstance(script_bytes, bytes) else script_bytes.encode('utf-8'))
            print(f"Extracted {data.name} to {os.path.basename(out_path)}")
            found = True
            break
    if not found:
        print(f"No TextAsset found in {os.path.basename(bundle_path)}")

bundle = r"c:\Unity\DUMP\com.ea.gp.minions\files\dlc\507b027d-17ee-463e-8bd4-65d4218a24ad.unity3d"
out = r"c:\Unity\FRANKENSTEIN-UNITYPROJECT\Assets\StreamingAssets\Audio_Music.bank"
extract_textasset(bundle, out)
