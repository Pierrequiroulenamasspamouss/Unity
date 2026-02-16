namespace Kampai.Game
{
	public class OrderBoardService : global::Kampai.Game.IOrderBoardService
	{
		private global::Kampai.Game.OrderBoard board;

		private int incrementCounter;

		private float ticketDifficulty = 1f;

		private int targetValue;

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Common.IRandomService randomService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService characterService { get; set; }

		[Inject]
		public global::Kampai.Game.ShowDialogSignal showDialogSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardUpdateTicketOnBoardSignal updateTicketOnBoardSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public void Initialize(global::Kampai.Game.OrderBoard orderBoard)
		{
			board = orderBoard;
		}

		public global::Kampai.Game.OrderBoard GetBoard()
		{
			return board;
		}

		public void ReplaceCharacterTickets(int characterDefinitionID)
		{
			foreach (global::Kampai.Game.OrderBoardTicket ticket in board.tickets)
			{
				if (ticket.CharacterDefinitionId == characterDefinitionID)
				{
					GetNewTicket(ticket.BoardIndex);
				}
			}
			updateTicketOnBoardSignal.Dispatch();
		}

		public void GetNewTicket(int orderBoardIndex)
		{
			bool flag = false;
			global::System.Collections.Generic.IList<string> list = new global::System.Collections.Generic.List<string>(board.Definition.OrderNames);
			global::System.Collections.Generic.IList<string> list2 = new global::System.Collections.Generic.List<string>();
			global::Kampai.Game.OrderBoardTicket orderBoardTicket = null;
			foreach (global::Kampai.Game.OrderBoardTicket ticket in board.tickets)
			{
				if (ticket.BoardIndex == orderBoardIndex)
				{
					orderBoardTicket = ticket;
					flag = true;
				}
				else
				{
					list2.Add(list[ticket.OrderNameTableIndex]);
				}
			}
			foreach (string item2 in list2)
			{
				list.Remove(item2);
			}
			if (!flag)
			{
				orderBoardTicket = new global::Kampai.Game.OrderBoardTicket();
			}
			bool flag2 = false;
			orderBoardTicket.StartTime = -1;
			orderBoardTicket.BoardIndex = orderBoardIndex;
			orderBoardTicket.Difficulty = SetDifficultyLevel();
			orderBoardTicket.TransactionInst = CreateNewOrder(orderBoardTicket.Difficulty);
			if (orderBoardTicket.TransactionInst.Inputs.Count == 0)
			{
				return;
			}
			if (!flag)
			{
				board.tickets.Add(orderBoardTicket);
			}
			int currentTicketXP = GetCurrentTicketXP(orderBoardTicket);
			if (board.PriorityPrestigeDefinitionIDs.Count > 0)
			{
				flag2 = true;
				orderBoardTicket.CharacterDefinitionId = board.PriorityPrestigeDefinitionIDs[0];
				board.PriorityPrestigeDefinitionIDs.RemoveAt(0);
				CheckAndUpdatePriorityPrestigeCharacterXP(orderBoardTicket, currentTicketXP);
			}
			else if (ShouldBeCharacterOrder())
			{
				int num = PickCharacterDefinitionId(currentTicketXP);
				if (num != 0)
				{
					orderBoardTicket.CharacterDefinitionId = num;
					flag2 = true;
				}
			}
			if (!flag2)
			{
				int index = RollDice(0, list.Count);
				string item = list[index];
				orderBoardTicket.OrderNameTableIndex = board.Definition.OrderNames.IndexOf(item);
				orderBoardTicket.CharacterDefinitionId = 0;
			}
		}

		public void UpdateLevelBand()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.BlackMarketBoardLevelBandDefinition> levelBands = board.Definition.LevelBands;
			uint quantity = playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
			int currentLevelBandIndex = 0;
			for (int i = 0; i < levelBands.Count; i++)
			{
				if (quantity >= levelBands[i].Level)
				{
					currentLevelBandIndex = i;
				}
			}
			board.CurrentLevelBandIndex = currentLevelBandIndex;
			UpdateOrderNumber();
		}

		public void AddPriorityPrestigeCharacter(int prestigeDefinitionID)
		{
			if (!board.PriorityPrestigeDefinitionIDs.Contains(prestigeDefinitionID))
			{
				global::Kampai.Game.Prestige prestige = characterService.GetPrestige(prestigeDefinitionID);
				if (prestigeDefinitionID != 40003 || prestige.CurrentPrestigeLevel >= 1)
				{
					board.PriorityPrestigeDefinitionIDs.Add(prestigeDefinitionID);
					global::Kampai.Game.PrestigeDefinition definition = prestige.Definition;
					global::Kampai.Game.QuestDialogSetting questDialogSetting = new global::Kampai.Game.QuestDialogSetting();
					questDialogSetting.type = global::Kampai.UI.View.QuestDialogType.NEWPRESTIGE;
					questDialogSetting.additionalStringParameter = definition.LocalizedKey;
					showDialogSignal.Dispatch("AlertPrePrestige", questDialogSetting, new global::Kampai.Util.Tuple<int, int>(0, 0));
				}
			}
		}

		public void SetEnabled(bool b)
		{
			GetBoard().menuEnabled = b;
		}

		private void CheckAndUpdatePriorityPrestigeCharacterXP(global::Kampai.Game.OrderBoardTicket ticket, int currentTicketXP)
		{
			global::Kampai.Game.Prestige prestige = characterService.GetPrestige(ticket.CharacterDefinitionId);
			if (prestige == null)
			{
				return;
			}
			int neededPrestigePoints = prestige.NeededPrestigePoints;
			if (currentTicketXP < neededPrestigePoints || prestige.state != global::Kampai.Game.PrestigeState.PreUnlocked)
			{
				return;
			}
			int num = RollDice(1, neededPrestigePoints);
			float num2 = (float)num / (float)currentTicketXP;
			global::Kampai.Game.Transaction.TransactionInstance transactionInst = ticket.TransactionInst;
			uint num3 = 0u;
			foreach (global::Kampai.Util.QuantityItem input in transactionInst.Inputs)
			{
				num3 = (uint)((float)input.Quantity * num2);
				num3 = ((num3 == 0) ? 1u : num3);
				input.Quantity = num3;
			}
			foreach (global::Kampai.Util.QuantityItem output in transactionInst.Outputs)
			{
				num3 = (uint)((float)output.Quantity * num2);
				num3 = ((num3 == 0) ? 1u : num3);
				output.Quantity = num3;
			}
		}

		private bool ShouldBeCharacterOrder()
		{
			int num = RollDice(0, 100);
			if (num < board.Definition.CharacterOrderChance)
			{
				return true;
			}
			return false;
		}

		private int PickCharacterDefinitionId(int currentTicketXP)
		{
			global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.CharacterTicketData> dictionary = UpdateCharacterTicketDataOnOrderBoard();
			global::Kampai.Game.Transaction.WeightedDefinition weightedDefinition = new global::Kampai.Game.Transaction.WeightedDefinition();
			weightedDefinition.Entities = new global::System.Collections.Generic.List<global::Kampai.Game.Transaction.WeightedQuantityItem>();
			global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Prestige> allUnlockedPrestiges = characterService.GetAllUnlockedPrestiges();
			bool flag = characterService.IsTikiBarFull();
			bool flag2 = characterService.GetEmptyCabana() == null;
			if (flag && flag2)
			{
				return 0;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::Kampai.Game.Prestige> item in allUnlockedPrestiges)
			{
				global::Kampai.Game.Prestige value = item.Value;
				global::Kampai.Game.PrestigeDefinition definition = value.Definition;
				if ((!flag || definition.Type != global::Kampai.Game.PrestigeType.Minion) && (!flag2 || definition.Type != global::Kampai.Game.PrestigeType.Villain))
				{
					int num = 0;
					int num2 = 0;
					int iD = definition.ID;
					if (dictionary.ContainsKey(iD))
					{
						num = dictionary[iD].OnBoardCount;
						num2 = dictionary[iD].XPAmount;
					}
					if (num < definition.MaxedBadgedOrder && (value.state != global::Kampai.Game.PrestigeState.PreUnlocked || num < 1) && (value.state == global::Kampai.Game.PrestigeState.PreUnlocked || value.state == global::Kampai.Game.PrestigeState.Prestige) && num2 + value.CurrentPrestigePoints <= value.NeededPrestigePoints && (value.state != global::Kampai.Game.PrestigeState.PreUnlocked || value.CurrentPrestigePoints + currentTicketXP + num2 <= value.NeededPrestigePoints))
					{
						weightedDefinition.Entities.Add(new global::Kampai.Game.Transaction.WeightedQuantityItem(iD, 0u, definition.OrderBoardWeight));
					}
				}
			}
			if (weightedDefinition.Entities.Count == 0)
			{
				return 0;
			}
			global::Kampai.Game.Transaction.WeightedInstance weightedInstance = new global::Kampai.Game.Transaction.WeightedInstance(weightedDefinition);
			return weightedInstance.NextPick(randomService).ID;
		}

		private global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.CharacterTicketData> UpdateCharacterTicketDataOnOrderBoard()
		{
			global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.CharacterTicketData> dictionary = new global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.CharacterTicketData>();
			foreach (global::Kampai.Game.OrderBoardTicket ticket in board.tickets)
			{
				int characterDefinitionId = ticket.CharacterDefinitionId;
				if (characterDefinitionId != 0 && ticket.StartTime == -1)
				{
					if (!dictionary.ContainsKey(characterDefinitionId))
					{
						global::Kampai.Game.CharacterTicketData characterTicketData = new global::Kampai.Game.CharacterTicketData();
						characterTicketData.OnBoardCount = 1;
						characterTicketData.XPAmount = GetCurrentTicketXP(ticket);
						dictionary.Add(characterDefinitionId, characterTicketData);
					}
					else
					{
						dictionary[characterDefinitionId].OnBoardCount++;
						dictionary[characterDefinitionId].XPAmount += global::Kampai.Game.Transaction.TransactionUtil.ExtractQuantityFromTransaction(ticket.TransactionInst, 2);
					}
				}
			}
			return dictionary;
		}

		private void UpdateOrderNumber()
		{
			int count = playerService.GetAllUnLockedIngredients().Count;
			int num = 0;
			foreach (global::Kampai.Game.BlackMarketBoardUnlockedOrderSlotDefinition item in board.Definition.UnlockedIngredientsToOrderSlotsTable)
			{
				if (count >= item.UnlockItems)
				{
					num = item.OrderSlots;
				}
			}
			global::Kampai.Game.StuartCharacter firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.StuartCharacter>(70001);
			if (firstInstanceByDefinitionId == null)
			{
				num = 1;
			}
			int count2 = board.tickets.Count;
			if (num > count2)
			{
				for (int i = count2; i < num; i++)
				{
					GetNewTicket(i);
				}
			}
		}

		private global::Kampai.Game.Transaction.TransactionInstance CreateNewOrder(global::Kampai.Game.OrderBoardTicketDifficulty difficulty)
		{
			incrementCounter = 0;
			global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem> uniqueIngredients = GetUniqueIngredients();
			global::Kampai.Game.BlackMarketBoardLevelBandDefinition currentLevelBand = board.Definition.LevelBands[board.CurrentLevelBandIndex];
			global::Kampai.Game.BlackMarketBoardValidationType validationType = GetValidationType();
			global::Kampai.Game.BlackMarketBoardValidationType blackMarketBoardValidationType = validationType;
			if (blackMarketBoardValidationType == global::Kampai.Game.BlackMarketBoardValidationType.ITEM_QUANTITY)
			{
				SetIngredientsQtyForQuantityValidation(uniqueIngredients, currentLevelBand);
			}
			else
			{
				SetIngredientsQtyForValidation(uniqueIngredients, currentLevelBand, validationType, difficulty);
			}
			return CreateOrderBoardTransactionBasedOnQuantityList(uniqueIngredients);
		}

		private global::Kampai.Game.OrderBoardTicketDifficulty SetDifficultyLevel()
		{
			int easyWeight = board.Definition.LevelBands[board.CurrentLevelBandIndex].EasyWeight;
			int mediumWeight = board.Definition.LevelBands[board.CurrentLevelBandIndex].MediumWeight;
			int hardWeight = board.Definition.LevelBands[board.CurrentLevelBandIndex].HardWeight;
			int maxValue = easyWeight + mediumWeight + hardWeight;
			int num = RollDice(0, maxValue);
			if (num < easyWeight)
			{
				return global::Kampai.Game.OrderBoardTicketDifficulty.EASY;
			}
			if (num >= easyWeight && num < easyWeight + mediumWeight)
			{
				return global::Kampai.Game.OrderBoardTicketDifficulty.MEDIUM;
			}
			return global::Kampai.Game.OrderBoardTicketDifficulty.HARD;
		}

		private int GetCurrentTicketXP(global::Kampai.Game.OrderBoardTicket targetTicket)
		{
			return global::Kampai.Game.Transaction.TransactionUtil.ExtractQuantityFromTransaction(targetTicket.TransactionInst, 2);
		}

		private global::Kampai.Game.BlackMarketBoardValidationType GetValidationType()
		{
			global::Kampai.Util.QuantityItem quantityItem = board.weightedInstance.NextPick(randomService);
			return (global::Kampai.Game.BlackMarketBoardValidationType)quantityItem.ID;
		}

		private global::Kampai.Game.Transaction.TransactionInstance CreateOrderBoardTransactionBasedOnQuantityList(global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> qList)
		{
			global::Kampai.Game.Transaction.TransactionInstance transactionInstance = new global::Kampai.Game.Transaction.TransactionInstance();
			transactionInstance.Inputs = qList;
			int num = CalculateValue(qList, global::Kampai.Game.BlackMarketBoardValidationType.GRIND_REWARD);
			int num2 = CalculateValue(qList, global::Kampai.Game.BlackMarketBoardValidationType.XP_REWARD);
			num = ((num == 0) ? 1 : num);
			num2 = ((num2 == 0) ? 1 : num2);
			transactionInstance.Outputs = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
			transactionInstance.Outputs.Add(new global::Kampai.Util.QuantityItem(0, (uint)num));
			transactionInstance.Outputs.Add(new global::Kampai.Util.QuantityItem(2, (uint)num2));
			playerService.AssignNextInstanceId(transactionInstance);
			return transactionInstance;
		}

		private void SetIngredientsQtyForValidation(global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem> qList, global::Kampai.Game.BlackMarketBoardLevelBandDefinition currentLevelBand, global::Kampai.Game.BlackMarketBoardValidationType type, global::Kampai.Game.OrderBoardTicketDifficulty difficulty)
		{
			targetValue = GetTargetValue(type, difficulty);
			int num = 0;
			bool flag = true;
			while (flag || !IncrementIngredientsList(qList, currentLevelBand.MaxIngredientQty))
			{
				num = CalculateValue(qList, type);
				flag = false;
				if (num >= targetValue)
				{
					break;
				}
			}
			logger.Log(global::Kampai.Util.Logger.Level.Info, true, string.Format("Actual Value: {0}", num));
		}

		private void SetIngredientsQtyForQuantityValidation(global::System.Collections.Generic.IEnumerable<global::Kampai.Util.QuantityItem> qList, global::Kampai.Game.BlackMarketBoardLevelBandDefinition currentLevelBand)
		{
			foreach (global::Kampai.Util.QuantityItem q in qList)
			{
				int quantity = RollDice(currentLevelBand.MinIngredientQty, currentLevelBand.MaxIngredientQty + 1);
				q.Quantity = (uint)quantity;
				if (q.Quantity == 0)
				{
					q.Quantity = 1u;
				}
			}
		}

		private int GetTargetValue(global::Kampai.Game.BlackMarketBoardValidationType type, global::Kampai.Game.OrderBoardTicketDifficulty difficultyLevel)
		{
			int minValue = 0;
			int num = 0;
			float easyMultiplier = board.Definition.LevelBands[board.CurrentLevelBandIndex].EasyMultiplier;
			float mediumMultiplier = board.Definition.LevelBands[board.CurrentLevelBandIndex].MediumMultiplier;
			float hardMultiplier = board.Definition.LevelBands[board.CurrentLevelBandIndex].HardMultiplier;
			switch (difficultyLevel)
			{
			case global::Kampai.Game.OrderBoardTicketDifficulty.EASY:
				ticketDifficulty = easyMultiplier;
				break;
			case global::Kampai.Game.OrderBoardTicketDifficulty.MEDIUM:
				ticketDifficulty = mediumMultiplier;
				break;
			case global::Kampai.Game.OrderBoardTicketDifficulty.HARD:
				ticketDifficulty = hardMultiplier;
				break;
			default:
				logger.Error("No Difficulty Level for Tickets");
				break;
			}
			global::Kampai.Game.BlackMarketBoardLevelBandDefinition blackMarketBoardLevelBandDefinition = board.Definition.LevelBands[board.CurrentLevelBandIndex];
			switch (type)
			{
			case global::Kampai.Game.BlackMarketBoardValidationType.GRIND_REWARD:
				minValue = (int)((float)blackMarketBoardLevelBandDefinition.MinGrindReward * ticketDifficulty);
				num = (int)((float)blackMarketBoardLevelBandDefinition.MaxGrindReward * ticketDifficulty);
				break;
			case global::Kampai.Game.BlackMarketBoardValidationType.TIME_INVESTMENT:
				minValue = (int)((float)blackMarketBoardLevelBandDefinition.MinInvestmentTime * ticketDifficulty);
				num = (int)((float)blackMarketBoardLevelBandDefinition.MaxInvestmentTime * ticketDifficulty);
				break;
			case global::Kampai.Game.BlackMarketBoardValidationType.XP_REWARD:
				minValue = (int)((float)blackMarketBoardLevelBandDefinition.MinXPReward * ticketDifficulty);
				num = (int)((float)blackMarketBoardLevelBandDefinition.MaxXPReward * ticketDifficulty);
				break;
			default:
				logger.Error("Invalid validation type");
				break;
			}
			return targetValue = RollDice(minValue, num + 1);
		}

		private int CalculateValue(global::System.Collections.Generic.IEnumerable<global::Kampai.Util.QuantityItem> qList, global::Kampai.Game.BlackMarketBoardValidationType type)
		{
			int num = 0;
			global::Kampai.Game.BlackMarketBoardLevelBandDefinition blackMarketBoardLevelBandDefinition = board.Definition.LevelBands[board.CurrentLevelBandIndex];
			switch (type)
			{
			case global::Kampai.Game.BlackMarketBoardValidationType.GRIND_REWARD:
				foreach (global::Kampai.Util.QuantityItem q in qList)
				{
					global::Kampai.Game.IngredientsItemDefinition ingredientsItemDefinition2 = definitionService.Get<global::Kampai.Game.IngredientsItemDefinition>(q.ID);
					num += ingredientsItemDefinition2.BaseGrindCost * (int)q.Quantity;
				}
				num = (int)((float)num * blackMarketBoardLevelBandDefinition.GrindMultipler);
				break;
			case global::Kampai.Game.BlackMarketBoardValidationType.TIME_INVESTMENT:
				foreach (global::Kampai.Util.QuantityItem q2 in qList)
				{
					global::Kampai.Game.IngredientsItemDefinition ingredientsItemDefinition3 = definitionService.Get<global::Kampai.Game.IngredientsItemDefinition>(q2.ID);
					int quantityByDefinitionId = (int)playerService.GetQuantityByDefinitionId(ingredientsItemDefinition3.ID);
					int num2 = (int)q2.Quantity - quantityByDefinitionId;
					num2 = ((num2 >= 0) ? num2 : 0);
					num += (int)ingredientsItemDefinition3.TimeToHarvest * num2;
					if (ingredientsItemDefinition3.Tier > 0)
					{
						num += playerService.GetInvestmentTimeForTransaction(ingredientsItemDefinition3.TransactionId) * num2;
					}
				}
				break;
			case global::Kampai.Game.BlackMarketBoardValidationType.XP_REWARD:
				foreach (global::Kampai.Util.QuantityItem q3 in qList)
				{
					global::Kampai.Game.IngredientsItemDefinition ingredientsItemDefinition = definitionService.Get<global::Kampai.Game.IngredientsItemDefinition>(q3.ID);
					num += ingredientsItemDefinition.BaseXP * (int)q3.Quantity;
				}
				num = (int)((float)num * blackMarketBoardLevelBandDefinition.XPMultipler);
				break;
			default:
				logger.Log(global::Kampai.Util.Logger.Level.Warning, "Invalid validation type or no value to calculate");
				break;
			}
			return num;
		}

		private bool IncrementIngredientsList(global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem> qList, int maxIngredientsQty)
		{
			int count = qList.Count;
			if (count == 0)
			{
				return true;
			}
			int num = incrementCounter++ % count;
			if (qList[num].Quantity == maxIngredientsQty && num == 0)
			{
				return true;
			}
			qList[num].Quantity++;
			return false;
		}

		private int FindHighestAndMissing(global::System.Collections.Generic.List<int> missingIngredients)
		{
			int[] array = new int[board.tickets.Count];
			int num = 0;
			foreach (global::Kampai.Game.OrderBoardTicket ticket in board.tickets)
			{
				array[num] = ticket.TransactionInst.Inputs[0].ID;
				num++;
			}
			int num2 = 0;
			int num3 = 0;
			global::System.Collections.Generic.Dictionary<int, int> dictionary = new global::System.Collections.Generic.Dictionary<int, int>();
			int[] array2 = array;
			foreach (int num4 in array2)
			{
				if (dictionary.ContainsKey(num4))
				{
					global::System.Collections.Generic.Dictionary<int, int> dictionary3;
					global::System.Collections.Generic.Dictionary<int, int> dictionary2 = (dictionary3 = dictionary);
					int key2;
					int key = (key2 = num4);
					key2 = dictionary3[key2];
					dictionary2[key] = key2 + 1;
					if (dictionary[num4] > num3)
					{
						num3 = dictionary[num4];
						num2 = num4;
					}
				}
				else
				{
					dictionary.Add(num4, 1);
				}
				if (missingIngredients.Contains(num4))
				{
					missingIngredients.Remove(num4);
				}
			}
			if (missingIngredients.Count > 0 || num2 == 0)
			{
				return 0;
			}
			return num2;
		}

		private global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem> GetUniqueIngredients()
		{
			int maxUniqueResources = board.Definition.LevelBands[board.CurrentLevelBandIndex].MaxUniqueResources;
			global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem> list = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
			global::System.Collections.Generic.List<int> list2 = new global::System.Collections.Generic.List<int>();
			global::System.Collections.Generic.IList<global::Kampai.Game.IngredientsItemDefinition> list3 = playerService.FindAllAvailableIngredients();
			global::System.Collections.Generic.IList<global::Kampai.Game.IngredientsItemDefinition> allUnLockedIngredients = playerService.GetAllUnLockedIngredients();
			global::System.Collections.Generic.IList<global::Kampai.Game.IngredientsItemDefinition> list5;
			if (HasOrderThatCantBeFilled())
			{
				global::System.Collections.Generic.IList<global::Kampai.Game.IngredientsItemDefinition> list4 = list3;
				list5 = list4;
			}
			else
			{
				list5 = allUnLockedIngredients;
			}
			global::System.Collections.Generic.IList<global::Kampai.Game.IngredientsItemDefinition> list6 = list5;
			int count = list6.Count;
			for (int i = 0; i < list3.Count; i++)
			{
				list2.Add(list3[i].ID);
			}
			int mostCommon = FindHighestAndMissing(list2);
			for (int j = 0; j < global::System.Math.Min(count, maxUniqueResources); j++)
			{
				int id = ((list2.Count > 0 && list.Count == 0) ? PickFromMissingItems(list2) : ((list.Count != 0) ? PickNextTicketItem(list, allUnLockedIngredients) : PickFirstTicketItem(mostCommon, list6)));
				list.Add(new global::Kampai.Util.QuantityItem(id, 1u));
			}
			return list;
		}

		private bool HasOrderThatCantBeFilled()
		{
			foreach (global::Kampai.Game.OrderBoardTicket ticket in board.tickets)
			{
				foreach (global::Kampai.Util.QuantityItem item in playerService.GetMissingItemListFromTransaction(ticket.TransactionInst.ToDefinition()))
				{
					int buildingDefintionIDFromItemDefintionID = definitionService.GetBuildingDefintionIDFromItemDefintionID(item.ID);
					if (playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.Building>(buildingDefintionIDFromItemDefintionID) == null)
					{
						return true;
					}
				}
			}
			return false;
		}

		private int PickFromMissingItems(global::System.Collections.Generic.List<int> missingIngredients)
		{
			return missingIngredients[RollDice(0, missingIngredients.Count)];
		}

		private int PickFirstTicketItem(int mostCommon, global::System.Collections.Generic.IList<global::Kampai.Game.IngredientsItemDefinition> iidList)
		{
			int iD;
			do
			{
				int index = RollDice(0, iidList.Count);
				iD = iidList[index].ID;
			}
			while (mostCommon == iD && iidList.Count > 1);
			return iD;
		}

		private int PickNextTicketItem(global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem> qiList, global::System.Collections.Generic.IList<global::Kampai.Game.IngredientsItemDefinition> unlockedIngredients)
		{
			for (int i = 0; i < unlockedIngredients.Count; i++)
			{
				foreach (global::Kampai.Util.QuantityItem qi in qiList)
				{
					if (qi.ID == unlockedIngredients[i].ID)
					{
						unlockedIngredients.Remove(unlockedIngredients[i]);
					}
				}
			}
			return unlockedIngredients[RollDice(0, unlockedIngredients.Count)].ID;
		}

		private int RollDice(int minValue, int maxValue)
		{
			return randomService.NextInt(minValue, maxValue);
		}
	}
}
