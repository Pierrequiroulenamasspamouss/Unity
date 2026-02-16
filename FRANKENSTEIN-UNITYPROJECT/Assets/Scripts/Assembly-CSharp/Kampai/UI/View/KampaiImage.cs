namespace Kampai.UI.View
{
	public class KampaiImage : global::UnityEngine.UI.Image
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Sprite m_maskSprite;

		private static readonly global::UnityEngine.Vector2[] s_VertScratch = new global::UnityEngine.Vector2[4];

		private static readonly global::UnityEngine.Vector2[] s_Uv = new global::UnityEngine.Vector2[4];

		private static readonly global::UnityEngine.Vector2[] s_Xy = new global::UnityEngine.Vector2[4];

		private static readonly global::UnityEngine.Vector2[] s_UVScratch = new global::UnityEngine.Vector2[4];

		private static readonly global::UnityEngine.Vector2[] s_MaskUVScratch = new global::UnityEngine.Vector2[4];

		private static readonly global::UnityEngine.Vector2[] s_MaskUv = new global::UnityEngine.Vector2[4];

		private global::UnityEngine.Material myMaterial;

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override global::UnityEngine.Material materialForRendering
		{
			get
			{
				if (myMaterial == null)
				{
					myMaterial = new global::UnityEngine.Material(material);
				}
				return myMaterial;
			}
		}

		public global::UnityEngine.Sprite maskSprite
		{
			get
			{
				return m_maskSprite;
			}
			set
			{
				if (m_maskSprite != value)
				{
					m_maskSprite = value;
					SetAllDirty();
				}
			}
		}

		protected override void OnFillVBO(global::System.Collections.Generic.List<global::UnityEngine.UIVertex> vbo)
		{
			if (base.overrideSprite == null)
			{
				base.OnFillVBO(vbo);
				return;
			}
			switch (base.type)
			{
			case global::UnityEngine.UI.Image.Type.Simple:
				GenerateSimpleSprite(vbo, base.preserveAspect);
				break;
			case global::UnityEngine.UI.Image.Type.Sliced:
				GenerateSlicedSprite(vbo);
				break;
			case global::UnityEngine.UI.Image.Type.Tiled:
				logger.Error("Tiled image type is not supported on KampaiImage");
				break;
			case global::UnityEngine.UI.Image.Type.Filled:
				GenerateFilledSprite(vbo, base.preserveAspect);
				break;
			}
		}

		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();
			if (IsActive() && materialForRendering != null && m_maskSprite != null)
			{
				materialForRendering.SetTexture("_AlphaTex", m_maskSprite.texture);
			}
		}

		private void GenerateSimpleSprite(global::System.Collections.Generic.IList<global::UnityEngine.UIVertex> vbo, bool preserveAspect)
		{
			global::UnityEngine.UIVertex simpleVert = global::UnityEngine.UIVertex.simpleVert;
			simpleVert.color = base.color;
			global::UnityEngine.Vector4 drawingDimensions = GetDrawingDimensions(preserveAspect);
			global::UnityEngine.Vector4 vector = ((base.overrideSprite != null) ? global::UnityEngine.Sprites.DataUtility.GetOuterUV(base.overrideSprite) : global::UnityEngine.Vector4.zero);
			global::UnityEngine.Vector4 vector2 = ((!(m_maskSprite == null)) ? global::UnityEngine.Sprites.DataUtility.GetOuterUV(m_maskSprite) : global::UnityEngine.Vector4.zero);
			simpleVert.position = new global::UnityEngine.Vector3(drawingDimensions.x, drawingDimensions.y);
			simpleVert.uv0 = new global::UnityEngine.Vector2(vector.x, vector.y);
			simpleVert.uv1 = new global::UnityEngine.Vector2(vector2.x, vector2.y);
			vbo.Add(simpleVert);
			simpleVert.position = new global::UnityEngine.Vector3(drawingDimensions.x, drawingDimensions.w);
			simpleVert.uv0 = new global::UnityEngine.Vector2(vector.x, vector.w);
			simpleVert.uv1 = new global::UnityEngine.Vector2(vector2.x, vector2.w);
			vbo.Add(simpleVert);
			simpleVert.position = new global::UnityEngine.Vector3(drawingDimensions.z, drawingDimensions.w);
			simpleVert.uv0 = new global::UnityEngine.Vector2(vector.z, vector.w);
			simpleVert.uv1 = new global::UnityEngine.Vector2(vector2.z, vector2.w);
			vbo.Add(simpleVert);
			simpleVert.position = new global::UnityEngine.Vector3(drawingDimensions.z, drawingDimensions.y);
			simpleVert.uv0 = new global::UnityEngine.Vector2(vector.z, vector.y);
			simpleVert.uv1 = new global::UnityEngine.Vector2(vector2.z, vector2.y);
			vbo.Add(simpleVert);
		}

		private void GenerateFilledSprite(global::System.Collections.Generic.IList<global::UnityEngine.UIVertex> vbo, bool preserveAspect)
		{
			if (base.fillAmount < 0.001f)
			{
				return;
			}
			global::UnityEngine.Vector4 drawingDimensions = GetDrawingDimensions(preserveAspect);
			global::UnityEngine.Vector4 vector = ((base.overrideSprite != null) ? global::UnityEngine.Sprites.DataUtility.GetOuterUV(base.overrideSprite) : global::UnityEngine.Vector4.zero);
			global::UnityEngine.Vector4 vector2 = ((m_maskSprite != null) ? global::UnityEngine.Sprites.DataUtility.GetOuterUV(m_maskSprite) : global::UnityEngine.Vector4.zero);
			global::UnityEngine.UIVertex simpleVert = global::UnityEngine.UIVertex.simpleVert;
			simpleVert.color = base.color;
			float x = vector.x;
			float y = vector.y;
			float z = vector.z;
			float w = vector.w;
			float x2 = vector2.x;
			float y2 = vector2.y;
			float z2 = vector2.z;
			float w2 = vector2.w;
			ProcessHorizontalOrVerticleFill(x, y, z, w, x2, y2, z2, w2, drawingDimensions);
			if (base.fillAmount < 1f)
			{
				if (base.fillMethod == global::UnityEngine.UI.Image.FillMethod.Radial90)
				{
					ProcessRadialFill_90(vbo, simpleVert);
					return;
				}
				if (base.fillMethod == global::UnityEngine.UI.Image.FillMethod.Radial180)
				{
					ProcessRadialFill_180(vbo, simpleVert, x, y, z, w, x2, y2, z2, w2, drawingDimensions);
					return;
				}
				if (base.fillMethod == global::UnityEngine.UI.Image.FillMethod.Radial360)
				{
					ProcessRadialFill_360(vbo, simpleVert, x, y, z, w, x2, y2, z2, w2, drawingDimensions);
					return;
				}
			}
			SetupVBO(vbo, simpleVert);
		}

		private void ProcessHorizontalOrVerticleFill(float num, float num2, float num3, float num4, float maskNum, float maskNum2, float maskNum3, float maskNum4, global::UnityEngine.Vector4 drawingDimensions)
		{
			if (base.fillMethod == global::UnityEngine.UI.Image.FillMethod.Horizontal || base.fillMethod == global::UnityEngine.UI.Image.FillMethod.Vertical)
			{
				if (base.fillMethod == global::UnityEngine.UI.Image.FillMethod.Horizontal)
				{
					float num5 = (num3 - num) * base.fillAmount;
					float num6 = (maskNum3 - maskNum) * base.fillAmount;
					if (base.fillOrigin == 1)
					{
						drawingDimensions.x = drawingDimensions.z - (drawingDimensions.z - drawingDimensions.x) * base.fillAmount;
						num = num3 - num5;
						maskNum = maskNum3 - num6;
					}
					else
					{
						drawingDimensions.z = drawingDimensions.x + (drawingDimensions.z - drawingDimensions.x) * base.fillAmount;
						num3 = num + num5;
						maskNum3 = maskNum + num6;
					}
				}
				else if (base.fillMethod == global::UnityEngine.UI.Image.FillMethod.Vertical)
				{
					float num7 = (num4 - num2) * base.fillAmount;
					float num8 = (maskNum4 - maskNum2) * base.fillAmount;
					if (base.fillOrigin == 1)
					{
						drawingDimensions.y = drawingDimensions.w - (drawingDimensions.w - drawingDimensions.y) * base.fillAmount;
						num2 = num4 - num7;
						maskNum2 = maskNum4 - num8;
					}
					else
					{
						drawingDimensions.w = drawingDimensions.y + (drawingDimensions.w - drawingDimensions.y) * base.fillAmount;
						num4 = num2 + num7;
						maskNum4 = maskNum2 + num8;
					}
				}
			}
			s_Xy[0] = new global::UnityEngine.Vector2(drawingDimensions.x, drawingDimensions.y);
			s_Xy[1] = new global::UnityEngine.Vector2(drawingDimensions.x, drawingDimensions.w);
			s_Xy[2] = new global::UnityEngine.Vector2(drawingDimensions.z, drawingDimensions.w);
			s_Xy[3] = new global::UnityEngine.Vector2(drawingDimensions.z, drawingDimensions.y);
			s_Uv[0] = new global::UnityEngine.Vector2(num, num2);
			s_Uv[1] = new global::UnityEngine.Vector2(num, num4);
			s_Uv[2] = new global::UnityEngine.Vector2(num3, num4);
			s_Uv[3] = new global::UnityEngine.Vector2(num3, num2);
			s_MaskUv[0] = new global::UnityEngine.Vector2(maskNum, maskNum2);
			s_MaskUv[1] = new global::UnityEngine.Vector2(maskNum, maskNum4);
			s_MaskUv[2] = new global::UnityEngine.Vector2(maskNum3, maskNum4);
			s_MaskUv[3] = new global::UnityEngine.Vector2(maskNum3, maskNum2);
		}

		private void ProcessRadialFill_90(global::System.Collections.Generic.IList<global::UnityEngine.UIVertex> vbo, global::UnityEngine.UIVertex simpleVert)
		{
			if (RadialCut(s_Xy, s_Uv, s_MaskUv, base.fillAmount, base.fillClockwise, base.fillOrigin))
			{
				SetupVBO(vbo, simpleVert);
			}
		}

		private void ProcessRadialFill_180(global::System.Collections.Generic.IList<global::UnityEngine.UIVertex> vbo, global::UnityEngine.UIVertex simpleVert, float num, float num2, float num3, float num4, float maskNum, float maskNum2, float maskNum3, float maskNum4, global::UnityEngine.Vector4 drawingDimensions)
		{
			for (int i = 0; i < 2; i++)
			{
				int num5 = ((base.fillOrigin > 1) ? 1 : 0);
				float custom;
				float custom2;
				float custom3;
				float custom4;
				if (base.fillOrigin == 0 || base.fillOrigin == 2)
				{
					custom = 0f;
					custom2 = 1f;
					if (i == num5)
					{
						custom3 = 0f;
						custom4 = 0.5f;
					}
					else
					{
						custom3 = 0.5f;
						custom4 = 1f;
					}
				}
				else
				{
					custom3 = 0f;
					custom4 = 1f;
					if (i == num5)
					{
						custom = 0.5f;
						custom2 = 1f;
					}
					else
					{
						custom = 0f;
						custom2 = 0.5f;
					}
				}
				SetupKampaiImageProperties(num, num2, num3, num4, maskNum, maskNum2, maskNum3, maskNum4, drawingDimensions, custom3, custom4, custom, custom2);
				float value = (base.fillClockwise ? (base.fillAmount * 2f - (float)i) : (base.fillAmount * 2f - (float)(1 - i)));
				if (RadialCut(s_Xy, s_Uv, s_MaskUv, global::UnityEngine.Mathf.Clamp01(value), base.fillClockwise, (i + base.fillOrigin + 3) % 4))
				{
					SetupVBO(vbo, simpleVert);
				}
			}
		}

		private void ProcessRadialFill_360(global::System.Collections.Generic.IList<global::UnityEngine.UIVertex> vbo, global::UnityEngine.UIVertex simpleVert, float num, float num2, float num3, float num4, float maskNum, float maskNum2, float maskNum3, float maskNum4, global::UnityEngine.Vector4 drawingDimensions)
		{
			for (int i = 0; i < 4; i++)
			{
				float custom;
				float custom2;
				if (i < 2)
				{
					custom = 0f;
					custom2 = 0.5f;
				}
				else
				{
					custom = 0.5f;
					custom2 = 1f;
				}
				float custom3;
				float custom4;
				if (i == 0 || i == 3)
				{
					custom3 = 0f;
					custom4 = 0.5f;
				}
				else
				{
					custom3 = 0.5f;
					custom4 = 1f;
				}
				SetupKampaiImageProperties(num, num2, num3, num4, maskNum, maskNum2, maskNum3, maskNum4, drawingDimensions, custom, custom2, custom3, custom4);
				float value = (base.fillClockwise ? (base.fillAmount * 4f - (float)((i + base.fillOrigin) % 4)) : (base.fillAmount * 4f - (float)(3 - (i + base.fillOrigin) % 4)));
				if (RadialCut(s_Xy, s_Uv, s_MaskUv, global::UnityEngine.Mathf.Clamp01(value), base.fillClockwise, (i + 2) % 4))
				{
					SetupVBO(vbo, simpleVert);
				}
			}
		}

		private void SetupKampaiImageProperties(float num, float num2, float num3, float num4, float maskNum, float maskNum2, float maskNum3, float maskNum4, global::UnityEngine.Vector4 drawingDimensions, float custom1, float custom2, float custom3, float custom4)
		{
			s_Xy[0].x = global::UnityEngine.Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, custom1);
			s_Xy[1].x = s_Xy[0].x;
			s_Xy[2].x = global::UnityEngine.Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, custom2);
			s_Xy[3].x = s_Xy[2].x;
			s_Xy[0].y = global::UnityEngine.Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, custom3);
			s_Xy[1].y = global::UnityEngine.Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, custom4);
			s_Xy[2].y = s_Xy[1].y;
			s_Xy[3].y = s_Xy[0].y;
			s_Uv[0].x = global::UnityEngine.Mathf.Lerp(num, num3, custom1);
			s_Uv[1].x = s_Uv[0].x;
			s_Uv[2].x = global::UnityEngine.Mathf.Lerp(num, num3, custom2);
			s_Uv[3].x = s_Uv[2].x;
			s_Uv[0].y = global::UnityEngine.Mathf.Lerp(num2, num4, custom3);
			s_Uv[1].y = global::UnityEngine.Mathf.Lerp(num2, num4, custom4);
			s_Uv[2].y = s_Uv[1].y;
			s_Uv[3].y = s_Uv[0].y;
			s_MaskUv[0].x = global::UnityEngine.Mathf.Lerp(maskNum, maskNum3, custom1);
			s_MaskUv[1].x = s_MaskUv[0].x;
			s_MaskUv[2].x = global::UnityEngine.Mathf.Lerp(maskNum, maskNum3, custom2);
			s_MaskUv[3].x = s_MaskUv[2].x;
			s_MaskUv[0].y = global::UnityEngine.Mathf.Lerp(maskNum2, maskNum4, custom3);
			s_MaskUv[1].y = global::UnityEngine.Mathf.Lerp(maskNum2, maskNum4, custom4);
			s_MaskUv[2].y = s_MaskUv[1].y;
			s_MaskUv[3].y = s_MaskUv[0].y;
		}

		private void SetupVBO(global::System.Collections.Generic.IList<global::UnityEngine.UIVertex> vbo, global::UnityEngine.UIVertex simpleVert)
		{
			for (int i = 0; i < 4; i++)
			{
				simpleVert.position = s_Xy[i];
				simpleVert.uv0 = s_Uv[i];
				simpleVert.uv1 = s_MaskUv[i];
				vbo.Add(simpleVert);
			}
		}

		private void GenerateSlicedSprite(global::System.Collections.Generic.IList<global::UnityEngine.UIVertex> vbo)
		{
			if (!base.hasBorder)
			{
				GenerateSimpleSprite(vbo, false);
				return;
			}
			global::UnityEngine.Vector4 vector;
			global::UnityEngine.Vector4 vector2;
			global::UnityEngine.Vector4 vector3;
			global::UnityEngine.Vector4 vector4;
			if (base.overrideSprite != null)
			{
				vector = global::UnityEngine.Sprites.DataUtility.GetOuterUV(base.overrideSprite);
				vector2 = global::UnityEngine.Sprites.DataUtility.GetInnerUV(base.overrideSprite);
				vector3 = global::UnityEngine.Sprites.DataUtility.GetPadding(base.overrideSprite);
				vector4 = base.overrideSprite.border;
			}
			else
			{
				vector = global::UnityEngine.Vector4.zero;
				vector2 = global::UnityEngine.Vector4.zero;
				vector3 = global::UnityEngine.Vector4.zero;
				vector4 = global::UnityEngine.Vector4.zero;
			}
			global::UnityEngine.Vector4 vector5;
			global::UnityEngine.Vector4 vector6;
			if (m_maskSprite != null)
			{
				vector5 = global::UnityEngine.Sprites.DataUtility.GetOuterUV(m_maskSprite);
				vector6 = global::UnityEngine.Sprites.DataUtility.GetInnerUV(m_maskSprite);
			}
			else
			{
				vector5 = global::UnityEngine.Vector4.zero;
				vector6 = global::UnityEngine.Vector4.zero;
			}
			global::UnityEngine.Rect pixelAdjustedRect = GetPixelAdjustedRect();
			vector4 = GetAdjustedBorders(vector4 / base.pixelsPerUnit, pixelAdjustedRect);
			s_VertScratch[0] = new global::UnityEngine.Vector2(vector3.x, vector3.y);
			s_VertScratch[3] = new global::UnityEngine.Vector2(pixelAdjustedRect.width - vector3.z, pixelAdjustedRect.height - vector3.w);
			s_VertScratch[1].x = vector4.x;
			s_VertScratch[1].y = vector4.y;
			s_VertScratch[2].x = pixelAdjustedRect.width - vector4.z;
			s_VertScratch[2].y = pixelAdjustedRect.height - vector4.w;
			for (int i = 0; i < 4; i++)
			{
				global::UnityEngine.Vector2[] array = s_VertScratch;
				int num = i;
				array[num].x = array[num].x + pixelAdjustedRect.x;
				global::UnityEngine.Vector2[] array2 = s_VertScratch;
				int num2 = i;
				array2[num2].y = array2[num2].y + pixelAdjustedRect.y;
			}
			s_UVScratch[0] = new global::UnityEngine.Vector2(vector.x, vector.y);
			s_UVScratch[1] = new global::UnityEngine.Vector2(vector2.x, vector2.y);
			s_UVScratch[2] = new global::UnityEngine.Vector2(vector2.z, vector2.w);
			s_UVScratch[3] = new global::UnityEngine.Vector2(vector.z, vector.w);
			s_MaskUVScratch[0] = new global::UnityEngine.Vector2(vector5.x, vector5.y);
			s_MaskUVScratch[1] = new global::UnityEngine.Vector2(vector6.x, vector6.y);
			s_MaskUVScratch[2] = new global::UnityEngine.Vector2(vector6.z, vector6.w);
			s_MaskUVScratch[3] = new global::UnityEngine.Vector2(vector5.z, vector5.w);
			global::UnityEngine.UIVertex simpleVert = global::UnityEngine.UIVertex.simpleVert;
			simpleVert.color = base.color;
			for (int j = 0; j < 3; j++)
			{
				int num3 = j + 1;
				for (int k = 0; k < 3; k++)
				{
					if (base.fillCenter || j != 1 || k != 1)
					{
						int num4 = k + 1;
						AddQuad(vbo, simpleVert, new global::UnityEngine.Vector2(s_VertScratch[j].x, s_VertScratch[k].y), new global::UnityEngine.Vector2(s_VertScratch[num3].x, s_VertScratch[num4].y), new global::UnityEngine.Vector2(s_UVScratch[j].x, s_UVScratch[k].y), new global::UnityEngine.Vector2(s_UVScratch[num3].x, s_UVScratch[num4].y), new global::UnityEngine.Vector2(s_MaskUVScratch[j].x, s_MaskUVScratch[k].y), new global::UnityEngine.Vector2(s_MaskUVScratch[num3].x, s_MaskUVScratch[num4].y));
					}
				}
			}
		}

		private global::UnityEngine.Vector4 GetAdjustedBorders(global::UnityEngine.Vector4 border, global::UnityEngine.Rect rect)
		{
			for (int i = 0; i <= 1; i++)
			{
				float num = border[i] + border[i + 2];
				if (rect.size[i] < num && num != 0f)
				{
					float num2 = rect.size[i] / num;
					int index2;
					int index = (index2 = i);
					float num3 = border[index2];
					border[index] = num3 * num2;
					int index3 = (index2 = i + 2);
					num3 = border[index2];
					border[index3] = num3 * num2;
				}
			}
			return border;
		}

		private void AddQuad(global::System.Collections.Generic.IList<global::UnityEngine.UIVertex> vbo, global::UnityEngine.UIVertex v, global::UnityEngine.Vector2 posMin, global::UnityEngine.Vector2 posMax, global::UnityEngine.Vector2 uvMin, global::UnityEngine.Vector2 uvMax, global::UnityEngine.Vector2 uvMaskMin, global::UnityEngine.Vector2 uvMaskMax)
		{
			v.position = new global::UnityEngine.Vector3(posMin.x, posMin.y, 0f);
			v.uv0 = new global::UnityEngine.Vector2(uvMin.x, uvMin.y);
			v.uv1 = new global::UnityEngine.Vector2(uvMaskMin.x, uvMaskMin.y);
			vbo.Add(v);
			v.position = new global::UnityEngine.Vector3(posMin.x, posMax.y, 0f);
			v.uv0 = new global::UnityEngine.Vector2(uvMin.x, uvMax.y);
			v.uv1 = new global::UnityEngine.Vector2(uvMaskMin.x, uvMaskMax.y);
			vbo.Add(v);
			v.position = new global::UnityEngine.Vector3(posMax.x, posMax.y, 0f);
			v.uv0 = new global::UnityEngine.Vector2(uvMax.x, uvMax.y);
			v.uv1 = new global::UnityEngine.Vector2(uvMaskMax.x, uvMaskMax.y);
			vbo.Add(v);
			v.position = new global::UnityEngine.Vector3(posMax.x, posMin.y, 0f);
			v.uv0 = new global::UnityEngine.Vector2(uvMax.x, uvMin.y);
			v.uv1 = new global::UnityEngine.Vector2(uvMaskMax.x, uvMaskMin.y);
			vbo.Add(v);
		}

		private global::UnityEngine.Vector4 GetDrawingDimensions(bool shouldPreserveAspect)
		{
			global::UnityEngine.Vector4 vector = ((base.overrideSprite == null) ? global::UnityEngine.Vector4.zero : global::UnityEngine.Sprites.DataUtility.GetPadding(base.overrideSprite));
			global::UnityEngine.Vector2 vector2 = ((base.overrideSprite == null) ? global::UnityEngine.Vector2.zero : new global::UnityEngine.Vector2(base.overrideSprite.rect.width, base.overrideSprite.rect.height));
			global::UnityEngine.Rect pixelAdjustedRect = GetPixelAdjustedRect();
			int num = global::UnityEngine.Mathf.RoundToInt(vector2.x);
			int num2 = global::UnityEngine.Mathf.RoundToInt(vector2.y);
			global::UnityEngine.Vector4 vector3 = new global::UnityEngine.Vector4(vector.x / (float)num, vector.y / (float)num2, ((float)num - vector.z) / (float)num, ((float)num2 - vector.w) / (float)num2);
			if (shouldPreserveAspect && vector2.sqrMagnitude > 0f)
			{
				float num3 = vector2.x / vector2.y;
				float num4 = pixelAdjustedRect.width / pixelAdjustedRect.height;
				if (num3 > num4)
				{
					float height = pixelAdjustedRect.height;
					pixelAdjustedRect.height = pixelAdjustedRect.width * (1f / num3);
					pixelAdjustedRect.y += (height - pixelAdjustedRect.height) * base.rectTransform.pivot.y;
				}
				else
				{
					float width = pixelAdjustedRect.width;
					pixelAdjustedRect.width = pixelAdjustedRect.height * num3;
					pixelAdjustedRect.x += (width - pixelAdjustedRect.width) * base.rectTransform.pivot.x;
				}
			}
			return new global::UnityEngine.Vector4(pixelAdjustedRect.x + pixelAdjustedRect.width * vector3.x, pixelAdjustedRect.y + pixelAdjustedRect.height * vector3.y, pixelAdjustedRect.x + pixelAdjustedRect.width * vector3.z, pixelAdjustedRect.y + pixelAdjustedRect.height * vector3.w);
		}

		private static void RadialCut(global::UnityEngine.Vector2[] xy, float cos, float sin, bool invert, int corner)
		{
			int num = (corner + 1) % 4;
			int num2 = (corner + 2) % 4;
			int num3 = (corner + 3) % 4;
			if ((corner & 1) == 1)
			{
				if (sin > cos)
				{
					cos /= sin;
					sin = 1f;
					if (invert)
					{
						xy[num].x = global::UnityEngine.Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
						xy[num2].x = xy[num].x;
					}
				}
				else if (cos > sin)
				{
					sin /= cos;
					cos = 1f;
					if (!invert)
					{
						xy[num2].y = global::UnityEngine.Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
						xy[num3].y = xy[num2].y;
					}
				}
				else
				{
					cos = 1f;
					sin = 1f;
				}
				if (!invert)
				{
					xy[num3].x = global::UnityEngine.Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
				}
				else
				{
					xy[num].y = global::UnityEngine.Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
				}
				return;
			}
			if (cos > sin)
			{
				sin /= cos;
				cos = 1f;
				if (!invert)
				{
					xy[num].y = global::UnityEngine.Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
					xy[num2].y = xy[num].y;
				}
			}
			else if (sin > cos)
			{
				cos /= sin;
				sin = 1f;
				if (invert)
				{
					xy[num2].x = global::UnityEngine.Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
					xy[num3].x = xy[num2].x;
				}
			}
			else
			{
				cos = 1f;
				sin = 1f;
			}
			if (invert)
			{
				xy[num3].y = global::UnityEngine.Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
			}
			else
			{
				xy[num].x = global::UnityEngine.Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
			}
		}

		private static bool RadialCut(global::UnityEngine.Vector2[] xy, global::UnityEngine.Vector2[] uv, global::UnityEngine.Vector2[] maskUV, float fill, bool invert, int corner)
		{
			if (fill < 0.001f)
			{
				return false;
			}
			if ((corner & 1) == 1)
			{
				invert = !invert;
			}
			if (!invert && fill > 0.999f)
			{
				return true;
			}
			float num = global::UnityEngine.Mathf.Clamp01(fill);
			if (invert)
			{
				num = 1f - num;
			}
			num *= (float)global::System.Math.PI / 2f;
			float cos = global::UnityEngine.Mathf.Cos(num);
			float sin = global::UnityEngine.Mathf.Sin(num);
			RadialCut(xy, cos, sin, invert, corner);
			RadialCut(uv, cos, sin, invert, corner);
			RadialCut(maskUV, cos, sin, invert, corner);
			return true;
		}
	}
}
