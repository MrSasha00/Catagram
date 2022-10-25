namespace Catagram.Server.Models.Service;

public interface IFtpService
{
	void Upload(string fileName, string base64Content);
	string Download(string fileName);
}