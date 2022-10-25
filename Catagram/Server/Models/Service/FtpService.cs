using System.Net;

namespace Catagram.Server.Models.Service;

public class FtpService : IFtpService
{
	private readonly FtpConfigOptions _ftpConfigOptions;

	public FtpService(FtpConfigOptions ftpConfigOptions)
	{
		_ftpConfigOptions = ftpConfigOptions;
	}

	public void Upload(string fileName, string base64Content)
	{
		var byteContent = Convert.FromBase64String(base64Content);
		var request = CreateRequest(fileName);
		request.Method = WebRequestMethods.Ftp.UploadFile;
		request.ContentLength = byteContent.Length;

		using var s = request.GetRequestStream();
		s.Write(byteContent, 0, byteContent.Length);
	}

	public string Download(string fileName)
	{
		var request = CreateRequest(fileName);
		request.Method = WebRequestMethods.Ftp.DownloadFile;
		var response = (FtpWebResponse)request.GetResponse();
		var responseStream = response.GetResponseStream();

		var buffer = new byte[16*1024];
		using var ms = new MemoryStream();
		int read;
		while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0) 
			ms.Write(buffer, 0, read);

		return Convert.ToBase64String(ms.ToArray());
	}

	private FtpWebRequest CreateRequest(string path)
	{
		var request = (FtpWebRequest)WebRequest.Create(_ftpConfigOptions.ConnectionString + path);
		request.Credentials = new NetworkCredential(_ftpConfigOptions.Login, _ftpConfigOptions.Password);
		request.UseBinary = true;

		return request;
	}
}