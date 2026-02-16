namespace strange.extensions.injector.impl
{
	public class InjectorFactory : global::strange.extensions.injector.api.IInjectorFactory
	{
		public object Get(global::strange.extensions.injector.api.IInjectionBinding binding, object[] args)
		{
			if (binding == null)
			{
				throw new global::strange.extensions.injector.impl.InjectionException("InjectorFactory cannot act on null binding", global::strange.extensions.injector.api.InjectionExceptionType.NULL_BINDING);
			}
			switch (binding.type)
			{
			case global::strange.extensions.injector.api.InjectionBindingType.SINGLETON:
				return singletonOf(binding, args);
			case global::strange.extensions.injector.api.InjectionBindingType.VALUE:
				return valueOf(binding);
			default:
				return instanceOf(binding, args);
			}
		}

		public object Get(global::strange.extensions.injector.api.IInjectionBinding binding)
		{
			return Get(binding, null);
		}

		protected object singletonOf(global::strange.extensions.injector.api.IInjectionBinding binding, object[] args)
		{
			if (binding.value != null)
			{
				if (binding.value.GetType().IsInstanceOfType(typeof(global::System.Type)))
				{
					object obj = createFromValue(binding.value, args);
					if (obj == null)
					{
						return null;
					}
					binding.SetValue(obj);
				}
			}
			else
			{
				binding.SetValue(generateImplicit((binding.key as object[])[0], args));
			}
			return binding.value;
		}

		protected object generateImplicit(object key, object[] args)
		{
			global::System.Type type = key as global::System.Type;
			if (!type.IsInterface && !type.IsAbstract)
			{
				return createFromValue(key, args);
			}
			throw new global::strange.extensions.injector.impl.InjectionException("InjectorFactory can't instantiate an Interface or Abstract Class. Class: " + key.ToString(), global::strange.extensions.injector.api.InjectionExceptionType.NOT_INSTANTIABLE);
		}

		protected object valueOf(global::strange.extensions.injector.api.IInjectionBinding binding)
		{
			return binding.value;
		}

		protected object instanceOf(global::strange.extensions.injector.api.IInjectionBinding binding, object[] args)
		{
			if (binding.value != null)
			{
				return createFromValue(binding.value, args);
			}
			object o = generateImplicit((binding.key as object[])[0], args);
			return createFromValue(o, args);
		}

		protected object createFromValue(object o, object[] args)
		{
			global::System.Type type = ((o is global::System.Type) ? (o as global::System.Type) : o.GetType());
			object result = null;
			try
			{
				global::strange.extensions.reflector.api.IReflectedClass reflectedClass = global::strange.extensions.reflector.api.PreReflectedTypesDatabase.Instance.GetClass(type);
				result = ((reflectedClass != null) ? reflectedClass.Constructor(args) : ((args != null && args.Length != 0) ? global::System.Activator.CreateInstance(type, args) : global::System.Activator.CreateInstance(type)));
			}
			catch (global::System.Exception ex)
			{
				global::UnityEngine.Debug.LogError(ex.ToString());
			}
			return result;
		}
	}
}
