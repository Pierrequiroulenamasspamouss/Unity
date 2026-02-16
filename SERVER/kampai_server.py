from flask import Flask, request, jsonify, send_file
import os
import json
import uuid
import threading
import logging
from collections import OrderedDict

# Disable verbose logs
log = logging.getLogger('werkzeug')
log.setLevel(logging.ERROR)

# ================= CONFIGURATION =================
VIDEO_PATH = r"C:\Unity\SERVER\video.mp4"
DEFINITIONS_PATH = r"C:\Unity\SERVER\definitions.json"
BASE_HOST = "http://localhost"

# ================= PLAYER PROFILE GENERATOR =================
def generate_new_player_profile(user_id_str):
    # Logic: We use a standard dictionary. Flask's jsonify will format it correctly.
    # We send "ID" as a huge integer.
    
    numeric_id = 1001
    try:
        # Try processing as decimal first (standard case)
        numeric_id = int(str(user_id_str))
    except ValueError:
        try:
            # Fallback to hex
            numeric_id = int(str(user_id_str), 16)
        except ValueError:
            numeric_id = 1001

    # Cap at reasonable max for C# long (Int64.MaxValue is ~9e18)
    if numeric_id > 9000000000000000000:
            numeric_id = numeric_id % 9000000000000000000
    
    # Ensure it's never 0
    if numeric_id == 0: numeric_id = 1001

    profile = {
        # PlayerData.cs Case "VERSION"
        "version": "3",
        
        # PlayerData.cs Case "ID" -> expects Convert.ToInt64(reader.Value)
        # FIX: Send as STRING to avoid JSON integer overflow/parsing issues on client
        "ID": str(numeric_id),
        
        # PlayerData.cs Case "NEXTID"
        "nextId": 1000,
        
        # PlayerData.cs Case "INVENTORY"
        # Empty inventory to avoid InventoryFastConverter issues with definition mismatches
        # Building defs (like 3022) are NOT valid inventory instance types
        "inventory": [],
        
        # Required Lists to prevent NullReference in other services
        "villainQueue": [],
        "pendingTransactions": [],
        "unlocks": [],
        "socialRewards": [],
        "PlatformStoreTransactionIDs": [],
        
        # Required Ints
        "highestFtueLevel": 999,
        "lastLevelUpTime": 0,
        "lastGameStartTime": 0,
        "totalGameplayDurationSinceLastLevelUp": 0,
        "targetExpansionID": 0,
        "freezeTime": 0
    }
    
    return profile

# ================= APP SETUP =================
def create_app(port):
    app = Flask(f"App_{port}")
    
    # CRITICAL: Keep JSON order intact
    app.config['JSON_SORT_KEYS'] = False

    # --- ROUTES ---
    @app.route('/video.mp4')
    def serve_video():
        if os.path.exists(VIDEO_PATH): return send_file(VIDEO_PATH, mimetype='video/mp4')
        return "", 404

    @app.route('/rest/config/<path:path>', methods=['GET'])
    def get_config(path):
        manifest_url = f"{BASE_HOST}:44733/rest/dlc/manifest/prod_v1"
        # ALL fields below are supported by ConfigurationDefinition.DeserializeProperty
        return jsonify({
            "serverPushNotifications": False,
            "minimumVersion": 0.0,
            "rateAppAfter": {
                "LevelUp": False,
                "XPPayout": False,
                "VillainCutscene": False
            },
            "maxRPS": 5,
            "killSwitches": None,
            "msHeartbeat": 1000,
            "fpsHeartbeat": 60,
            "logLevel": 0,
            "healthMetricPercentage": 100,
            "nudgeUpgradePercentage": 0,
            "dlcManifests": {
                "low": manifest_url,
                "mid": manifest_url,
                "high": manifest_url,
                "android": manifest_url
            },
            "featureAccess": {
                "DeltaDNA": {},
                "MoreGames": {},
                "CloudSave": {}
            },
            "targetPerformance": "LOW",
            "definitionId": "prod_v1",
            "definitions": f"{BASE_HOST}:44733/rest/definitions/prod_v1",
            "upsightPromoTriggers": [],
            "allowedVersions": ["0.0.0", "9.9.9"],
            "nudgeUpgradeVersions": [],
            "logglyConfig": {
                "logLevel": 0,
                "samplePercentage": 0
            },
            "videoUri": f"{BASE_HOST}:44733/video.mp4",
            # These can be included - the fixed deserializer will skip them properly
            "assetBundles": f"{BASE_HOST}:44733/assetbundles",
            "client_telemetry_url": f"{BASE_HOST}:44733/rest/telemetry/client"
        })

    @app.route('/rest/healthMetrics/meters', methods=['POST'])
    def health_metrics():
        return jsonify({
            "logglyConfig": { "logLevel": 0, "samplePercentage": 0 },
            "killSwitches": []
        })

    @app.route('/rest/dlc/manifest/<version>', methods=['GET'])
    def get_manifest(version):
        return jsonify({ "id": version, "baseURL": f"{BASE_HOST}:44733/assets/", "assets": {}, "bundles": [], "bundledAssets": [] })

    @app.route('/rest/definitions/<version>', methods=['GET'])
    def get_definitions(version):
        if os.path.exists(DEFINITIONS_PATH): return send_file(DEFINITIONS_PATH, mimetype='application/json')
        return jsonify({})

    @app.route('/rest/user/login', methods=['POST'])
    @app.route('/rest/user/session', methods=['POST'])
    def login():
        data = request.json or {}
        # Client sends UserID as a decimal string, NOT hex
        # e.g., "1562897461" is already a decimal number
        uid_str = data.get('userId', '1000000000')
        
        print(f"[{port}] LOGIN: UserID={uid_str}")
        
        return jsonify({
            "userId": uid_str, 
            "synergyId": f"syn_{uid_str}",
            "isNewUser": False, 
            "isTester": True, 
            "country": "US",
            "tosVersion": "1.0", 
            "privacyVersion": "1.0"
        })

    @app.route('/rest/user/register', methods=['POST'])
    def register():
        # Generate random numeric ID (Safe for Int64)
        new_numeric_id = 1000000000 + int(uuid.uuid4().int % 2000000000)
        new_id_str = str(new_numeric_id)
        
        return jsonify({
            "userId": new_id_str, "synergyId": f"syn_{new_id_str}",
            "secret": "mock", "sessionKey": "mock",
            "isNewUser": True, "isTester": True, "country": "US"
        })

    @app.route('/rest/gamestate/<user_id>', methods=['GET'])
    def get_gamestate(user_id):
        # Try to load existing player data from file
        player_data_dir = os.path.join(os.path.dirname(__file__), 'player_data')
        os.makedirs(player_data_dir, exist_ok=True)
        player_file = os.path.join(player_data_dir, f'{user_id}.json')
        
        if os.path.exists(player_file):
            print(f"[{port}] LOADING EXISTING PROFILE for user {user_id}")
            try:
                with open(player_file, 'r') as f:
                    profile = json.load(f)
            except Exception as e:
                print(f"[{port}] ERROR LOADING PROFILE: {e}, generating new one")
                profile = generate_new_player_profile(user_id)
        else:
            print(f"[{port}] GENERATING NEW PROFILE for user {user_id}")
            profile = generate_new_player_profile(user_id)
            # Save immediately
            try:
                with open(player_file, 'w') as f:
                    json.dump(profile, f, indent=2)
            except Exception as e:
                print(f"[{port}] ERROR SAVING NEW PROFILE: {e}")
        
        json_str = json.dumps(profile, ensure_ascii=False)
        return app.response_class(
            response=json_str,
            status=200,
            mimetype='application/json'
        )

    @app.route('/rest/gamestate/<user_id>', methods=['POST'])
    def save_gamestate(user_id):
        print(f"[{port}] SAVING PROFILE for user {user_id}")
        
        # Get player data from request
        try:
            player_data = request.get_json()
            
            # Save to file
            player_data_dir = os.path.join(os.path.dirname(__file__), 'player_data')
            os.makedirs(player_data_dir, exist_ok=True)
            player_file = os.path.join(player_data_dir, f'{user_id}.json')
            
            with open(player_file, 'w') as f:
                json.dump(player_data, f, indent=2)
            
            print(f"[{port}] PROFILE SAVED to {player_file}")
            return jsonify({"success": True})
        except Exception as e:
            print(f"[{port}] ERROR SAVING PROFILE: {e}")
            return jsonify({"success": False, "error": str(e)}), 500

    @app.route('/persistence', methods=['POST', 'GET'])
    def persistence_fallback():
        return jsonify({"status": "success", "digest": "mock"})

    @app.route('/rest/telemetry', methods=['POST'])
    @app.route('/metrics', methods=['POST'])
    def telemetry():
        return jsonify({"status": "ok"})

    return app

def run_server(port):
    app = create_app(port)
    print(f">>> Server started on port {port}")
    app.run(host='0.0.0.0', port=port, threaded=True, use_reloader=False)

if __name__ == '__main__':
    t1 = threading.Thread(target=run_server, args=(44733,))
    t2 = threading.Thread(target=run_server, args=(44732,))
    t1.start()
    t2.start()
    t1.join()
    t2.join()