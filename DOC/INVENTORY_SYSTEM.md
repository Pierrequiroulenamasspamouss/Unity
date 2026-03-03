# Inventory & Definition System

## Inventory Item Types (InventoryFastConverter)

When a player's JSON `"def"` ID is looked up, the `DefinitionService` returns a `Definition` object, and `InventoryFastConverter.Create()` maps it to its Instance type:

| Definition Type | Instance Type | How created |
|---|---|---|
| `ItemDefinition` | `Item` | Via `IBuilder<Instance>.Build()` |
| `BuildingDefinition` (abstract) | various `Building` subtypes | Via `IBuilder<Instance>.Build()` |
| `NamedCharacterDefinition` (abstract) | character instance | Via `IBuilder<Instance>.Build()` |
| `MinionDefinition` | `Minion` | Explicit case |
| `Transaction.WeightedDefinition` | `Transaction.WeightedInstance` | Explicit case |
| `PrestigeDefinition` | `Prestige` | Explicit case |
| `RewardCollectionDefinition` | `RewardCollection` | Explicit case |
| `NoOpPlotDefinition` | `NoOpPlot` | Explicit case |
| `PurchasedLandExpansionDefinition` | `PurchasedLandExpansion` | Explicit case |
| `CompositeBuildingPieceDefinition` | `CompositeBuildingPiece` | Explicit case |
| `StickerDefinition` | `Sticker` | Explicit case |
| `MarketplaceSaleSlotDefinition` | `MarketplaceSaleSlot` | Explicit case |
| `MarketplaceItemDefinition` | `MarketplaceSaleItem` or `MarketplaceBuyItem` | Explicit case (depends on BuyQuantity) |
| `MarketplaceRefreshTimerDefinition` | `MarketplaceRefreshTimer` | Explicit case |
| `Transaction.TransactionDefinition` | `Transaction.TransactionInstance` | Explicit case (safety net) |

## Definition Deserialization

Definitions are parsed by the custom fast JSON stack. All property names are `.ToUpper()` before dispatch.

Key rule: `default:` in any `DeserializeProperty` override **must** call `return base.DeserializeProperty(...)` — do NOT read the reader inside `default:` without routing to base, or the `ID` and `LocalizedKey` fields will be swallowed.

## DefinitionService.MarkDefinitions() Order

Definitions are registered into `AllDefinitions` (a `Dictionary<int, Definition>`). The order matters because `MarkDefinitionAsUsed()` skips duplicates (first-wins). Current priority order:

1. `itemDefinitions` ← **top priority** (IDs 0-10 are currencies/items)
2. `minionDefinitions`
3. `currencyItemDefinitions`
4. `storeItemDefinitions`
5. `buildingDefinitions`
6. `plotDefinitions`
7. `purchasedExpansionDefinitions`
8. `commonExpansionDefinitions`
9. `expansionDefinitions`
10. `debrisDefinitions`
11. `aspirationalBuildingDefinitions`
12. `footprintDefinitions`
13. `weightedDefinitions`
14. `transactions` ← **lowest priority** (489 entries; their IDs must not collide with items)

## Player Data Format

The server sends player data as JSON. The `DefaultPlayerSerializer` parses it.

```json
{
  "version": 3,
  "ID": "1001",
  "nextId": 1000,
  "inventory": [
    { "def": 0, "id": 100, "quantity": 500 },
    { "def": 70000, "id": 101, "name": "Phil" }
  ],
  "unlocks": [],
  "villainQueue": [],
  "pendingTransactions": [],
  "socialRewards": [],
  "PlatformStoreTransactionIDs": [],
  "highestFtueLevel": 999,
  "lastLevelUpTime": 0,
  "lastGameStartTime": 0,
  "totalGameplayDurationSinceLastLevelUp": 0,
  "targetExpansionID": 0,
  "freezeTime": 0
}
```

- `"def"` or `"Definition"` — identifies the definition ID in `AllDefinitions`
- `"id"` — the instance's runtime ID (must be unique within the player's inventory)
