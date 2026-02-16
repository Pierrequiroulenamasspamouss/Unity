namespace FMOD.Studio
{
	public class Util
	{
		public static global::FMOD.RESULT ParseID(string idString, out global::System.Guid id)
		{
			return FMOD_Studio_ParseID(global::System.Text.Encoding.UTF8.GetBytes(idString + '\0'), out id);
		}

		[global::System.Runtime.InteropServices.DllImport("fmodstudio")]
		private static extern global::FMOD.RESULT FMOD_Studio_ParseID(byte[] idString, out global::System.Guid id);
	}
}
