import time

def generate_new_player_profile(user_id_str):
    numeric_id = 1001
    try:
        numeric_id = int(str(user_id_str))
    except ValueError:
        try:
            numeric_id = int(str(user_id_str), 16)
        except ValueError:
            numeric_id = 1001

    if numeric_id > 9000000000000000000:
        numeric_id = numeric_id % 9000000000000000000
    
    if numeric_id == 0: numeric_id = 1001

    profile = {
        "version": "3",
        "ID": str(numeric_id),
        "nextId": 1000,
        
        # [FIX] OrderBoard is now correctly seeded on account creation
        "inventory": [
            {
                "ID": 3022, # OrderBoard definition
                "version": 0,
                "Definition": 3022,
                "Placed": {"x": 2, "y": 48}, # Typical start location
                "Level": 1,
                "BuildStartTime": 0,
                "IsNew": False
            }
        ],
        
        "villainQueue": [],
        "pendingTransactions": [],
        "unlocks": [],
        "socialRewards": [],
        "PlatformStoreTransactionIDs": [],
        
        "highestFtueLevel": 999,
        "lastLevelUpTime": 0,
        "lastGameStartTime": 0,
        "totalGameplayDurationSinceLastLevelUp": 0,
        "targetExpansionID": 0,
        "freezeTime": 0
    }
    
    return profile
