namespace UnityTest
{
	public class CallTesting : global::UnityEngine.MonoBehaviour
	{
		public enum Functions
		{
			CallAfterSeconds = 0,
			CallAfterFrames = 1,
			Start = 2,
			Update = 3,
			FixedUpdate = 4,
			LateUpdate = 5,
			OnDestroy = 6,
			OnEnable = 7,
			OnDisable = 8,
			OnControllerColliderHit = 9,
			OnParticleCollision = 10,
			OnJointBreak = 11,
			OnBecameInvisible = 12,
			OnBecameVisible = 13,
			OnTriggerEnter = 14,
			OnTriggerExit = 15,
			OnTriggerStay = 16,
			OnCollisionEnter = 17,
			OnCollisionExit = 18,
			OnCollisionStay = 19,
			OnTriggerEnter2D = 20,
			OnTriggerExit2D = 21,
			OnTriggerStay2D = 22,
			OnCollisionEnter2D = 23,
			OnCollisionExit2D = 24,
			OnCollisionStay2D = 25
		}

		public enum Method
		{
			Pass = 0,
			Fail = 1
		}

		public int afterFrames;

		public float afterSeconds;

		public global::UnityTest.CallTesting.Functions callOnMethod = global::UnityTest.CallTesting.Functions.Start;

		public global::UnityTest.CallTesting.Method methodToCall;

		private int startFrame;

		private float startTime;

		private void TryToCallTesting(global::UnityTest.CallTesting.Functions invokingMethod)
		{
			if (invokingMethod == callOnMethod)
			{
				if (methodToCall == global::UnityTest.CallTesting.Method.Pass)
				{
					IntegrationTest.Pass(base.gameObject);
				}
				else
				{
					IntegrationTest.Fail(base.gameObject);
				}
				afterFrames = 0;
				afterSeconds = 0f;
				startTime = float.PositiveInfinity;
				startFrame = int.MinValue;
			}
		}

		public void Start()
		{
			startTime = global::UnityEngine.Time.time;
			startFrame = afterFrames;
			TryToCallTesting(global::UnityTest.CallTesting.Functions.Start);
		}

		public void Update()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.Update);
			CallAfterSeconds();
			CallAfterFrames();
		}

		private void CallAfterFrames()
		{
			if (afterFrames > 0 && startFrame + afterFrames <= global::UnityEngine.Time.frameCount)
			{
				TryToCallTesting(global::UnityTest.CallTesting.Functions.CallAfterFrames);
			}
		}

		private void CallAfterSeconds()
		{
			if (startTime + afterSeconds <= global::UnityEngine.Time.time)
			{
				TryToCallTesting(global::UnityTest.CallTesting.Functions.CallAfterSeconds);
			}
		}

		public void OnDisable()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnDisable);
		}

		public void OnEnable()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnEnable);
		}

		public void OnDestroy()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnDestroy);
		}

		public void FixedUpdate()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.FixedUpdate);
		}

		public void LateUpdate()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.LateUpdate);
		}

		public void OnControllerColliderHit()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnControllerColliderHit);
		}

		public void OnParticleCollision()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnParticleCollision);
		}

		public void OnJointBreak()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnJointBreak);
		}

		public void OnBecameInvisible()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnBecameInvisible);
		}

		public void OnBecameVisible()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnBecameVisible);
		}

		public void OnTriggerEnter()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnTriggerEnter);
		}

		public void OnTriggerExit()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnTriggerExit);
		}

		public void OnTriggerStay()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnTriggerStay);
		}

		public void OnCollisionEnter()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnCollisionEnter);
		}

		public void OnCollisionExit()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnCollisionExit);
		}

		public void OnCollisionStay()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnCollisionStay);
		}

		public void OnTriggerEnter2D()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnTriggerEnter2D);
		}

		public void OnTriggerExit2D()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnTriggerExit2D);
		}

		public void OnTriggerStay2D()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnTriggerStay2D);
		}

		public void OnCollisionEnter2D()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnCollisionEnter2D);
		}

		public void OnCollisionExit2D()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnCollisionExit2D);
		}

		public void OnCollisionStay2D()
		{
			TryToCallTesting(global::UnityTest.CallTesting.Functions.OnCollisionStay2D);
		}
	}
}
