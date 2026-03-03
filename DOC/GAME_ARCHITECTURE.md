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
    kampai_server.py             # Flask server
    definitions.json             # Game definitions (all game data)
    player_data/                 # Per-user saved state
  DOC/                           # This documentation
```

---

## Server Architecture

The mock server (`kampai_server.py`) runs two Flask instances on ports **44732** and **44733** and exposes these endpoints:

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

```
App Start
  → ConfigurationsService.Deserialize()      ← GET /rest/config/...
  → DefinitionService.Deserialize()          ← GET /rest/definitions/prod_v1
      → Definitions.Deserialize() (JSON)
      → MarkDefinitions() / MarkMoreDefinitions()  (builds AllDefinitions dict)
  → LoginCommand                             ← POST /rest/user/login
  → LoadPlayerCommand                        ← GET /rest/gamestate/<userId>
      → DefaultPlayerSerializer.Deserialize()
          → InventoryFastConverter (per item)
              → definitionService.Get(defId)
              → Instance constructor
```

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

---

## Known Decompilation Artifacts

The codebase was decompiled from IL bytecode. A recurring decompilation artifact introduces broken switch-case `default:` blocks:

```csharp
// BROKEN (decompiler artifact):
default:
{
    int num;
    num = 1;  // artificially added
    if (num == 1)
    {
        // ... reads some field ...
        break;
    }
    return false;  // unreachable
}
```

This causes the `default:` case to **always** execute for any unrecognized property, swallowing the value and breaking the parser. 

**Fixed files:**
- `TransactionDefinition.cs` — `OUTPUTS` is now an explicit case; `default:` falls through to `base`
- `Plot.cs` — `ID` is now an explicit case; `default:` returns `false`
