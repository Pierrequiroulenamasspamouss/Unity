namespace ICSharpCode.SharpZipLib.Zip
{
	public class DiskArchiveStorage : global::ICSharpCode.SharpZipLib.Zip.BaseArchiveStorage
	{
		private global::System.IO.Stream temporaryStream_;

		private string fileName_;

		private string temporaryName_;

		public DiskArchiveStorage(global::ICSharpCode.SharpZipLib.Zip.ZipFile file, global::ICSharpCode.SharpZipLib.Zip.FileUpdateMode updateMode)
			: base(updateMode)
		{
			if (file.Name == null)
			{
				throw new global::ICSharpCode.SharpZipLib.Zip.ZipException("Cant handle non file archives");
			}
			fileName_ = file.Name;
		}

		public DiskArchiveStorage(global::ICSharpCode.SharpZipLib.Zip.ZipFile file)
			: this(file, global::ICSharpCode.SharpZipLib.Zip.FileUpdateMode.Safe)
		{
		}

		public override global::System.IO.Stream GetTemporaryOutput()
		{
			if (temporaryName_ != null)
			{
				temporaryName_ = GetTempFileName(temporaryName_, true);
				temporaryStream_ = global::System.IO.File.Open(temporaryName_, global::System.IO.FileMode.OpenOrCreate, global::System.IO.FileAccess.Write, global::System.IO.FileShare.None);
			}
			else
			{
				temporaryName_ = global::System.IO.Path.GetTempFileName();
				temporaryStream_ = global::System.IO.File.Open(temporaryName_, global::System.IO.FileMode.OpenOrCreate, global::System.IO.FileAccess.Write, global::System.IO.FileShare.None);
			}
			return temporaryStream_;
		}

		public override global::System.IO.Stream ConvertTemporaryToFinal()
		{
			if (temporaryStream_ == null)
			{
				throw new global::ICSharpCode.SharpZipLib.Zip.ZipException("No temporary stream has been created");
			}
			global::System.IO.Stream stream = null;
			string tempFileName = GetTempFileName(fileName_, false);
			bool flag = false;
			try
			{
				temporaryStream_.Close();
				global::System.IO.File.Move(fileName_, tempFileName);
				global::System.IO.File.Move(temporaryName_, fileName_);
				flag = true;
				global::System.IO.File.Delete(tempFileName);
				return global::System.IO.File.Open(fileName_, global::System.IO.FileMode.Open, global::System.IO.FileAccess.Read, global::System.IO.FileShare.Read);
			}
			catch (global::System.Exception)
			{
				stream = null;
				if (!flag)
				{
					global::System.IO.File.Move(tempFileName, fileName_);
					global::System.IO.File.Delete(temporaryName_);
				}
				throw;
			}
		}

		public override global::System.IO.Stream MakeTemporaryCopy(global::System.IO.Stream stream)
		{
			stream.Close();
			temporaryName_ = GetTempFileName(fileName_, true);
			global::System.IO.File.Copy(fileName_, temporaryName_, true);
			temporaryStream_ = new global::System.IO.FileStream(temporaryName_, global::System.IO.FileMode.Open, global::System.IO.FileAccess.ReadWrite);
			return temporaryStream_;
		}

		public override global::System.IO.Stream OpenForDirectUpdate(global::System.IO.Stream stream)
		{
			if (stream == null || !stream.CanWrite)
			{
				if (stream != null)
				{
					stream.Close();
				}
				return new global::System.IO.FileStream(fileName_, global::System.IO.FileMode.Open, global::System.IO.FileAccess.ReadWrite);
			}
			return stream;
		}

		public override void Dispose()
		{
			if (temporaryStream_ != null)
			{
				temporaryStream_.Close();
			}
		}

		private static string GetTempFileName(string original, bool makeTempFile)
		{
			string text = null;
			if (original == null)
			{
				text = global::System.IO.Path.GetTempFileName();
			}
			else
			{
				int num = 0;
				int second = global::System.DateTime.Now.Second;
				while (text == null)
				{
					num++;
					string text2 = string.Format("{0}.{1}{2}.tmp", original, second, num);
					if (global::System.IO.File.Exists(text2))
					{
						continue;
					}
					if (makeTempFile)
					{
						try
						{
							using (global::System.IO.File.Create(text2))
							{
							}
							text = text2;
						}
						catch
						{
							second = global::System.DateTime.Now.Second;
						}
					}
					else
					{
						text = text2;
					}
				}
			}
			return text;
		}
	}
}
