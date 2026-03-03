from flask import Blueprint, request, jsonify
import uuid
import os

user_bp = Blueprint('user', __name__)

@user_bp.route('/rest/user/login', methods=['POST'])
@user_bp.route('/rest/user/session', methods=['POST'])
def login():
    try:
        data = request.get_json(force=True, silent=True) or {}
    except Exception:
        data = {}
    
    print(f"[LOGIN] Raw Request Data: {request.data.decode('utf-8', errors='ignore')}")
    uid_str = data.get('userId', data.get('UserID', '1000000000'))
    
    # Check if this user already has a save file
    # This determines if they get the intro video or skip straight to the game
    player_file = os.path.join(os.path.dirname(__file__), '..', 'player_data', f'{uid_str}.json')
    is_new = not os.path.exists(player_file)
    
    print(f"[LOGIN] UserID={uid_str} | isNewUser={is_new}")
    
    return jsonify({
        "userId": uid_str, 
        "synergyId": f"syn_{uid_str}",
        "isNewUser": is_new, 
        "isTester": True, 
        "country": "US",
        "tosVersion": "1.0", 
        "privacyVersion": "1.0"
    })

@user_bp.route('/rest/user/register', methods=['POST'])
def register():
    new_numeric_id = 1000000000 + int(uuid.uuid4().int % 2000000000)
    new_id_str = str(new_numeric_id)
    
    print(f"[REGISTER] UserID={new_id_str}", flush=True)
    return jsonify({
        "userId": new_id_str, "synergyId": f"syn_{new_id_str}",
        "secret": "mock", "sessionKey": "mock",
        "isNewUser": True, "isTester": True, "country": "US"
    })
