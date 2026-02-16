namespace strange.extensions.injector.api
{
	public interface IInjectorFactory
	{
		object Get(global::strange.extensions.injector.api.IInjectionBinding binding);

		object Get(global::strange.extensions.injector.api.IInjectionBinding binding, object[] args);
	}
}
