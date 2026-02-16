namespace Ea.Sharkbite.HttpPlugin.Http.Impl
{
	public class FileDownloadRequest : global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest, global::Ea.Sharkbite.HttpPlugin.Http.Api.INotifiable
	{
		private const int kBufferLength = 4096;

		private const string kContentLengthHeader = "Content-Length";

		public const uint KUndefinedContentLength = 0u;

		private string m_FilePath;

		private string m_TempFilePath;

		private string m_MD5 = string.Empty;

		private long m_DownloadedAmount;

		private uint m_DownloadSize;

		private bool m_CachedTag;

		private bool m_UseGzip;

		private global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.DownloadProgress, global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> notifyAction;

		private global::Ea.Sharkbite.HttpPlugin.Http.Api.DownloadProgress progress;

		public int DownloadedBytes
		{
			get
			{
				return (int)m_DownloadedAmount;
			}
		}

		public uint ContentLength
		{
			get
			{
				return m_DownloadSize;
			}
		}

		public FileDownloadRequest(string uri)
			: base(uri)
		{
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadRequest WithOutputFile(string filePath)
		{
			m_FilePath = filePath;
			m_TempFilePath = string.Format("{0}.download", filePath);
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadRequest WithMD5(string md5)
		{
			m_MD5 = md5;
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadRequest WithGzip(bool useGzip)
		{
			m_UseGzip = useGzip;
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadRequest WithBackupFlag(bool backup)
		{
			return this;
		}

		private string ReadResponse(global::System.IO.Stream input)
		{
			string result = string.Empty;
			global::System.Security.Cryptography.MD5 mD = global::System.Security.Cryptography.MD5.Create();
			using (global::System.IO.FileStream fileStream = global::System.IO.File.Create(m_TempFilePath))
			{
				m_DownloadedAmount = 0L;
				byte[] array = new byte[4096];
				int num = input.Read(array, 0, array.Length);
				while (num > 0 && !abort)
				{
					fileStream.Write(array, 0, num);
					m_DownloadedAmount += num;
					mD.TransformBlock(array, 0, num, array, 0);
					Notify(102400L);
					num = input.Read(array, 0, array.Length);
				}
				if (!abort)
				{
					Notify(0L);
					mD.TransformFinalBlock(array, 0, 0);
					byte[] hash = mD.Hash;
					global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
					for (int i = 0; i < hash.Length; i++)
					{
						stringBuilder.Append(hash[i].ToString("x2"));
					}
					result = stringBuilder.ToString();
				}
			}
			return result;
		}

		protected new string ReadResponse(global::System.Net.HttpWebResponse response)
		{
			using (global::System.IO.Stream stream = response.GetResponseStream())
			{
				string text = response.Headers["Content-Encoding"];
				if (!string.IsNullOrEmpty(text) && text.ToLower().Contains("gzip"))
				{
					using (global::ICSharpCode.SharpZipLib.GZip.GZipInputStream input = new global::ICSharpCode.SharpZipLib.GZip.GZipInputStream(stream))
					{
						return ReadResponse(input);
					}
				}
				return ReadResponse(stream);
			}
		}

		protected override global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse GetResponse()
		{
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response = null;
			if (m_FilePath != null)
			{
				try
				{
					string error = null;
					if (global::System.IO.File.Exists(m_FilePath))
					{
						global::System.IO.File.Delete(m_FilePath);
					}
					if (global::System.IO.File.Exists(m_TempFilePath))
					{
						global::System.IO.File.Delete(m_TempFilePath);
					}
					string directoryName = global::System.IO.Path.GetDirectoryName(m_FilePath);
					if (!global::System.IO.Directory.Exists(directoryName))
					{
						global::System.IO.Directory.CreateDirectory(directoryName);
					}
					global::System.DateTime now = global::System.DateTime.Now;
					using (global::System.Net.HttpWebResponse httpWebResponse = ExecuteRequest())
					{
						if (httpWebResponse == null)
						{
							response = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadResponse().WithError("The request timed out?").WithRequest(this).WithCode(408)
								.WithConnectionLoss(true);
							return response;
						}
						global::System.Collections.Generic.Dictionary<string, string> dictionary = ProcessResponse(httpWebResponse);
						if (dictionary.ContainsKey("Content-Length"))
						{
							m_DownloadSize = global::System.Convert.ToUInt32(dictionary["Content-Length"]);
						}
						else
						{
							m_DownloadSize = 0u;
						}
						string text = string.Empty;
						if (httpWebResponse.StatusCode != global::System.Net.HttpStatusCode.RequestedRangeNotSatisfiable)
						{
							text = ReadResponse(httpWebResponse);
						}
						int code = (int)httpWebResponse.StatusCode;
						if (!abort && (string.IsNullOrEmpty(m_MD5) || m_MD5 == text))
						{
							global::System.IO.File.Move(m_TempFilePath, m_FilePath);
						}
						else
						{
							global::System.IO.File.Delete(m_TempFilePath);
							error = ((!abort) ? string.Format("Invalid MD5SUM {0}!={1}", m_MD5, text) : "Aborting file download");
							code = 418;
						}
						int downloadTime = global::UnityEngine.Mathf.RoundToInt((float)(global::System.DateTime.Now - now).TotalMilliseconds);
						response = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadResponse().WithError(error).WithCode(code).WithRequest(this)
							.WithContentLength(httpWebResponse.ContentLength)
							.WithContentType(httpWebResponse.ContentType)
							.WithDownloadTime(downloadTime)
							.WithHeaders(dictionary);
					}
				}
				catch (global::System.Exception ex)
				{
					response = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadResponse().WithError(ex.Message).WithRequest(this).WithCode(500);
					global::Kampai.Util.Native.LogError(ex.Message);
				}
				if (response == null)
				{
					response = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.FileDownloadResponse().WithError("Download could not be completed successfully.").WithRequest(this);
				}
				return response;
			}
			throw new global::System.InvalidOperationException("You must specify a file path using .WithOutputFile() to perform this operation.");
		}

		private void Notify(long downloadSizeDeltaMin)
		{
			if (notifyAction != null)
			{
				if (progress == null)
				{
					progress = new global::Ea.Sharkbite.HttpPlugin.Http.Api.DownloadProgress();
					progress.NotifySignal = NotifyProgress;
					progress.TotalBytes = m_DownloadSize;
				}
				long num = m_DownloadedAmount - progress.DownloadedBytes;
				if (num > downloadSizeDeltaMin)
				{
					progress.Delta = num;
					progress.DownloadedBytes = m_DownloadedAmount;
					notifyAction(progress, this);
				}
			}
		}

		public void RegisterNotifiable(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.DownloadProgress, global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> notify)
		{
			notifyAction = notify;
		}

		protected override global::System.Net.HttpWebRequest CreateRequest()
		{
			global::System.Net.HttpWebRequest httpWebRequest = base.CreateRequest();
			if (m_UseGzip)
			{
				httpWebRequest.Headers["Accept-Encoding"] = "gzip";
			}
			return httpWebRequest;
		}
	}
}
