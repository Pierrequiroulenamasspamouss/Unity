namespace UnityTest
{
	public class MemberResolver
	{
		private object callingObjectRef;

		private global::System.Reflection.MemberInfo[] callstack;

		private global::UnityEngine.GameObject gameObject;

		private string path;

		public MemberResolver(global::UnityEngine.GameObject gameObject, string path)
		{
			path = path.Trim();
			ValidatePath(path);
			this.gameObject = gameObject;
			this.path = path.Trim();
		}

		public object GetValue(bool useCache)
		{
			if (useCache && callingObjectRef != null)
			{
				object valueFromMember = callingObjectRef;
				for (int i = 0; i < callstack.Length; i++)
				{
					valueFromMember = GetValueFromMember(valueFromMember, callstack[i]);
				}
				return valueFromMember;
			}
			object obj = GetBaseObject();
			global::System.Reflection.MemberInfo[] array = GetCallstack();
			callingObjectRef = obj;
			global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> list = new global::System.Collections.Generic.List<global::System.Reflection.MemberInfo>();
			foreach (global::System.Reflection.MemberInfo memberInfo in array)
			{
				obj = GetValueFromMember(obj, memberInfo);
				list.Add(memberInfo);
				if (obj == null)
				{
					return null;
				}
				if (!IsValueType(obj.GetType()))
				{
					list.Clear();
					callingObjectRef = obj;
				}
			}
			callstack = list.ToArray();
			return obj;
		}

		public global::System.Type GetMemberType()
		{
			global::System.Reflection.MemberInfo[] array = GetCallstack();
			if (array.Length == 0)
			{
				return GetBaseObject().GetType();
			}
			global::System.Reflection.MemberInfo memberInfo = array[array.Length - 1];
			if (memberInfo is global::System.Reflection.FieldInfo)
			{
				return (memberInfo as global::System.Reflection.FieldInfo).FieldType;
			}
			if (memberInfo is global::System.Reflection.MethodInfo)
			{
				return (memberInfo as global::System.Reflection.MethodInfo).ReturnType;
			}
			return null;
		}

		public static bool TryGetMemberType(global::UnityEngine.GameObject gameObject, string path, out global::System.Type value)
		{
			try
			{
				global::UnityTest.MemberResolver memberResolver = new global::UnityTest.MemberResolver(gameObject, path);
				value = memberResolver.GetMemberType();
				return true;
			}
			catch (global::UnityTest.InvalidPathException)
			{
				value = null;
				return false;
			}
		}

		public static bool TryGetValue(global::UnityEngine.GameObject gameObject, string path, out object value)
		{
			try
			{
				global::UnityTest.MemberResolver memberResolver = new global::UnityTest.MemberResolver(gameObject, path);
				value = memberResolver.GetValue(false);
				return true;
			}
			catch (global::UnityTest.InvalidPathException)
			{
				value = null;
				return false;
			}
		}

		private object GetValueFromMember(object obj, global::System.Reflection.MemberInfo memberInfo)
		{
			if (memberInfo is global::System.Reflection.FieldInfo)
			{
				return (memberInfo as global::System.Reflection.FieldInfo).GetValue(obj);
			}
			if (memberInfo is global::System.Reflection.MethodInfo)
			{
				return (memberInfo as global::System.Reflection.MethodInfo).Invoke(obj, null);
			}
			throw new global::UnityTest.InvalidPathException(memberInfo.Name);
		}

		private object GetBaseObject()
		{
			if (string.IsNullOrEmpty(path))
			{
				return gameObject;
			}
			string type = path.Split('.')[0];
			global::UnityEngine.Component component = gameObject.GetComponent(type);
			if (component != null)
			{
				return component;
			}
			return gameObject;
		}

		private global::System.Reflection.MemberInfo[] GetCallstack()
		{
			if (path == string.Empty)
			{
				return new global::System.Reflection.MemberInfo[0];
			}
			global::System.Collections.Generic.Queue<string> queue = new global::System.Collections.Generic.Queue<string>(path.Split('.'));
			global::System.Type type = GetBaseObject().GetType();
			if (type != typeof(global::UnityEngine.GameObject))
			{
				queue.Dequeue();
			}
			global::System.Reflection.PropertyInfo propertyInfo = null;
			global::System.Reflection.FieldInfo fieldInfo = null;
			global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> list = new global::System.Collections.Generic.List<global::System.Reflection.MemberInfo>();
			while (queue.Count != 0)
			{
				string text = queue.Dequeue();
				fieldInfo = GetField(type, text);
				if (fieldInfo != null)
				{
					type = fieldInfo.FieldType;
					list.Add(fieldInfo);
					continue;
				}
				propertyInfo = GetProperty(type, text);
				if (propertyInfo != null)
				{
					type = propertyInfo.PropertyType;
					global::System.Reflection.MethodInfo getMethod = GetGetMethod(propertyInfo);
					list.Add(getMethod);
					continue;
				}
				throw new global::UnityTest.InvalidPathException(text);
			}
			return list.ToArray();
		}

		private global::System.Type GetTypeFromMember(global::System.Reflection.MemberInfo memberInfo)
		{
			if (memberInfo is global::System.Reflection.FieldInfo)
			{
				return (memberInfo as global::System.Reflection.FieldInfo).FieldType;
			}
			if (memberInfo is global::System.Reflection.MethodInfo)
			{
				return (memberInfo as global::System.Reflection.MethodInfo).ReturnType;
			}
			throw new global::UnityTest.InvalidPathException(memberInfo.Name);
		}

		private void ValidatePath(string path)
		{
			bool flag = false;
			if (path.StartsWith(".") || path.EndsWith("."))
			{
				flag = true;
			}
			if (path.IndexOf("..") >= 0)
			{
				flag = true;
			}
			if (global::System.Text.RegularExpressions.Regex.IsMatch(path, "\\s"))
			{
				flag = true;
			}
			if (flag)
			{
				throw new global::UnityTest.InvalidPathException(path);
			}
		}

		private static bool IsValueType(global::System.Type type)
		{
			return type.IsValueType;
		}

		private static global::System.Reflection.FieldInfo GetField(global::System.Type type, string fieldName)
		{
			return type.GetField(fieldName);
		}

		private static global::System.Reflection.PropertyInfo GetProperty(global::System.Type type, string propertyName)
		{
			return type.GetProperty(propertyName);
		}

		private static global::System.Reflection.MethodInfo GetGetMethod(global::System.Reflection.PropertyInfo propertyInfo)
		{
			return propertyInfo.GetGetMethod();
		}
	}
}
