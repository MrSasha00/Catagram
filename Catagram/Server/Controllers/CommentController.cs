using Catagram.Server.Data;
using Catagram.Shared.Interfaces;
using Catagram.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catagram.Server.Controllers;


[Authorize]
[Route("[controller]/[action]")]
[ApiController]
public class CommentController : ControllerBase, ICommentController
{
	private readonly ApplicationDBContext _applicationDbContext;

	public CommentController(ApplicationDBContext applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
	}

	[HttpPost]
	public async Task SaveComment(CreateCommentDto createCommentDto)
	{
		var user = _applicationDbContext.Users.First(u => u.Login == createCommentDto.UserName);
		var post = _applicationDbContext.Feeds.First(p => p.Id == createCommentDto.PostId);
		var comment = new Comment
		{
			Content = createCommentDto.Content,
			CreatedDt = createCommentDto.CreatedDt.ToUniversalTime(),
			UserId = user.Id,
			User = user,
			PostId = post.Id,
			Post = post
		};
		await _applicationDbContext.Comments.AddAsync(comment);
		await _applicationDbContext.SaveChangesAsync();
	}

	[HttpGet]
	public async Task<IEnumerable<Comment>> GetCommentsByPost(int postId)
	{
			return await _applicationDbContext.Comments
				.Include(c => c.User)
				.Where(m => m.PostId == postId)
				.ToListAsync();
	}
}