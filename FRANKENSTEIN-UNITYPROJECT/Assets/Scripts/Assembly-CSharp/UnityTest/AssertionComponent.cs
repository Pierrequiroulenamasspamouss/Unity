namespace UnityTest
{
	[global::System.Serializable]
	public class AssertionComponent : global::UnityEngine.MonoBehaviour, global::UnityTest.AssertionComponentConfigurator
	{
		[global::UnityEngine.SerializeField]
		public float checkAfterTime = 1f;

		[global::UnityEngine.SerializeField]
		public bool repeatCheckTime = true;

		[global::UnityEngine.SerializeField]
		public float repeatEveryTime = 1f;

		[global::UnityEngine.SerializeField]
		public int checkAfterFrames = 1;

		[global::UnityEngine.SerializeField]
		public bool repeatCheckFrame = true;

		[global::UnityEngine.SerializeField]
		public int repeatEveryFrame = 1;

		[global::UnityEngine.SerializeField]
		public bool hasFailed;

		[global::UnityEngine.SerializeField]
		public global::UnityTest.CheckMethod checkMethods = global::UnityTest.CheckMethod.Start;

		[global::UnityEngine.SerializeField]
		private global::UnityTest.ActionBase m_ActionBase;

		[global::UnityEngine.SerializeField]
		public int checksPerformed;

		private int checkOnFrame;

		private string createdInFilePath;

		private int createdInFileLine = -1;

		public global::UnityTest.ActionBase Action
		{
			get
			{
				return m_ActionBase;
			}
			set
			{
				m_ActionBase = value;
				m_ActionBase.go = base.gameObject;
			}
		}

		public int UpdateCheckStartOnFrame
		{
			set
			{
				checkAfterFrames = value;
			}
		}

		public int UpdateCheckRepeatFrequency
		{
			set
			{
				repeatEveryFrame = value;
			}
		}

		public bool UpdateCheckRepeat
		{
			set
			{
				repeatCheckFrame = value;
			}
		}

		public float TimeCheckStartAfter
		{
			set
			{
				checkAfterTime = value;
			}
		}

		public float TimeCheckRepeatFrequency
		{
			set
			{
				repeatEveryTime = value;
			}
		}

		public bool TimeCheckRepeat
		{
			set
			{
				repeatCheckTime = value;
			}
		}

		public global::UnityTest.AssertionComponent Component
		{
			get
			{
				return this;
			}
		}

		public global::UnityEngine.Object GetFailureReferenceObject()
		{
			if (!string.IsNullOrEmpty(createdInFilePath))
			{
				return global::UnityEngine.Resources.LoadAssetAtPath(createdInFilePath, typeof(global::UnityEngine.Object));
			}
			return this;
		}

		public string GetCreationLocation()
		{
			if (!string.IsNullOrEmpty(createdInFilePath))
			{
				int startIndex = createdInFilePath.LastIndexOf("\\") + 1;
				return string.Format("{0}, line {1} ({2})", createdInFilePath.Substring(startIndex), createdInFileLine, createdInFilePath);
			}
			return string.Empty;
		}

		public void Awake()
		{
			if (!global::UnityEngine.Debug.isDebugBuild)
			{
				global::UnityEngine.Object.Destroy(this);
			}
			OnComponentCopy();
		}

		private void OnComponentCopy()
		{
			if (m_ActionBase == null)
			{
				return;
			}
			global::System.Collections.Generic.IEnumerable<global::UnityEngine.Object> source = global::System.Linq.Enumerable.Where(global::UnityEngine.Resources.FindObjectsOfTypeAll(typeof(global::UnityTest.AssertionComponent)), (global::UnityEngine.Object o) => ((global::UnityTest.AssertionComponent)o).m_ActionBase == m_ActionBase && o != this);
			if (global::System.Linq.Enumerable.Any(source))
			{
				if (global::System.Linq.Enumerable.Count(source) > 1)
				{
					global::UnityEngine.Debug.LogWarning("More than one refence to comparer found. This shouldn't happen");
				}
				global::UnityTest.AssertionComponent assertionComponent = global::System.Linq.Enumerable.First(source) as global::UnityTest.AssertionComponent;
				m_ActionBase = assertionComponent.m_ActionBase.CreateCopy(assertionComponent.gameObject, base.gameObject);
			}
		}

		public void Start()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.Start);
			if (IsCheckMethodSelected(global::UnityTest.CheckMethod.AfterPeriodOfTime))
			{
				StartCoroutine("CheckPeriodically");
			}
			if (IsCheckMethodSelected(global::UnityTest.CheckMethod.Update))
			{
				checkOnFrame = global::UnityEngine.Time.frameCount + checkAfterFrames;
			}
		}

		public global::System.Collections.IEnumerator CheckPeriodically()
		{
			yield return new global::UnityEngine.WaitForSeconds(checkAfterTime);
			CheckAssertionFor(global::UnityTest.CheckMethod.AfterPeriodOfTime);
			while (repeatCheckTime)
			{
				yield return new global::UnityEngine.WaitForSeconds(repeatEveryTime);
				CheckAssertionFor(global::UnityTest.CheckMethod.AfterPeriodOfTime);
			}
		}

		public bool ShouldCheckOnFrame()
		{
			if (global::UnityEngine.Time.frameCount > checkOnFrame)
			{
				if (repeatCheckFrame)
				{
					checkOnFrame += repeatEveryFrame;
				}
				else
				{
					checkOnFrame = int.MaxValue;
				}
				return true;
			}
			return false;
		}

		public void OnDisable()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnDisable);
		}

		public void OnEnable()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnEnable);
		}

		public void OnDestroy()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnDestroy);
		}

		public void Update()
		{
			if (IsCheckMethodSelected(global::UnityTest.CheckMethod.Update) && ShouldCheckOnFrame())
			{
				CheckAssertionFor(global::UnityTest.CheckMethod.Update);
			}
		}

		public void FixedUpdate()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.FixedUpdate);
		}

		public void LateUpdate()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.LateUpdate);
		}

		public void OnControllerColliderHit()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnControllerColliderHit);
		}

		public void OnParticleCollision()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnParticleCollision);
		}

		public void OnJointBreak()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnJointBreak);
		}

		public void OnBecameInvisible()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnBecameInvisible);
		}

		public void OnBecameVisible()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnBecameVisible);
		}

		public void OnTriggerEnter()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnTriggerEnter);
		}

		public void OnTriggerExit()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnTriggerExit);
		}

		public void OnTriggerStay()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnTriggerStay);
		}

		public void OnCollisionEnter()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnCollisionEnter);
		}

		public void OnCollisionExit()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnCollisionExit);
		}

		public void OnCollisionStay()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnCollisionStay);
		}

		public void OnTriggerEnter2D()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnTriggerEnter2D);
		}

		public void OnTriggerExit2D()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnTriggerExit2D);
		}

		public void OnTriggerStay2D()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnTriggerStay2D);
		}

		public void OnCollisionEnter2D()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnCollisionEnter2D);
		}

		public void OnCollisionExit2D()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnCollisionExit2D);
		}

		public void OnCollisionStay2D()
		{
			CheckAssertionFor(global::UnityTest.CheckMethod.OnCollisionStay2D);
		}

		private void CheckAssertionFor(global::UnityTest.CheckMethod checkMethod)
		{
			if (IsCheckMethodSelected(checkMethod))
			{
				global::UnityTest.Assertions.CheckAssertions(this);
			}
		}

		public bool IsCheckMethodSelected(global::UnityTest.CheckMethod method)
		{
			return method == (checkMethods & method);
		}

		public static T Create<T>(global::UnityTest.CheckMethod checkOnMethods, global::UnityEngine.GameObject gameObject, string propertyPath) where T : global::UnityTest.ActionBase
		{
			global::UnityTest.AssertionComponentConfigurator configurator;
			return Create<T>(out configurator, checkOnMethods, gameObject, propertyPath);
		}

		public static T Create<T>(out global::UnityTest.AssertionComponentConfigurator configurator, global::UnityTest.CheckMethod checkOnMethods, global::UnityEngine.GameObject gameObject, string propertyPath) where T : global::UnityTest.ActionBase
		{
			return CreateAssertionComponent<T>(out configurator, checkOnMethods, gameObject, propertyPath);
		}

		public static T Create<T>(global::UnityTest.CheckMethod checkOnMethods, global::UnityEngine.GameObject gameObject, string propertyPath, global::UnityEngine.GameObject gameObject2, string propertyPath2) where T : global::UnityTest.ComparerBase
		{
			global::UnityTest.AssertionComponentConfigurator configurator;
			return Create<T>(out configurator, checkOnMethods, gameObject, propertyPath, gameObject2, propertyPath2);
		}

		public static T Create<T>(out global::UnityTest.AssertionComponentConfigurator configurator, global::UnityTest.CheckMethod checkOnMethods, global::UnityEngine.GameObject gameObject, string propertyPath, global::UnityEngine.GameObject gameObject2, string propertyPath2) where T : global::UnityTest.ComparerBase
		{
			T val = CreateAssertionComponent<T>(out configurator, checkOnMethods, gameObject, propertyPath);
			val.compareToType = global::UnityTest.ComparerBase.CompareToType.CompareToObject;
			val.other = gameObject2;
			val.otherPropertyPath = propertyPath2;
			return val;
		}

		public static T Create<T>(global::UnityTest.CheckMethod checkOnMethods, global::UnityEngine.GameObject gameObject, string propertyPath, object constValue) where T : global::UnityTest.ComparerBase
		{
			global::UnityTest.AssertionComponentConfigurator configurator;
			return Create<T>(out configurator, checkOnMethods, gameObject, propertyPath, constValue);
		}

		public static T Create<T>(out global::UnityTest.AssertionComponentConfigurator configurator, global::UnityTest.CheckMethod checkOnMethods, global::UnityEngine.GameObject gameObject, string propertyPath, object constValue) where T : global::UnityTest.ComparerBase
		{
			T val = CreateAssertionComponent<T>(out configurator, checkOnMethods, gameObject, propertyPath);
			if (constValue == null)
			{
				val.compareToType = global::UnityTest.ComparerBase.CompareToType.CompareToNull;
				return val;
			}
			val.compareToType = global::UnityTest.ComparerBase.CompareToType.CompareToConstantValue;
			val.ConstValue = constValue;
			return val;
		}

		private static T CreateAssertionComponent<T>(out global::UnityTest.AssertionComponentConfigurator configurator, global::UnityTest.CheckMethod checkOnMethods, global::UnityEngine.GameObject gameObject, string propertyPath) where T : global::UnityTest.ActionBase
		{
			global::UnityTest.AssertionComponent assertionComponent = gameObject.AddComponent<global::UnityTest.AssertionComponent>();
			assertionComponent.checkMethods = checkOnMethods;
			T result = (T)(assertionComponent.Action = global::UnityEngine.ScriptableObject.CreateInstance<T>());
			assertionComponent.Action.go = gameObject;
			assertionComponent.Action.thisPropertyPath = propertyPath;
			configurator = assertionComponent;
			global::System.Diagnostics.StackTrace stackTrace = new global::System.Diagnostics.StackTrace(true);
			string fileName = stackTrace.GetFrame(0).GetFileName();
			for (int i = 1; i < stackTrace.FrameCount; i++)
			{
				global::System.Diagnostics.StackFrame frame = stackTrace.GetFrame(i);
				if (frame.GetFileName() != fileName)
				{
					string text = frame.GetFileName().Substring(global::UnityEngine.Application.dataPath.Length - "Assets".Length);
					assertionComponent.createdInFilePath = text;
					assertionComponent.createdInFileLine = frame.GetFileLineNumber();
					break;
				}
			}
			return result;
		}
	}
}
