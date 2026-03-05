import os

folder = r'c:\Unity\FRANKENSTEIN-UNITYPROJECT\Assets\FMOD STUDIO'

removed = 0
for f in os.listdir(folder):
    if f.endswith('.bank') and f not in [
        'Audio_Main.bank', 'Audio_Main.strings.bank', 'Audio_MinionState.bank', 'Audio_Music.bank'
    ]:
        path = os.path.join(folder, f)
        with open(path, 'rb') as file:
            header = file.read(4)
        
        # Binary FMOD or Sound banks do not start with '{' or '[' or '--' or 'loca'
        try:
            text = header.decode('utf-8')
            if text[0] in "{[-_" or "loca" in text or "def" in text:
                os.remove(path)
                print(f"Removed non-audio file: {f}")
                removed += 1
            else:
                pass
        except:
            pass

print(f"Cleaned up {removed} dummy banks.")
