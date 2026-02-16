import json
import os

def process():
    # Le fichier à corriger (celui dans ton projet Unity ou AppData)
    input_path = r"C:\Users\Pierr\AppData\LocalLow\Electronic Arts\Minions\definitions.json"
    
    if not os.path.exists(input_path):
        print(f"Erreur : Impossible de trouver {input_path}")
        return

    print("Lecture du fichier...")
    with open(input_path, 'r', encoding='utf-8') as f:
        data = json.load(f)

    # Correction des doublons dans rushDefinitions
    if "rushDefinitions" in data:
        print("Renumérotation des rushDefinitions (+90000)...")
        for item in data["rushDefinitions"]:
            # On gère "ID" (majuscule) ou "id" (minuscule) au cas où
            if "ID" in item:
                old_id = item["ID"]
                item["ID"] = old_id + 90000
                print(f"Rush ID {old_id} -> {item['ID']}")
            elif "id" in item:
                old_id = item["id"]
                item["id"] = old_id + 90000
                print(f"Rush ID {old_id} -> {item['id']}")
    
    # Sauvegarde
    print("Sauvegarde...")
    with open(input_path, 'w', encoding='utf-8') as f:
        json.dump(data, f, indent=4)
    
    print("Terminé ! Relance le jeu.")

if __name__ == "__main__":
    process()