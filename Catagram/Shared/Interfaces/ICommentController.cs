using Catagram.Shared.Model;
using Refit;

namespace Catagram.Shared.Interfaces;

public interface ICommentController
{
	[Post("/Comment/SaveComment")]
	Task SaveComment(CreateCommentDto comment);

	[Get("/Comment/GetCommentsByPost")]
	Task<IEnumerable<Comment>> GetCommentsByPost(int postId);
}