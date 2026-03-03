# Kampai (Minion Paradise) — Game Architecture Overview

## Project Structure

```
c:\Unity\
  FRANKENSTEIN-UNITYPROJECT/     # Unity game client (decompiled)
    Assets/Scripts/Assembly-CSharp/Kampai/
      Game/                      # Core game logic
      Util/                      # Utilities, JSON parsers
      Splash/                    # Splash/loading screens
      UI/                        # UI layer
  SERVER/                        # Local mock server
    kampai_server.py             # Flask server (ENTRYPOINT)
    server/                      # Modular backend routes (user, game, metrics, profile)
    definitions.json             # Game definitions (all game data)
    player_data/                 # Per-user saved state
  DOC/                           # This documentation
```

---

## Server Architecture

~~The mock server (`kampai_server.py`) runs two Flask instances on ports **44732** and **44733** and exposes these endpoints:~~
*(Updated: `kampai_server.py` has been refactored into a modular Flask application mapped by Route decorators in the `server/routes` directory.)*

| Endpoint | Method | Purpose |
|---|---|---|
| `/rest/config/<path>` | GET | Game configuration (DLC URLs, feature flags) |
| `/rest/definitions/<version>` | GET | Serves `definitions.json` |
| `/rest/user/login` | POST | Login/session creation |
| `/rest/user/register` | POST | New user registration |
| `/rest/gamestate/<user_id>` | GET | Load player save |
| `/rest/gamestate/<user_id>` | POST | Save player state |
| `/rest/dlc/manifest/<version>` | GET | DLC asset manifest |
| `/rest/healthMetrics/meters` | POST | Health metrics (no-op) |

---

## Client Initialization Pipeline
The Minions Paradise client relies extensively on an Inverse of Control (IoC) driven architecture via **StrangeIoC**.

1. **SplashContext**
    - Boots the application, initializes basic `LoadInService` and `VideoService`.
    - Shows the initial loading tip/video.
2. **MainContext**
    - Responsible for setting up the `GameStartCommand`.
    - Triggers the user login phase (`UserSessionService.LoginRequestCallback()`). The client generates `isNewUser: True` dynamic checks.
3. **GameContext**
    - Mounts the actual game map, injects `PlayerService`, `DefinitionService`, etc.
    - Executes `LoadPlayerCommand` to fetch `/rest/gamestate/<userId>`.
    - ~~Resolves local vs server persistence fallbacks.~~ *(Updated: The game now operates in strictly online remote mode `LoadMode = 'remote'`).*

---

## Key Namespaces

| Namespace | Purpose |
|---|---|
| `Kampai.Game` | Core game logic (definitions, player, instances) |
| `Kampai.Game.Transaction` | Crafting/transaction system |
| `Kampai.Util` | JSON parsers, utilities |
| `Kampai.Splash` | Loading screen tips |

---

## JSON Parsing System

The game uses a **custom fast JSON parser** (not full Newtonsoft.Json) for performance:

- `FastJSONDeserializer.Deserialize<T>()` — deserializes a typed object
- `FastJsonCreationConverter<T>` — factory converter (similar to Newtonsoft `CustomCreationConverter`)
- `IFastJSONDeserializable` — interface all parseable types implement
- `ReaderUtil` — helpers (`PopulateList`, `ReadLocation`, etc.)

The `Definition.Deserialize()` base method reads property names `.ToUpper()` before dispatch, so all `DeserializeProperty` switch cases must use **UPPERCASE** keys (e.g. `"OUTPUTS"`, `"ID"`, `"LOCALIZEDKEY"`).

~~Newtonsoft.Json originally caused InvalidCastException across payloads like BuildMenuLocalState and LocalResourcesManifest.~~ *(We have successfully eradicated Newtonsoft parsing in favor of FastJsonParser for core startup components like `UserSessionService`).*

---

## Known Decompilation Artifacts

The codebase was decompiled from IL bytecode. A recurring decompilation artifact introduces broken switch-case `default:` blocks:

~~// BROKEN (decompiler artifact):~~
~~default:~~
~~{~~
~~    int num;~~
~~    num = 1;  // artificially added~~
~~    if (num == 1)~~
~~    {~~
~~        // ... reads some field ...~~
~~        break;~~
~~    }~~
~~    return false;  // unreachable~~
~~}~~

~~This causes the `default:` case to **always** execute for any unrecognized property, swallowing the value and breaking the parser.~~ 

*(Updated: All 15+ Definition classes suffering from this artifact have been manually repaired. Explicit standard case-switch blocks fall cleanly through to `base.DeserializeProperty(...)`.)*
