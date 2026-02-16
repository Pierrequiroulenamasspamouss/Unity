namespace __preref_Kampai_UI_View_SpawnDooberCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.SpawnDooberCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[18]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).uiCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).hud = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(ILocalPersistanceService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).localPersistence = (ILocalPersistanceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.PeekHUDSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).peekHUDSignal = (global::Kampai.UI.View.PeekHUDSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).glassCanvas = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).UICamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetXPSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).setXPSignal = (global::Kampai.UI.View.SetXPSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetGrindCurrencySignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).setGrindCurrencySignal = (global::Kampai.UI.View.SetGrindCurrencySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetPremiumCurrencySignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).setPremiumCurrencySignal = (global::Kampai.UI.View.SetPremiumCurrencySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetStorageCapacitySignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).setStorageSignal = (global::Kampai.UI.View.SetStorageCapacitySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.FireXPVFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).fireXpSignal = (global::Kampai.UI.View.FireXPVFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.FireGrindVFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).fireGrindSignal = (global::Kampai.UI.View.FireGrindVFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.FirePremiumVFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).firePremiumSignal = (global::Kampai.UI.View.FirePremiumVFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SpawnDooberModel), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).dooberModel = (global::Kampai.UI.View.SpawnDooberModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).soundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.DoobersFlownSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SpawnDooberCommand)target).doobersFlownSignal = (global::Kampai.UI.View.DoobersFlownSignal)val;
				})
			};
			SetterNames = new object[18]
			{
				null,
				global::Kampai.Game.GameElement.CONTEXT,
				global::Kampai.UI.View.UIElement.CAMERA,
				global::Kampai.UI.View.UIElement.HUD,
				null,
				null,
				global::Kampai.Main.MainElement.UI_GLASSCANVAS,
				global::Kampai.UI.View.UIElement.CAMERA,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null
			};
		}
	}
}
