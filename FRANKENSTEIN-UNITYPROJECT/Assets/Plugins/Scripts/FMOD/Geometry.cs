namespace FMOD
{
	public class Geometry : global::FMOD.HandleBase
	{
		public Geometry(global::System.IntPtr raw)
			: base(raw)
		{
		}

		public global::FMOD.RESULT release()
		{
			global::FMOD.RESULT rESULT = FMOD5_Geometry_Release(getRaw());
			if (rESULT == global::FMOD.RESULT.OK)
			{
				rawPtr = global::System.IntPtr.Zero;
			}
			return rESULT;
		}

		public global::FMOD.RESULT addPolygon(float directocclusion, float reverbocclusion, bool doublesided, int numvertices, global::FMOD.VECTOR[] vertices, out int polygonindex)
		{
			return FMOD5_Geometry_AddPolygon(rawPtr, directocclusion, reverbocclusion, doublesided, numvertices, vertices, out polygonindex);
		}

		public global::FMOD.RESULT getNumPolygons(out int numpolygons)
		{
			return FMOD5_Geometry_GetNumPolygons(rawPtr, out numpolygons);
		}

		public global::FMOD.RESULT getMaxPolygons(out int maxpolygons, out int maxvertices)
		{
			return FMOD5_Geometry_GetMaxPolygons(rawPtr, out maxpolygons, out maxvertices);
		}

		public global::FMOD.RESULT getPolygonNumVertices(int index, out int numvertices)
		{
			return FMOD5_Geometry_GetPolygonNumVertices(rawPtr, index, out numvertices);
		}

		public global::FMOD.RESULT setPolygonVertex(int index, int vertexindex, ref global::FMOD.VECTOR vertex)
		{
			return FMOD5_Geometry_SetPolygonVertex(rawPtr, index, vertexindex, ref vertex);
		}

		public global::FMOD.RESULT getPolygonVertex(int index, int vertexindex, out global::FMOD.VECTOR vertex)
		{
			return FMOD5_Geometry_GetPolygonVertex(rawPtr, index, vertexindex, out vertex);
		}

		public global::FMOD.RESULT setPolygonAttributes(int index, float directocclusion, float reverbocclusion, bool doublesided)
		{
			return FMOD5_Geometry_SetPolygonAttributes(rawPtr, index, directocclusion, reverbocclusion, doublesided);
		}

		public global::FMOD.RESULT getPolygonAttributes(int index, out float directocclusion, out float reverbocclusion, out bool doublesided)
		{
			return FMOD5_Geometry_GetPolygonAttributes(rawPtr, index, out directocclusion, out reverbocclusion, out doublesided);
		}

		public global::FMOD.RESULT setActive(bool active)
		{
			return FMOD5_Geometry_SetActive(rawPtr, active);
		}

		public global::FMOD.RESULT getActive(out bool active)
		{
			return FMOD5_Geometry_GetActive(rawPtr, out active);
		}

		public global::FMOD.RESULT setRotation(ref global::FMOD.VECTOR forward, ref global::FMOD.VECTOR up)
		{
			return FMOD5_Geometry_SetRotation(rawPtr, ref forward, ref up);
		}

		public global::FMOD.RESULT getRotation(out global::FMOD.VECTOR forward, out global::FMOD.VECTOR up)
		{
			return FMOD5_Geometry_GetRotation(rawPtr, out forward, out up);
		}

		public global::FMOD.RESULT setPosition(ref global::FMOD.VECTOR position)
		{
			return FMOD5_Geometry_SetPosition(rawPtr, ref position);
		}

		public global::FMOD.RESULT getPosition(out global::FMOD.VECTOR position)
		{
			return FMOD5_Geometry_GetPosition(rawPtr, out position);
		}

		public global::FMOD.RESULT setScale(ref global::FMOD.VECTOR scale)
		{
			return FMOD5_Geometry_SetScale(rawPtr, ref scale);
		}

		public global::FMOD.RESULT getScale(out global::FMOD.VECTOR scale)
		{
			return FMOD5_Geometry_GetScale(rawPtr, out scale);
		}

		public global::FMOD.RESULT save(global::System.IntPtr data, out int datasize)
		{
			return FMOD5_Geometry_Save(rawPtr, data, out datasize);
		}

		public global::FMOD.RESULT setUserData(global::System.IntPtr userdata)
		{
			return FMOD5_Geometry_SetUserData(rawPtr, userdata);
		}

		public global::FMOD.RESULT getUserData(out global::System.IntPtr userdata)
		{
			return FMOD5_Geometry_GetUserData(rawPtr, out userdata);
		}

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_Release(global::System.IntPtr geometry);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_AddPolygon(global::System.IntPtr geometry, float directocclusion, float reverbocclusion, bool doublesided, int numvertices, global::FMOD.VECTOR[] vertices, out int polygonindex);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_GetNumPolygons(global::System.IntPtr geometry, out int numpolygons);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_GetMaxPolygons(global::System.IntPtr geometry, out int maxpolygons, out int maxvertices);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_GetPolygonNumVertices(global::System.IntPtr geometry, int index, out int numvertices);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_SetPolygonVertex(global::System.IntPtr geometry, int index, int vertexindex, ref global::FMOD.VECTOR vertex);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_GetPolygonVertex(global::System.IntPtr geometry, int index, int vertexindex, out global::FMOD.VECTOR vertex);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_SetPolygonAttributes(global::System.IntPtr geometry, int index, float directocclusion, float reverbocclusion, bool doublesided);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_GetPolygonAttributes(global::System.IntPtr geometry, int index, out float directocclusion, out float reverbocclusion, out bool doublesided);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_SetActive(global::System.IntPtr geometry, bool active);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_GetActive(global::System.IntPtr geometry, out bool active);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_SetRotation(global::System.IntPtr geometry, ref global::FMOD.VECTOR forward, ref global::FMOD.VECTOR up);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_GetRotation(global::System.IntPtr geometry, out global::FMOD.VECTOR forward, out global::FMOD.VECTOR up);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_SetPosition(global::System.IntPtr geometry, ref global::FMOD.VECTOR position);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_GetPosition(global::System.IntPtr geometry, out global::FMOD.VECTOR position);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_SetScale(global::System.IntPtr geometry, ref global::FMOD.VECTOR scale);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_GetScale(global::System.IntPtr geometry, out global::FMOD.VECTOR scale);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_Save(global::System.IntPtr geometry, global::System.IntPtr data, out int datasize);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_SetUserData(global::System.IntPtr geometry, global::System.IntPtr userdata);

		[global::System.Runtime.InteropServices.DllImport("fmod")]
		private static extern global::FMOD.RESULT FMOD5_Geometry_GetUserData(global::System.IntPtr geometry, out global::System.IntPtr userdata);
	}
}
