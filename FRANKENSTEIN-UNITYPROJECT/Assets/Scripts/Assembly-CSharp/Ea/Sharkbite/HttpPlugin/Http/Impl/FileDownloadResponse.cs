namespace Ea.Sharkbite.HttpPlugin.Http.Impl
{
	public class FileDownloadResponse : global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultResponse
	{
		public override string Body
		{
			get
			{
				throw new global::System.NotImplementedException("Download requests do not have a body.  The downloaded contents were written to the specified file.");
			}
			set
			{
				throw new global::System.NotImplementedException("Download requests do not have a body.  The downloaded contents were written to the specified file.");
			}
		}

		public override bool Success
		{
			get
			{
				return Code >= 200 && Code < 300 && string.IsNullOrEmpty(Error);
			}
		}

		public virtual string Error { get; set; }

		public global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadResponse WithError(string error)
		{
			Error = error;
			return this;
		}
	}
}
