namespace strange.extensions.injector.impl
{
	public class Injector : global::strange.extensions.injector.api.IInjector
	{
		private const int INFINITY_LIMIT = 10;

		private global::System.Collections.Generic.Dictionary<global::strange.extensions.injector.api.IInjectionBinding, int> infinityLock;

		public global::strange.extensions.injector.api.IInjectorFactory factory { get; set; }

		public global::strange.extensions.injector.api.IInjectionBinder binder { get; set; }

		public global::strange.extensions.reflector.api.IReflectionBinder reflector { get; set; }

		public Injector()
		{
			factory = new global::strange.extensions.injector.impl.InjectorFactory();
		}

		public object Instantiate(global::strange.extensions.injector.api.IInjectionBinding binding)
		{
			failIf(binder == null, "Attempt to instantiate from Injector without a Binder", global::strange.extensions.injector.api.InjectionExceptionType.NO_BINDER);
			failIf(factory == null, "Attempt to inject into Injector without a Factory", global::strange.extensions.injector.api.InjectionExceptionType.NO_FACTORY);
			armorAgainstInfiniteLoops(binding);
			object obj = null;
			global::System.Type type = null;
			if (binding.value is global::System.Type)
			{
				type = binding.value as global::System.Type;
			}
			else if (binding.value == null)
			{
				object[] array = binding.key as object[];
				type = array[0] as global::System.Type;
				if (type.IsPrimitive || type == typeof(decimal) || type == typeof(string))
				{
					obj = binding.value;
				}
			}
			else
			{
				obj = binding.value;
			}
			if (obj == null)
			{
				global::strange.extensions.reflector.api.IReflectedClass reflectedClass = reflector.Get(type);
				global::System.Type[] constructorParameters = reflectedClass.ConstructorParameters;
				int num = constructorParameters.Length;
				object[] array2 = new object[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = getValueInjection(constructorParameters[i], null, null);
				}
				obj = factory.Get(binding, array2);
				if (obj != null)
				{
					if (binding.toInject)
					{
						obj = Inject(obj, false);
					}
					if (binding.type == global::strange.extensions.injector.api.InjectionBindingType.SINGLETON || binding.type == global::strange.extensions.injector.api.InjectionBindingType.VALUE)
					{
						binding.ToInject(false);
					}
				}
			}
			infinityLock.Clear();
			return obj;
		}

		public object Inject(object target)
		{
			return Inject(target, true);
		}

		public object Inject(object target, bool attemptConstructorInjection)
		{
			failIf(binder == null, "Attempt to inject into Injector without a Binder", global::strange.extensions.injector.api.InjectionExceptionType.NO_BINDER);
			failIf(reflector == null, "Attempt to inject without a reflector", global::strange.extensions.injector.api.InjectionExceptionType.NO_REFLECTOR);
			failIf(target == null, "Attempt to inject into null instance", global::strange.extensions.injector.api.InjectionExceptionType.NULL_TARGET);
			global::System.Type type = target.GetType();
			if (type.IsPrimitive || type == typeof(decimal) || type == typeof(string))
			{
				return target;
			}
			global::strange.extensions.reflector.api.IReflectedClass reflection = reflector.Get(type);
			if (attemptConstructorInjection)
			{
				target = performConstructorInjection(target, reflection);
			}
			performSetterInjection(target, reflection);
			postInject(target, reflection);
			return target;
		}

		public void Uninject(object target)
		{
			failIf(binder == null, "Attempt to inject into Injector without a Binder", global::strange.extensions.injector.api.InjectionExceptionType.NO_BINDER);
			failIf(reflector == null, "Attempt to inject without a reflector", global::strange.extensions.injector.api.InjectionExceptionType.NO_REFLECTOR);
			failIf(target == null, "Attempt to inject into null instance", global::strange.extensions.injector.api.InjectionExceptionType.NULL_TARGET);
			global::System.Type type = target.GetType();
			if (!type.IsPrimitive && type != typeof(decimal) && type != typeof(string))
			{
				global::strange.extensions.reflector.api.IReflectedClass reflection = reflector.Get(type);
				performUninjection(target, reflection);
			}
		}

		private object performConstructorInjection(object target, global::strange.extensions.reflector.api.IReflectedClass reflection)
		{
			failIf(target == null, "Attempt to perform constructor injection into a null object", global::strange.extensions.injector.api.InjectionExceptionType.NULL_TARGET);
			failIf(reflection == null, "Attempt to perform constructor injection without a reflection", global::strange.extensions.injector.api.InjectionExceptionType.NULL_REFLECTION);
			global::System.Func<object[], object> constructor = reflection.Constructor;
			failIf(constructor == null, "Attempt to construction inject a null constructor", global::strange.extensions.injector.api.InjectionExceptionType.NULL_CONSTRUCTOR);
			global::System.Type[] constructorParameters = reflection.ConstructorParameters;
			object[] array = new object[constructorParameters.Length];
			int num = 0;
			global::System.Type[] array2 = constructorParameters;
			foreach (global::System.Type t in array2)
			{
				array[num] = getValueInjection(t, null, target);
				num++;
			}
			if (array.Length == 0)
			{
				return target;
			}
			object obj = constructor(array);
			if (obj != null)
			{
				return obj;
			}
			return target;
		}

		private void performSetterInjection(object target, global::strange.extensions.reflector.api.IReflectedClass reflection)
		{
			failIf(target == null, "Attempt to inject into a null object", global::strange.extensions.injector.api.InjectionExceptionType.NULL_TARGET);
			failIf(reflection == null, "Attempt to inject without a reflection", global::strange.extensions.injector.api.InjectionExceptionType.NULL_REFLECTION);
			failIf(reflection.Setters.Length != reflection.SetterNames.Length, "Attempt to perform setter injection with mismatched names.\nThere must be exactly as many names as setters.", global::strange.extensions.injector.api.InjectionExceptionType.SETTER_NAME_MISMATCH);
			int num = reflection.Setters.Length;
			for (int i = 0; i < num; i++)
			{
				global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>> keyValuePair = reflection.Setters[i];
				object valueInjection = getValueInjection(keyValuePair.Key, reflection.SetterNames[i], target);
				injectValueIntoPoint(valueInjection, target, keyValuePair.Value);
			}
		}

		private object getValueInjection(global::System.Type t, object name, object target)
		{
			global::strange.extensions.injector.api.IInjectionBinding binding = binder.GetBinding(t, name);
			failIf(binding == null, "Attempt to Instantiate a null binding.", global::strange.extensions.injector.api.InjectionExceptionType.NULL_BINDING, t, name, target);
			if (binding.type == global::strange.extensions.injector.api.InjectionBindingType.VALUE)
			{
				if (!binding.toInject)
				{
					return binding.value;
				}
				object result = Inject(binding.value, false);
				binding.ToInject(false);
				return result;
			}
			if (binding.type == global::strange.extensions.injector.api.InjectionBindingType.SINGLETON)
			{
				if (binding.value is global::System.Type || binding.value == null)
				{
					Instantiate(binding);
				}
				return binding.value;
			}
			return Instantiate(binding);
		}

		private void injectValueIntoPoint(object value, object target, global::System.Action<object, object> setter)
		{
			failIf(target == null, "Attempt to inject into a null target", global::strange.extensions.injector.api.InjectionExceptionType.NULL_TARGET);
			failIf(setter == null, "Attempt to inject into a null point", global::strange.extensions.injector.api.InjectionExceptionType.NULL_INJECTION_POINT);
			failIf(value == null, "Attempt to inject null into a target object", global::strange.extensions.injector.api.InjectionExceptionType.NULL_VALUE_INJECTION);
			setter(target, value);
		}

		private void postInject(object target, global::strange.extensions.reflector.api.IReflectedClass reflection)
		{
			failIf(target == null, "Attempt to PostConstruct a null target", global::strange.extensions.injector.api.InjectionExceptionType.NULL_TARGET);
			failIf(reflection == null, "Attempt to PostConstruct without a reflection", global::strange.extensions.injector.api.InjectionExceptionType.NULL_REFLECTION);
			global::System.Action<object>[] postConstructors = reflection.PostConstructors;
			if (postConstructors != null)
			{
				global::System.Action<object>[] array = postConstructors;
				foreach (global::System.Action<object> action in array)
				{
					action(target);
				}
			}
		}

		private void performUninjection(object target, global::strange.extensions.reflector.api.IReflectedClass reflection)
		{
			int num = reflection.Setters.Length;
			for (int i = 0; i < num; i++)
			{
				global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>> keyValuePair = reflection.Setters[i];
				keyValuePair.Value(target, null);
			}
		}

		private void failIf(bool condition, string message, global::strange.extensions.injector.api.InjectionExceptionType type)
		{
			failIf(condition, message, type, null, null, null);
		}

		private void failIf(bool condition, string message, global::strange.extensions.injector.api.InjectionExceptionType type, global::System.Type t, object name)
		{
			failIf(condition, message, type, t, name, null);
		}

		private void failIf(bool condition, string message, global::strange.extensions.injector.api.InjectionExceptionType type, global::System.Type t, object name, object target)
		{
			if (condition)
			{
				message = message + "\n\t\ttarget: " + target;
				message = message + "\n\t\ttype: " + t;
				message = message + "\n\t\tname: " + name;
				throw new global::strange.extensions.injector.impl.InjectionException(message, type);
			}
		}

		private void armorAgainstInfiniteLoops(global::strange.extensions.injector.api.IInjectionBinding binding)
		{
			if (binding != null)
			{
				if (infinityLock == null)
				{
					infinityLock = new global::System.Collections.Generic.Dictionary<global::strange.extensions.injector.api.IInjectionBinding, int>();
				}
				if (!infinityLock.ContainsKey(binding))
				{
					infinityLock.Add(binding, 0);
				}
				infinityLock[binding] += 1;
				if (infinityLock[binding] > 10)
				{
					throw new global::strange.extensions.injector.impl.InjectionException("There appears to be a circular dependency. Terminating loop.", global::strange.extensions.injector.api.InjectionExceptionType.CIRCULAR_DEPENDENCY);
				}
			}
		}
	}
}
