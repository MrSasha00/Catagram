namespace Catagram.Client.Service;

public interface INavigationService
{
	void ToRoot();
	void ToCreatePost();
	void ToLogin();
	void ToPostsByLogin(string login);
	void ToPosts();
	void ToMyPosts();
	void ToEditPost(int editPostId);
}