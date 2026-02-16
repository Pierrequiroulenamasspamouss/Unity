namespace strange.extensions.reflector.api
{
	public interface IReflectedClass
	{
		global::System.Func<object[], object> Constructor { get; set; }

		global::System.Type[] ConstructorParameters { get; set; }

		global::System.Action<object>[] PostConstructors { get; set; }

		global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[] Setters { get; set; }

		object[] SetterNames { get; set; }

		bool PreGenerated { get; set; }
	}
}
