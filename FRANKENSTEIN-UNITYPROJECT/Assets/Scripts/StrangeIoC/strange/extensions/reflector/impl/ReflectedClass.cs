namespace strange.extensions.reflector.impl
{
	public class ReflectedClass : global::strange.extensions.reflector.api.IReflectedClass
	{
		public global::System.Func<object[], object> Constructor { get; set; }

		public global::System.Type[] ConstructorParameters { get; set; }

		public global::System.Action<object>[] PostConstructors { get; set; }

		public global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[] Setters { get; set; }

		public object[] SetterNames { get; set; }

		public bool PreGenerated { get; set; }
	}
}
