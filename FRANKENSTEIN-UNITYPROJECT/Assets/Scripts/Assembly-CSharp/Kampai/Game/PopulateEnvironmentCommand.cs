namespace Kampai.Game
{
	public class PopulateEnvironmentCommand : global::strange.extensions.command.impl.Command
	{
		private global::UnityEngine.GameObject parent;

		private global::UnityEngine.Color darkGreen = new global::UnityEngine.Color(0f, 0.5f, 0f, 1f);

		private global::UnityEngine.Color darkBlue = new global::UnityEngine.Color(0f, 0f, 0.5f, 1f);

		private global::UnityEngine.Color lightBlue = new global::UnityEngine.Color(0f, 1f, 1f, 1f);

		private global::UnityEngine.Color brown = new global::UnityEngine.Color(0.5f, 0.4f, 0.3f, 1f);

		private global::UnityEngine.Color green = new global::UnityEngine.Color(0f, 1f, 0f, 1f);

		private global::UnityEngine.Color red = new global::UnityEngine.Color(1f, 0f, 0f, 1f);

		private global::UnityEngine.Color blue = new global::UnityEngine.Color(0f, 0f, 1f, 1f);

		private global::UnityEngine.Color locked = new global::UnityEngine.Color(1f, 1f, 0f, 1f);

		private global::UnityEngine.Color sidewalk = new global::UnityEngine.Color(0.8f, 0f, 1f, 1f);

		private global::UnityEngine.Color occupied = new global::UnityEngine.Color(0.6f, 0f, 0f, 1f);

		private global::UnityEngine.Color error = new global::UnityEngine.Color(1f, 0f, 0.8f, 1f);

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.Environment environment { get; set; }

		[Inject]
		public global::Kampai.Game.EnvironmentBuilder environmentBuilder { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalMusicSignal musicSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DebugUpdateGridSignal DebugUpdateGridSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public bool display { get; set; }

		[Inject]
		public global::Kampai.Game.EnvironmentState state { get; set; }

		[Inject]
		public global::Kampai.Util.PathFinder pathFinder { get; set; }

		public override void Execute()
		{
			logger.EventStart("PopulateEnvironmentCommand.Execute");
			if (!state.EnvironmentBuilt)
			{
				global::System.Collections.Generic.IList<string> environemtDefinition = definitionService.GetEnvironemtDefinition();
				definitionService.ReclaimEnfironmentDefinitions();
				global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
				dictionary.Add("x", environemtDefinition[0].Length);
				dictionary.Add("y", environemtDefinition.Count);
				global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
				foreach (string item in environemtDefinition)
				{
					for (int i = 0; i < item.Length; i++)
					{
						int result = 0;
						if (int.TryParse(item[i].ToString(), out result))
						{
							list.Add(result);
						}
					}
				}
				dictionary.Add("definitionLayout", list);
				environmentBuilder.Build(dictionary, definitionService.GetPartyDefinition());
				global::System.Collections.Generic.Dictionary<string, float> type = new global::System.Collections.Generic.Dictionary<string, float>();
				musicSignal.Dispatch("Play_backGroundMusic_01", type);
				state.EnvironmentBuilt = true;
				pathFinder.SetEnvironment(environment);
			}
			else
			{
				logger.Debug("PopulateEnvironmentCommand: Environment already built.");
			}
			if (display)
			{
				logger.Debug("PopulateEnvironmentCommand: Setting up grid and textures.");
				SetupGrid();
				UpdateTextures();
				DebugUpdateGridSignal.AddListener(UpdateTextures);
				state.DisplayOn = true;
			}
			else if (state.DisplayOn)
			{
				RemoveGrid();
				DebugUpdateGridSignal.RemoveListener(UpdateTextures);
				state.DisplayOn = false;
			}
			logger.EventStop("PopulateEnvironmentCommand.Execute");
		}

		private void SetupGrid()
		{
			if (!state.GridConstructed)
			{
				logger.EventStart("PopulateEnvironmentCommand.SetupGrid");
				global::UnityEngine.Shader shader = global::UnityEngine.Shader.Find("Kampai/Standard/Texture");
				if (shader == null)
				{
					shader = global::UnityEngine.Shader.Find("Diffuse");
				}
				parent = global::UnityEngine.GameObject.CreatePrimitive(global::UnityEngine.PrimitiveType.Quad);
				parent.name = "Environment";
				global::UnityEngine.Object.Destroy(parent.collider);
				parent.transform.localScale = new global::UnityEngine.Vector3(250f, 250f, 0f);
				parent.transform.Rotate(new global::UnityEngine.Vector3(90f, 0f, 0f));
				global::UnityEngine.Renderer renderer = parent.renderer;
				renderer.castShadows = false;
				renderer.receiveShadows = false;
				renderer.material.shader = shader;
				renderer.material.EnableKeyword("ALPHA_MASK");
				int length = environment.Definition.DefinitionGrid.GetLength(0);
				int length2 = environment.Definition.DefinitionGrid.GetLength(1);
				parent.transform.position = new global::UnityEngine.Vector3((float)length / 2f - 0.5f, 0f, (float)length2 / 2f - 0.5f);
				global::UnityEngine.Texture2D texture2D = new global::UnityEngine.Texture2D(length, length2, global::UnityEngine.TextureFormat.ARGB32, false);
				texture2D.filterMode = global::UnityEngine.FilterMode.Point;
				texture2D.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
				renderer.material.mainTexture = texture2D;
				state.GridConstructed = true;
				logger.EventStop("PopulateEnvironmentCommand.SetupGrid");
			}
			else
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.Find("Environment");
				if (gameObject != null)
				{
					gameObject.GetComponent<global::UnityEngine.MeshRenderer>().enabled = true;
				}
			}
		}

		private void RemoveGrid()
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.Find("Environment");
			if (gameObject != null)
			{
				gameObject.GetComponent<global::UnityEngine.MeshRenderer>().enabled = false;
			}
		}

		private void UpdateTextures()
		{
			if (parent == null)
			{
				return;
			}
			logger.EventStart("PopulateEnvironmentCommand.UpdateTextures");
			int length = environment.Definition.DefinitionGrid.GetLength(0);
			int length2 = environment.Definition.DefinitionGrid.GetLength(1);
			global::Kampai.Game.EnvironmentGridSquareDefinition[,] definitionGrid = environment.Definition.DefinitionGrid;
			global::Kampai.Game.EnvironmentGridSquare[,] playerGrid = environment.PlayerGrid;
			global::UnityEngine.Texture2D texture2D = parent.renderer.material.mainTexture as global::UnityEngine.Texture2D;
			for (int i = 0; i < length2; i++)
			{
				for (int j = 0; j < length; j++)
				{
					if (definitionGrid[j, i].Water)
					{
						if (definitionGrid[j, i].Usable)
						{
							if (definitionGrid[j, i].Pathable)
							{
								texture2D.SetPixel(j, i, blue);
							}
							else
							{
								texture2D.SetPixel(j, i, lightBlue);
							}
						}
						else if (definitionGrid[j, i].Pathable)
						{
							texture2D.SetPixel(j, i, red);
						}
						else
						{
							texture2D.SetPixel(j, i, darkBlue);
						}
					}
					else if (!definitionGrid[j, i].Usable && !playerGrid[j, i].Occupied)
					{
						if (definitionGrid[j, i].Pathable)
						{
							texture2D.SetPixel(j, i, brown);
						}
						else
						{
							texture2D.SetPixel(j, i, darkGreen);
						}
					}
					else if (!playerGrid[j, i].Unlocked)
					{
						if (playerGrid[j, i].Walkable)
						{
							texture2D.SetPixel(j, i, sidewalk);
						}
						else
						{
							texture2D.SetPixel(j, i, locked);
						}
					}
					else if (playerGrid[j, i].Occupied && playerGrid[j, i].Walkable)
					{
						texture2D.SetPixel(j, i, sidewalk);
					}
					else if (playerGrid[j, i].Occupied)
					{
						texture2D.SetPixel(j, i, occupied);
					}
					else if (!playerGrid[j, i].Occupied)
					{
						texture2D.SetPixel(j, i, green);
					}
					else
					{
						texture2D.SetPixel(j, i, error);
					}
				}
			}
			texture2D.Apply();
			parent.renderer.material.mainTexture = texture2D;
			logger.EventStop("PopulateEnvironmentCommand.UpdateTextures");
		}
	}
}
