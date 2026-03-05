import os
import UnityPy
import re
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

dump_dir = r"C:\Users\Pierr\Documents\Projects\Decompiles\Minions_Paradise"
out_dir = r"c:\Unity\FRANKENSTEIN-UNITYPROJECT\Assets\StreamingAssets"
os.makedirs(out_dir, exist_ok=True)

print(f"Scanning for banks recursively in {dump_dir}...")

found_banks = set()
count = 0

for root, dirs, files in os.walk(dump_dir):
    for f in files:
        if f.endswith('.assets') or f.endswith('.unity3d') or f.endswith('.bytes'):
            path = os.path.join(root, f)
            try:
                env = UnityPy.load(path)
                for obj in env.objects:
                    if obj.type.name == "TextAsset":
                        data = obj.read()
                        
                        script_bytes = getattr(data, 'script', getattr(data, 'm_Script', None))
                        if script_bytes is None:
                            if hasattr(data, 'text'):
                                script_bytes = data.text
                        
                        if isinstance(script_bytes, str):
                            script_bytes = script_bytes.encode('utf-8', 'surrogateescape')
                        
                        # Large text asset = FMOD bank
                        if script_bytes and len(script_bytes) > 20000:
                            bank_name = getattr(data, 'm_Name', getattr(data, 'name', 'Unknown'))
                            if not bank_name.endswith('.bank'):
                                bank_name += '.bank'
                            
                            # Deduplicate output across multiple dumps
                            if bank_name in found_banks:
                                continue
                            found_banks.add(bank_name)
                            
                            dest = os.path.join(out_dir, bank_name)
                            with open(dest, "wb") as out_f:
                                out_f.write(script_bytes)
                            print(f"Extracted {bank_name} from {f} (Size: {len(script_bytes)})")
                            count += 1
            except Exception as e:
                pass  # Skip broken/unreadable bundles

print(f"Done! Extracted {count} banks to {out_dir}")
