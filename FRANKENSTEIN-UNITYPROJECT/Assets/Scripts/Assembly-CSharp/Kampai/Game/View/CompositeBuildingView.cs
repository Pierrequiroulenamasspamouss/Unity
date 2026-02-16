namespace Kampai.Game.View
{
	public class CompositeBuildingView : global::strange.extensions.mediation.impl.View
	{
		public global::System.Collections.Generic.List<global::UnityEngine.Transform> SpawnPoints;

		public global::UnityEngine.Transform Placement01VFXPrefab;

		public global::UnityEngine.Transform Placement02VFXPrefab;

		public global::UnityEngine.Transform ShuffleVFXPrefab;

		public float WaitBetweenJumps = 0.5f;

		public float ShuffleTweenTimeTop = 0.75f;

		public float ShuffleTweenTimeNotTopUp = 0.3f;

		public float ShuffleTweenTimeNotTopDown = 0.3f;

		public float HangTime = 0.2f;

		public float FallInShuffleOffsetTime = 0.1f;

		public float BeforeCapChangeTimeNotTop = 0.0125f;

		public global::UnityEngine.Vector3 MidShuffleOffsetTop = new global::UnityEngine.Vector3(-1.732f, 0f, 1f);

		public global::UnityEngine.Vector3 MidShuffleOffsetNotTop = new global::UnityEngine.Vector3(0f, 4f, 0f);

		public float PlacementVFXSpawnTime = 0.4f;

		public float SecondsToWaitBeforeNewPieceFallsIn = 2.2f;

		private global::System.Collections.Generic.IList<global::Kampai.Game.View.CompositeBuildingPieceObject> pieceObjects = new global::System.Collections.Generic.List<global::Kampai.Game.View.CompositeBuildingPieceObject>();

		public void SetupPieces(global::System.Collections.Generic.IList<global::Kampai.Game.CompositeBuildingPiece> compositePieces)
		{
			for (int i = 0; i < compositePieces.Count; i++)
			{
				AddPiece(compositePieces[i]);
			}
			RefreshColliderSize();
			RefreshPieceAppearance();
		}

		public void AddNewlyCreatedPiece(global::Kampai.Game.CompositeBuildingPiece piece)
		{
			StartCoroutine(WaitThenAddNewPiece(piece));
		}

		private global::System.Collections.IEnumerator WaitThenAddNewPiece(global::Kampai.Game.CompositeBuildingPiece piece)
		{
			yield return new global::UnityEngine.WaitForSeconds(SecondsToWaitBeforeNewPieceFallsIn);
			global::Kampai.Game.View.CompositeBuildingPieceObject newPiece = AddPiece(piece);
			newPiece.PlayFallInAnimation();
			RefreshColliderSize();
			yield return new global::UnityEngine.WaitForSeconds(PlacementVFXSpawnTime);
			RefreshPieceAppearance();
			global::UnityEngine.Transform vfxInstanceP = (global::UnityEngine.Transform)global::UnityEngine.Object.Instantiate(Placement01VFXPrefab);
			vfxInstanceP.SetParent(newPiece.transform, false);
			vfxInstanceP.transform.localPosition = new global::UnityEngine.Vector3(0f, 0.1f, 0f);
			global::UnityEngine.Transform vfxInstanceP2 = (global::UnityEngine.Transform)global::UnityEngine.Object.Instantiate(Placement02VFXPrefab);
			vfxInstanceP2.SetParent(newPiece.transform, false);
			vfxInstanceP2.transform.localPosition = new global::UnityEngine.Vector3(0f, 0.1f, 0f);
			global::UnityEngine.Transform vfxInstanceP3 = (global::UnityEngine.Transform)global::UnityEngine.Object.Instantiate(ShuffleVFXPrefab);
			vfxInstanceP3.SetParent(newPiece.transform, false);
			vfxInstanceP3.transform.localPosition = new global::UnityEngine.Vector3(0f, 0.1f, 0f);
		}

		private global::Kampai.Game.View.CompositeBuildingPieceObject AddPiece(global::Kampai.Game.CompositeBuildingPiece piece)
		{
			global::UnityEngine.GameObject original = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.GameObject>(piece.Definition.PrefabName);
			global::UnityEngine.GameObject gameObject = (global::UnityEngine.GameObject)global::UnityEngine.Object.Instantiate(original);
			global::Kampai.Game.View.CompositeBuildingPieceObject component = gameObject.GetComponent<global::Kampai.Game.View.CompositeBuildingPieceObject>();
			global::UnityEngine.Transform transform = component.transform;
			transform.SetParent(base.transform, false);
			transform.localPosition = SpawnPoints[pieceObjects.Count].localPosition;
			transform.localRotation = SpawnPoints[pieceObjects.Count].localRotation;
			component.PieceID = piece.ID;
			pieceObjects.Add(component);
			return component;
		}

		private void RefreshColliderSize()
		{
			float num = 0f;
			for (int i = 0; i < pieceObjects.Count; i++)
			{
				num = global::UnityEngine.Mathf.Max(num, pieceObjects[i].GetMaxBounds().y);
			}
			num -= base.transform.position.y;
			global::UnityEngine.BoxCollider component = GetComponent<global::UnityEngine.BoxCollider>();
			if (component != null)
			{
				component.size = new global::UnityEngine.Vector3(component.size.x, num, component.size.z);
				component.center = new global::UnityEngine.Vector3(component.center.x, num / 2f, component.center.z);
			}
		}

		private void RefreshPieceAppearance()
		{
			int count = pieceObjects.Count;
			for (int i = 0; i < count; i++)
			{
				bool isOnTop = count > 1 && i == count - 1;
				bool allPiecesCollected = count == SpawnPoints.Count;
				pieceObjects[i].RefreshAppearance(isOnTop, allPiecesCollected);
			}
		}

		public int GetNumPieces()
		{
			return pieceObjects.Count;
		}

		public void PlayShuffleSequence(global::System.Collections.Generic.IList<int> newPieceOrder)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.View.CompositeBuildingPieceObject> list = new global::System.Collections.Generic.List<global::Kampai.Game.View.CompositeBuildingPieceObject>();
			for (int i = 0; i < newPieceOrder.Count; i++)
			{
				list.Add(getPieceObjectByID(newPieceOrder[i]));
			}
			pieceObjects = list;
			StartCoroutine(ShufflePiecesStaggered());
		}

		private global::Kampai.Game.View.CompositeBuildingPieceObject getPieceObjectByID(int pieceID)
		{
			for (int i = 0; i < pieceObjects.Count; i++)
			{
				if (pieceObjects[i].PieceID == pieceID)
				{
					return pieceObjects[i];
				}
			}
			return null;
		}

		private global::System.Collections.IEnumerator ShufflePiecesStaggered()
		{
			global::UnityEngine.Vector3 squashMove = new global::UnityEngine.Vector3(0f, 0f, 0f);
			float squashAmtUp = 0.38f;
			float squashAmtDown = 0.35f;
			global::Kampai.Game.View.CompositeBuildingPieceObject topPiece = pieceObjects[0];
			topPiece.PlayFallInShuffleTopAnimation();
			TweenTopPieceTo(topPiece, SpawnPoints[0].position, MidShuffleOffsetTop);
			yield return new global::UnityEngine.WaitForSeconds(WaitBetweenJumps);
			for (int i = pieceObjects.Count - 1; i > 0; i--)
			{
				global::Kampai.Game.View.CompositeBuildingPieceObject piece = pieceObjects[i];
				float squashAmtCurrent = (float)(i - 1) * squashAmtUp;
				squashMove = new global::UnityEngine.Vector3(0f, 0f - squashAmtCurrent, 0f);
				piece.PlayJumpAnimation();
				TweenNotTopPieceToUp(piece, MidShuffleOffsetNotTop, squashMove);
			}
			global::UnityEngine.Transform vfxInstance2 = (global::UnityEngine.Transform)global::UnityEngine.Object.Instantiate(ShuffleVFXPrefab);
			vfxInstance2.SetParent(topPiece.transform, false);
			vfxInstance2.transform.localPosition = new global::UnityEngine.Vector3(0f, 0.1f, 0f);
			topPiece.RefreshAppearance(false, true);
			yield return new global::UnityEngine.WaitForSeconds(HangTime);
			for (int j = 1; j < pieceObjects.Count; j++)
			{
				global::Kampai.Game.View.CompositeBuildingPieceObject piece2 = pieceObjects[j];
				float squashAmtCurrent = ((j != 1) ? squashAmtDown : 0f);
				squashMove = new global::UnityEngine.Vector3(0f, 0f - squashAmtCurrent, 0f);
				yield return new global::UnityEngine.WaitForSeconds(FallInShuffleOffsetTime);
				piece2.PlayFallInShuffleNotTopAnimation();
				TweenNotTopPieceToDown(piece2, SpawnPoints[j].position, squashMove);
			}
			yield return new global::UnityEngine.WaitForSeconds(BeforeCapChangeTimeNotTop);
			RefreshPieceAppearance();
			yield return new global::UnityEngine.WaitForSeconds(0.25f);
			global::UnityEngine.Transform vfxInstance3 = (global::UnityEngine.Transform)global::UnityEngine.Object.Instantiate(ShuffleVFXPrefab);
			vfxInstance3.SetParent(pieceObjects[pieceObjects.Count - 1].transform, false);
			vfxInstance3.transform.localPosition = new global::UnityEngine.Vector3(0f, 0.1f, 0f);
		}

		private void TweenTopPieceTo(global::Kampai.Game.View.CompositeBuildingPieceObject piece, global::UnityEngine.Vector3 targetPosition, global::UnityEngine.Vector3 midTweenOffset)
		{
			global::UnityEngine.Transform pieceTransform = piece.transform;
			global::UnityEngine.Vector3 pieceOrigin = pieceTransform.position;
			global::UnityEngine.Vector3 AnticAmt = new global::UnityEngine.Vector3(0.3464f, 0f, -0.2f);
			Go.to(pieceTransform, ShuffleTweenTimeTop * 0.1f, new GoTweenConfig().setEaseType(GoEaseType.SineIn).position(pieceOrigin + AnticAmt).onComplete(delegate(AbstractGoTween thisTweenA)
			{
				thisTweenA.destroy();
				Go.to(pieceTransform, ShuffleTweenTimeTop * 0.15f, new GoTweenConfig().setEaseType(GoEaseType.SineInOut).position(pieceOrigin + midTweenOffset).onComplete(delegate(AbstractGoTween thisTween)
				{
					thisTween.destroy();
					Go.to(pieceTransform, ShuffleTweenTimeTop * 1E-05f, new GoTweenConfig().setEaseType(GoEaseType.Linear).position(pieceOrigin + midTweenOffset).onComplete(delegate(AbstractGoTween abstractGoTween)
					{
						abstractGoTween.destroy();
						Go.to(pieceTransform, ShuffleTweenTimeTop * 0.35f, new GoTweenConfig().setEaseType(GoEaseType.SineIn).position(targetPosition + midTweenOffset).onComplete(delegate(AbstractGoTween abstractGoTween2)
						{
							abstractGoTween2.destroy();
							Go.to(pieceTransform, ShuffleTweenTimeTop * 0.225f, new GoTweenConfig().setEaseType(GoEaseType.Linear).position(targetPosition + midTweenOffset).onComplete(delegate(AbstractGoTween abstractGoTween3)
							{
								abstractGoTween3.destroy();
								Go.to(pieceTransform, ShuffleTweenTimeTop * 0.125f, new GoTweenConfig().setEaseType(GoEaseType.SineInOut).position(targetPosition + AnticAmt).onComplete(delegate(AbstractGoTween thisTweenF)
								{
									thisTweenF.destroy();
									Go.to(pieceTransform, ShuffleTweenTimeTop * 0.15f, new GoTweenConfig().setEaseType(GoEaseType.SineOut).position(targetPosition));
								}));
							}));
						}));
					}));
				}));
			}));
		}

		private void TweenNotTopPieceToUp(global::Kampai.Game.View.CompositeBuildingPieceObject piece, global::UnityEngine.Vector3 midTweenOffset, global::UnityEngine.Vector3 squashOffset)
		{
			global::UnityEngine.Transform pieceTransform = piece.transform;
			global::UnityEngine.Vector3 pieceOrigin = pieceTransform.position;
			Go.to(pieceTransform, ShuffleTweenTimeNotTopUp * 0.3f, new GoTweenConfig().setEaseType(GoEaseType.SineIn).position(pieceTransform.position + squashOffset).onComplete(delegate(AbstractGoTween thisTween)
			{
				thisTween.destroy();
				Go.to(pieceTransform, ShuffleTweenTimeNotTopUp * 0.7f, new GoTweenConfig().setEaseType(GoEaseType.SineOut).position(pieceOrigin + midTweenOffset));
			}));
		}

		private void TweenNotTopPieceToDown(global::Kampai.Game.View.CompositeBuildingPieceObject piece, global::UnityEngine.Vector3 targetPosition, global::UnityEngine.Vector3 squashOffset)
		{
			global::UnityEngine.Vector3 bounceAmt = new global::UnityEngine.Vector3(0f, 0.2f, 0f);
			global::UnityEngine.Transform pieceTransform = piece.transform;
			Go.to(pieceTransform, ShuffleTweenTimeNotTopDown * 0.7f, new GoTweenConfig().setEaseType(GoEaseType.SineIn).position(targetPosition + squashOffset).onComplete(delegate(AbstractGoTween thisTween)
			{
				thisTween.destroy();
				Go.to(pieceTransform, ShuffleTweenTimeNotTopDown * 0.2f, new GoTweenConfig().setEaseType(GoEaseType.SineOut).position(targetPosition + bounceAmt).onComplete(delegate(AbstractGoTween abstractGoTween)
				{
					abstractGoTween.destroy();
					Go.to(pieceTransform, ShuffleTweenTimeNotTopDown * 0.1f, new GoTweenConfig().setEaseType(GoEaseType.SineIn).position(targetPosition));
				}));
			}));
		}
	}
}
