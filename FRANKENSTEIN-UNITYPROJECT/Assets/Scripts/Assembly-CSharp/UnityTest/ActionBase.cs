namespace UnityTest
{
	public abstract class ActionBase : global::UnityEngine.ScriptableObject
	{
		public global::UnityEngine.GameObject go;

		protected object objVal;

		private global::UnityTest.MemberResolver memberResolver;

		public string thisPropertyPath = string.Empty;

		protected virtual bool UseCache
		{
			get
			{
				return false;
			}
		}

		public virtual global::System.Type[] GetAccepatbleTypesForA()
		{
			return null;
		}

		public virtual int GetDepthOfSearch()
		{
			return 2;
		}

		public virtual string[] GetExcludedFieldNames()
		{
			return new string[0];
		}

		public bool Compare()
		{
			if (memberResolver == null)
			{
				memberResolver = new global::UnityTest.MemberResolver(go, thisPropertyPath);
			}
			objVal = memberResolver.GetValue(UseCache);
			return Compare(objVal);
		}

		protected abstract bool Compare(object objVal);

		public virtual global::System.Type GetParameterType()
		{
			return typeof(object);
		}

		public virtual string GetConfigurationDescription()
		{
			string text = string.Empty;
			foreach (global::System.Reflection.FieldInfo item in global::System.Linq.Enumerable.Where(GetType().GetFields(global::System.Reflection.BindingFlags.DeclaredOnly | global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public), (global::System.Reflection.FieldInfo info) => info.FieldType.IsSerializable))
			{
				object obj = item.GetValue(this);
				if (obj is double)
				{
					obj = ((double)obj).ToString("0.########");
				}
				if (obj is float)
				{
					obj = ((float)obj).ToString("0.########");
				}
				text = string.Concat(text, obj, " ");
			}
			return text;
		}

		private global::System.Collections.Generic.IEnumerable<global::System.Reflection.FieldInfo> GetFields(global::System.Type type)
		{
			return type.GetFields(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);
		}

		public global::UnityTest.ActionBase CreateCopy(global::UnityEngine.GameObject oldGameObject, global::UnityEngine.GameObject newGameObject)
		{
			global::UnityTest.ActionBase actionBase = global::UnityEngine.ScriptableObject.CreateInstance(GetType()) as global::UnityTest.ActionBase;
			global::System.Collections.Generic.IEnumerable<global::System.Reflection.FieldInfo> fields = GetFields(GetType());
			foreach (global::System.Reflection.FieldInfo item in fields)
			{
				object obj = item.GetValue(this);
				if (obj is global::UnityEngine.GameObject && obj as global::UnityEngine.GameObject == oldGameObject)
				{
					obj = newGameObject;
				}
				item.SetValue(actionBase, obj);
			}
			return actionBase;
		}

		public virtual void Fail(global::UnityTest.AssertionComponent assertion)
		{
			global::UnityEngine.Debug.LogException(new global::UnityTest.AssertionException(assertion), assertion.GetFailureReferenceObject());
		}

		public virtual string GetFailureMessage()
		{
			return string.Concat(GetType().Name, " assertion failed.\n(", go, ").", thisPropertyPath, " failed. Value: ", objVal);
		}
	}
}
