import json

file_path = r'C:\Unity\SERVER\definitions.json'
with open(file_path, 'r', encoding='utf-8') as f:
    data = json.load(f)

# Ensure buildingDefinitions exists
if 'buildingDefinitions' not in data:
    data['buildingDefinitions'] = []

# Check if OrderBoard (3022) is already in buildingDefinitions
if not any(b.get('ID') == 3022 for b in data['buildingDefinitions']):
    order_board_def = {
        "ID": 3022,
        "LocalizedKey": "BlackMarketBoard",
        "BuildingType": "BLACKMARKETBOARD",
        "Type": 16, # BLACKMARKETBOARD enum
        "MaxLevel": 1,
        "LevelPrereq": 1,
        "XDim": 3,
        "YDim": 3,
        "XOffset": 0,
        "YOffset": 0,
        "ConstructionTime": 0,
        "IsStorable": False,
        "IsSellable": False,
        "Levels": [{"Level": 1, "Cost": []}],
        "AssetBundle": "blackmarketboard"
    }
    data['buildingDefinitions'].append(order_board_def)
    print("Injected OrderBoard definition 3022.")
else:
    print("OrderBoard already exists in buildingDefinitions.")

with open(file_path, 'w', encoding='utf-8') as f:
    json.dump(data, f)
