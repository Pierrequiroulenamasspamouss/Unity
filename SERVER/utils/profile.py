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
        
        # [FIX] All Required Buildings are now correctly seeded with State and Location on account creation
        "inventory": [
            {
                "ID": 3022,
                "version": 0,
                "Definition": 3022,
                "Location": {"x": 2, "y": 48},
                "Level": 1,
                "State": 3, # Idle
                "BuildStartTime": 0,
                "IsNew": False
            },
            { "ID": 3041, "version": 0, "Definition": 3041, "Location": {"x": 3, "y": 50}, "Level": 1, "State": 3, "BuildStartTime": 0, "IsNew": False },
            { "ID": 3054, "version": 0, "Definition": 3054, "Location": {"x": 4, "y": 50}, "Level": 1, "State": 3, "BuildStartTime": 0, "IsNew": False },
            { "ID": 3055, "version": 0, "Definition": 3055, "Location": {"x": 5, "y": 50}, "Level": 1, "State": 3, "BuildStartTime": 0, "IsNew": False },
            { "ID": 3070, "version": 0, "Definition": 3070, "Location": {"x": 6, "y": 50}, "Level": 1, "State": 3, "BuildStartTime": 0, "IsNew": False },
            { "ID": 3042, "version": 0, "Definition": 3042, "Location": {"x": 7, "y": 50}, "Level": 1, "State": 3, "BuildStartTime": 0, "IsNew": False },
            { "ID": 3043, "version": 0, "Definition": 3043, "Location": {"x": 8, "y": 50}, "Level": 1, "State": 3, "BuildStartTime": 0, "IsNew": False },
            { "ID": 3044, "version": 0, "Definition": 3044, "Location": {"x": 9, "y": 50}, "Level": 1, "State": 3, "BuildStartTime": 0, "IsNew": False },
            { "ID": 3553, "version": 0, "Definition": 3553, "Location": {"x": 10, "y": 50}, "Level": 1, "State": 3, "BuildStartTime": 0, "IsNew": False }
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
