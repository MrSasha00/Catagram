using Catagram.Server.Data;
using Catagram.Server.Models.Service;
using Catagram.Shared.Interfaces;
using Catagram.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catagram.Server.Controllers;

[Authorize]
[Route("[controller]/[action]")]
[ApiController]
public class PostController : ControllerBase, IPostController
{
	private readonly ApplicationDBContext _applicationDbContext;
	private readonly IFtpService _ftpService;

	public PostController(IFtpService ftpService, ApplicationDBContext applicationDbContext)
	{
		_ftpService = ftpService;
		_applicationDbContext = applicationDbContext;
	}

	[HttpGet]
	public async Task<IEnumerable<Post>> GetAll() =>
		await _applicationDbContext.Feeds
			.Include(f => f.User)
			.ToListAsync();

	[HttpGet]
	public async Task<IEnumerable<Post>> GetByLogin([FromQuery] string login) =>
		await _applicationDbContext.Feeds
			.Where(p => p.User.Login == login)
			.Include(f => f.User)
			.ToListAsync();

	[HttpPost]
	public async Task Create(CreatePostDTO createPost)
	{
		var userName = User.Identity?.Name;
		var user = _applicationDbContext.Users.First(u => u.Login == userName);
		var fileName = $"{userName}-{Guid.NewGuid()}.png";
		_ftpService.Upload(fileName, createPost.PathPhoto);

		await _applicationDbContext.Feeds.AddAsync(new Post
		{
			PathPhoto = fileName,
			Description = createPost.Description,
			CreatedDt = DateTime.Now.ToUniversalTime(),
			UserId = user.Id,
			User = user
		});

		await _applicationDbContext.SaveChangesAsync();
	}

	[HttpGet]
	public async Task<string> GetFile([FromQuery] string fileName) =>
		await Task.FromResult(_ftpService.Download(fileName));

	[HttpGet]
	public async Task<Post> GetPostById([FromQuery] int postId) =>
		await _applicationDbContext.Feeds
			.Include(f => f.User)
			.FirstAsync(p => p.Id == postId);

	[HttpPut]
	public async Task Update([FromBody] Post updatePost)
	{
		var post = await _applicationDbContext.Feeds.FirstOrDefaultAsync(p => p.Id == updatePost.Id);
		if (post != null)
		{
			post.Description = updatePost.Description;
			await _applicationDbContext.SaveChangesAsync();
		}
	}

	[HttpDelete]
	public async Task Delete([FromQuery] int postId)
	{
		var post = await _applicationDbContext.Feeds.FirstOrDefaultAsync(p => p.Id == postId);
		if (post != null)
		{
			_applicationDbContext.Feeds.Remove(post);
			await _applicationDbContext.SaveChangesAsync();
		}
	}

}