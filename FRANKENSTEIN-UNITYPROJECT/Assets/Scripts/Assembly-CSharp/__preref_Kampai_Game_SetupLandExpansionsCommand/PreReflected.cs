namespace __preref_Kampai_Game_SetupLandExpansionsCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.SetupLandExpansionsCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[9]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.SetupLandExpansionsCommand)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CreateInventoryBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.SetupLandExpansionsCommand)target).createInventoryBuildingSignal = (global::Kampai.Game.CreateInventoryBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ILandExpansionService), delegate(object target, object val)
				{
					((global::Kampai.Game.SetupLandExpansionsCommand)target).landExpansionService = (global::Kampai.Game.ILandExpansionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.SetupLandExpansionsCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupForSaleSignsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.SetupLandExpansionsCommand)target).setupForSaleSignsSignal = (global::Kampai.Game.SetupForSaleSignsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupFlowersSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.SetupLandExpansionsCommand)target).setupFlowersSignal = (global::Kampai.Game.SetupFlowersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PurchaseLandExpansionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.SetupLandExpansionsCommand)target).purchaseSignal = (global::Kampai.Game.PurchaseLandExpansionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.SetupLandExpansionsCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.SetupLandExpansionsCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[9];
		}
	}
}
