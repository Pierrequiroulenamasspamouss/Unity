from flask import Blueprint, request, jsonify, send_file, current_app
import os
import json
from utils.profile import generate_new_player_profile

game_bp = Blueprint('game', __name__)

BASE_HOST = "http://localhost"
VIDEO_PATH = r"C:\Unity\SERVER\assets\video.mp4"
DEFINITIONS_PATH = r"C:\Unity\SERVER\definitions.json"

@game_bp.route('/video.mp4')
def serve_video():
    if os.path.exists(VIDEO_PATH): return send_file(VIDEO_PATH, mimetype='video/mp4')
    return "", 404

@game_bp.route('/rest/config/<path:path>', methods=['GET'])
def get_config(path):
    manifest_url = f"{BASE_HOST}:44733/rest/dlc/manifest/prod_v1"
    return jsonify({
        "serverPushNotifications": False,
        "minimumVersion": 0.0,
        "rateAppAfter": {
            "LevelUp": False, "XPPayout": False, "VillainCutscene": False
        },
        "maxRPS": 5, "killSwitches": None, "msHeartbeat": 1000,
        "fpsHeartbeat": 60, "logLevel": 0, "healthMetricPercentage": 100,
        "nudgeUpgradePercentage": 0,
        "dlcManifests": {
            "low": manifest_url, "mid": manifest_url, "high": manifest_url, "android": manifest_url
        },
        "featureAccess": {
            "DeltaDNA": {}, "MoreGames": {}, "CloudSave": {}
        },
        "targetPerformance": "LOW",
        "definitionId": "prod_v1",
        "definitions": f"{BASE_HOST}:44733/rest/definitions/prod_v1",
        "upsightPromoTriggers": [],
        "allowedVersions": ["0.0.0", "9.9.9"],
        "nudgeUpgradeVersions": [],
        "logglyConfig": {
            "logLevel": 0, "samplePercentage": 0
        },
        "videoUri": f"{BASE_HOST}:44733/video.mp4",
        "assetBundles": f"{BASE_HOST}:44733/assetbundles",
        "client_telemetry_url": f"{BASE_HOST}:44733/rest/telemetry/client"
    })

@game_bp.route('/rest/dlc/manifest/<version>', methods=['GET'])
def get_manifest(version):
    return jsonify({ "id": version, "baseURL": f"{BASE_HOST}:44733/assets/", "assets": {}, "bundles": [], "bundledAssets": [] })

@game_bp.route('/rest/definitions/<version>', methods=['GET'])
def get_definitions(version):
    if os.path.exists(DEFINITIONS_PATH): return send_file(DEFINITIONS_PATH, mimetype='application/json')
    return jsonify({})

@game_bp.route('/rest/gamestate/<user_id>', methods=['GET'])
def get_gamestate(user_id):
    player_data_dir = os.path.join(os.path.dirname(__file__), '..', 'player_data')
    os.makedirs(player_data_dir, exist_ok=True)
    player_file = os.path.join(player_data_dir, f'{user_id}.json')
    
    if os.path.exists(player_file):
        print(f"[GAME] LOADING EXISTING PROFILE for user {user_id}")
        try:
            with open(player_file, 'r') as f:
                profile = json.load(f)
        except Exception as e:
            print(f"[GAME] ERROR LOADING PROFILE: {e}, generating new one")
            profile = generate_new_player_profile(user_id)
    else:
        print(f"[GAME] GENERATING NEW PROFILE for user {user_id}")
        profile = generate_new_player_profile(user_id)
        # Save immediately so the next load works correctly
        try:
            with open(player_file, 'w') as f:
                json.dump(profile, f, indent=2)
        except Exception as e:
            print(f"[GAME] ERROR SAVING NEW PROFILE: {e}")
    
    json_str = json.dumps(profile, ensure_ascii=False)
    return current_app.response_class(
        response=json_str,
        status=200,
        mimetype='application/json'
    )

@game_bp.route('/rest/gamestate/<user_id>', methods=['POST'])
def save_gamestate(user_id):
    try:
        player_data = request.get_json()
        player_data_dir = os.path.join(os.path.dirname(__file__), '..', 'player_data')
        os.makedirs(player_data_dir, exist_ok=True)
        player_file = os.path.join(player_data_dir, f'{user_id}.json')
        
        with open(player_file, 'w') as f:
            json.dump(player_data, f, indent=2)
        
        return jsonify({"success": True})
    except Exception as e:
        print(f"[GAME] ERROR SAVING PROFILE: {e}")
        return jsonify({"success": False, "error": str(e)}), 500

@game_bp.route('/rest/gamestate/<user_id>/reset', methods=['POST'])
def reset_gamestate(user_id):
    player_file = os.path.join(os.path.dirname(__file__), '..', 'player_data', f'{user_id}.json')
    if os.path.exists(player_file):
        os.remove(player_file)
        print(f"[GAME] RESET PROFILE for user {user_id}")
    return jsonify({"success": True})
