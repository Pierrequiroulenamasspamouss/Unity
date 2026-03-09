import os

# Set your project path here
PROJECT_ROOT = r'C:\Unity\FRANKENSTEIN-UNITYPROJECT\Assets'
RESOURCES_DIR = os.path.join(PROJECT_ROOT, 'Resources')

def cleanup_duplicates():
    if not os.path.exists(RESOURCES_DIR):
        print(f"Error: Resources folder not found at {RESOURCES_DIR}")
        return

    # 1. Map out everything inside the Resources folder
    resources_files = {}
    for root, dirs, files in os.walk(RESOURCES_DIR):
        for file in files:
            # We skip .meta files to focus on the actual assets
            if file.endswith('.meta'):
                continue
            resources_files[file] = os.path.join(root, file)

    print(f"Found {len(resources_files)} unique assets in Resources.\n")

    # 2. Scan the rest of the Assets folder for matches
    deleted_count = 0
    for root, dirs, files in os.walk(PROJECT_ROOT):
        # SKIP the Resources folder during this scan so we don't delete our "keepers"
        if "Resources" in root:
            continue

        for file in files:
            if file in resources_files:
                target_path = os.path.join(root, file)
                meta_path = target_path + ".meta"

                print(f"Duplicate found: {file}")
                print(f"  > Keeping: {resources_files[file]}")
                print(f"  > Deleting: {target_path}")
                
                try:
                    os.remove(target_path)
                    if os.path.exists(meta_path):
                        os.remove(meta_path)
                    deleted_count += 1
                except Exception as e:
                    print(f"  !! Failed to delete: {e}")

    print(f"\nCleanup finished. Removed {deleted_count} duplicate files from the root Assets folder.")

if __name__ == "__main__":
    cleanup_duplicates()