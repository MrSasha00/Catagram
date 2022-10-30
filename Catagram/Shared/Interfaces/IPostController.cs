using Catagram.Shared.Model;
using Refit;

namespace Catagram.Shared.Interfaces;

public interface IPostController
{
	[Get("/Post/GetAll")]
	Task<IEnumerable<Post>> GetAll();

	[Get("/Post/GetByLogin")]
	Task<IEnumerable<Post>> GetByLogin(string login);

	[Post("/Post/Create")]
	Task Create(CreatePostDTO createPost);

	[Get("/Post/GetFile")]
	Task<string> GetFile(string fileName);

	[Get("/Post/GetPostById")]
	Task<Post> GetPostById(int postId);

	[Put("/Post/Update")]
	Task Update(Post updatePost);

	[Delete("/Post/Delete")]
	Task Delete(int postId);
}