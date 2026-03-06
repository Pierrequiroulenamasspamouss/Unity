import os
import uuid

folder = r"c:\Unity\FRANKENSTEIN-UNITYPROJECT\Assets\FMOD STUDIO"

meta_template = """fileFormatVersion: 2
guid: {guid}
timeCreated: 1475510665
DefaultImporter:
  userData: 
  assetBundleName: 
  assetBundleVariant: 
"""

count = 0
for f in os.listdir(folder):
    if f.endswith('.bank') and not f.endswith('.strings.bank'):
        meta_path = os.path.join(folder, f + '.meta')
        if not os.path.exists(meta_path):
            with open(meta_path, 'w') as mf:
                mf.write(meta_template.format(guid=uuid.uuid4().hex))
            count += 1
    elif f.endswith('.strings.bank'):
        meta_path = os.path.join(folder, f + '.meta')
        if not os.path.exists(meta_path):
            with open(meta_path, 'w') as mf:
                mf.write(meta_template.format(guid=uuid.uuid4().hex))
            count += 1

print(f"Generated {count} .meta files for banks.")
