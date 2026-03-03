# Definitions Reference

This document maps known definition ID ranges to their types.

## Item Definitions (itemDefinitions)
IDs: 0–10 (core currencies), 11+ (various items)

| ID | LocalizedKey | Description |
|---|---|---|
| 0 | Bananas | Primary soft currency |
| 1 | Diamonds | Premium currency |
| 2 | Sand Dollars | Secondary soft currency |
| 3 | XP | Experience points |
| 4 | (unlock token) | |
| 5–10 | misc items | |

## Minion Definitions (minionDefinitions)
IDs: 600–699 (regular minions)

| ID Range | Type |
|---|---|
| 601–607 | Standard playable minions |

## Named Character Definitions (namedCharacterDefinitions)
IDs: 70000–79999 (named characters behave like special minions)

| ID | Name |
|---|---|
| 70000 | Phil |
| 70008 | TravellingSalesMinion (TSM) |

## Building Definitions (buildingDefinitions)
IDs: 3000–3999 (placed buildings on the island)

| ID Range | Description |
|---|---|
| 3000–3099 | Standard buildings (huts, shops, etc.) |
| 3100–3999 | Extended buildings |

Notable Player JSON buildings (verified in definitions.json):
- 3002, 3018, 3032, 3041, 3042, 3043, 3044, 3053, 3054, 3055, 3070, 3094, 3096, 3553

## Plot Definitions (plotDefinitions)
IDs: 60000–60999

| ID | Type | Description |
|---|---|---|
| 60002 | RED_CARPET (NoOpPlotDefinition) | Decorative plot tile |

## Purchased Land Expansion Definitions (purchasedExpansionDefinitions)
IDs: 30000+

| ID | Description |
|---|---|
| 30000 | Primary land expansion |

## Weighted Definitions (weightedDefinitions)
IDs: 4000–4999 (gacha/drop pools)

## Transactions (transactions)
IDs: 18–5999+ (crafting recipes)

~~> [!IMPORTANT]~~
~~> Transactions must NOT have IDs that overlap with `itemDefinitions` (0–10+) or building IDs. The `DefinitionService` now registers items before transactions, and skips duplicates (first-wins), so item ownership of low IDs is guaranteed.~~
*(Updated: The C# `DeserializeProperty(propertyName)` decompilation bug zeroing out IDs has been thoroughly rectified across the codebase. Base-call property inheritance successfully parses IDs, eliminating collisions naturally.)*

## Currency Item Definitions (currencyItemDefinitions)
IAP Packs — IDs: 8001–8999

| ID | Description |
|---|---|
| 8001 | Few Diamonds (IAP pack) |
| 8002 | Pile of Diamonds |
| 8003 | Sack of Diamonds |
| 8004 | Pile of Sand Dollars |
| 8005 | Bag of Sand Dollars |

## Special Single-Entry Definitions
| ID | Type | Notes |
|---|---|---|
| 88888 | LevelUpDefinition | Registered by AddLevelUpDefinition() |
| 88889 | DropLevelBandDefinition | Registered by AddDropLevelBandDefinition() |
| 99999 | LevelXPTable | Registered by AddLevelXPTable() |
| N/A | MarketplaceDefinition | (Injected natively into `/rest/definitions` server endpoint payload base) |
