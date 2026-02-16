namespace Kampai.Game.View
{
	public class SetAnimatorAction : global::Kampai.Game.View.KampaiAction
	{
		protected global::Kampai.Game.View.ActionableObject obj;

		private global::UnityEngine.RuntimeAnimatorController controller;

		private global::System.Collections.Generic.Dictionary<string, object> animationParams;

		public SetAnimatorAction(global::Kampai.Game.View.ActionableObject obj, global::UnityEngine.RuntimeAnimatorController controller, global::Kampai.Util.ILogger logger, global::System.Collections.Generic.Dictionary<string, object> animationParams = null)
			: base(logger)
		{
			this.obj = obj;
			this.controller = controller;
			this.animationParams = animationParams;
		}

		public SetAnimatorAction(global::Kampai.Game.View.ActionableObject obj, global::UnityEngine.RuntimeAnimatorController controller, string paramName, global::Kampai.Util.ILogger logger, object paramValue = null)
			: base(logger)
		{
			this.obj = obj;
			this.controller = controller;
			animationParams = new global::System.Collections.Generic.Dictionary<string, object>();
			animationParams.Add(paramName, paramValue);
		}

		public override void Execute()
		{
			if (controller != null)
			{
				obj.SetAnimController(controller);
			}
			if (animationParams != null)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, object> animationParam in animationParams)
				{
					if (animationParam.Value is float || animationParam.Value is double)
					{
						obj.SetAnimFloat(animationParam.Key, (float)animationParam.Value);
					}
					else if (animationParam.Value is int || animationParam.Value is uint || animationParam.Value is long)
					{
						obj.SetAnimInteger(animationParam.Key, global::System.Convert.ToInt32(animationParam.Value));
					}
					else if (animationParam.Value is bool)
					{
						obj.SetAnimBool(animationParam.Key, (bool)animationParam.Value);
					}
					else if (animationParam.Value == null)
					{
						obj.SetAnimTrigger(animationParam.Key);
					}
					else if (animationParam.Value.ToString().Length == 0)
					{
						obj.SetAnimBool(animationParam.Key, true);
					}
					else if (animationParam.Value is string)
					{
						bool result;
						if (bool.TryParse(animationParam.Value.ToString(), out result))
						{
							obj.SetAnimBool(animationParam.Key, result);
							continue;
						}
						logger.Error(string.Concat("Unknown animation argument: ", animationParam.Value, " (type=", animationParam.Value.GetType(), ")"));
					}
					else
					{
						logger.Error(string.Concat("Unknown animation argument: ", animationParam.Value, " (type=", animationParam.Value.GetType(), ")"));
					}
				}
			}
			base.Done = true;
		}

		public override string ToString()
		{
			string arg = "null";
			if (controller != null)
			{
				arg = controller.name;
			}
			string arg2 = "null";
			if (animationParams != null)
			{
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
				foreach (string key in animationParams.Keys)
				{
					stringBuilder.Append(key).Append("=").Append(animationParams[key].ToString())
						.Append(" ");
				}
				arg2 = stringBuilder.ToString();
			}
			return string.Format("{0} - {1} params: {2}", GetType().Name, arg, arg2);
		}
	}
}
