namespace UnityTest
{
	public abstract class ComparerBase : global::UnityTest.ActionBase
	{
		public enum CompareToType
		{
			CompareToObject = 0,
			CompareToConstantValue = 1,
			CompareToNull = 2
		}

		public global::UnityTest.ComparerBase.CompareToType compareToType;

		public global::UnityEngine.GameObject other;

		protected object objOtherVal;

		public string otherPropertyPath = string.Empty;

		private global::UnityTest.MemberResolver memberResolverB;

		public virtual object ConstValue { get; set; }

		protected abstract bool Compare(object a, object b);

		protected override bool Compare(object objVal)
		{
			if (compareToType == global::UnityTest.ComparerBase.CompareToType.CompareToConstantValue)
			{
				objOtherVal = ConstValue;
			}
			else if (compareToType == global::UnityTest.ComparerBase.CompareToType.CompareToNull)
			{
				objOtherVal = null;
			}
			else if (other == null)
			{
				objOtherVal = null;
			}
			else
			{
				if (memberResolverB == null)
				{
					memberResolverB = new global::UnityTest.MemberResolver(other, otherPropertyPath);
				}
				objOtherVal = memberResolverB.GetValue(UseCache);
			}
			return Compare(objVal, objOtherVal);
		}

		public virtual global::System.Type[] GetAccepatbleTypesForB()
		{
			return null;
		}

		public virtual object GetDefaultConstValue()
		{
			throw new global::System.NotImplementedException();
		}

		public override string GetFailureMessage()
		{
			string text = GetType().Name + " assertion failed.\n" + go.name + "." + thisPropertyPath + " " + compareToType;
			string text2;
			switch (compareToType)
			{
			case global::UnityTest.ComparerBase.CompareToType.CompareToObject:
				text2 = text;
				text = string.Concat(text2, " (", other, ").", otherPropertyPath, " failed.");
				break;
			case global::UnityTest.ComparerBase.CompareToType.CompareToConstantValue:
				text2 = text;
				text = string.Concat(text2, " ", ConstValue, " failed.");
				break;
			case global::UnityTest.ComparerBase.CompareToType.CompareToNull:
				text += " failed.";
				break;
			}
			text2 = text;
			return string.Concat(text2, " Expected: ", objOtherVal, " Actual: ", objVal);
		}
	}
}
