using System;
using System.Threading;
using System.Threading.Tasks;
using DATABASE = PTPSite.Database;

namespace PTPSite.Services.Impl
{
	public class CommentService : ICommentService
	{
		private DATABASE.ApplicationDbContext _context;

		public CommentService(
			DATABASE.ApplicationDbContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		#region ICommentService members

		public async Task Create(Comment comment, CancellationToken cancellationToken = default)
		{
			if (comment == null)
			{
				throw new ArgumentNullException(nameof(comment));
			}

			try
			{
				DATABASE.Comment dbComment = ConvertComment(comment);

				await _context.Comments.AddAsync(dbComment, cancellationToken);
				await _context.SaveChangesAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				throw new ServiceException($"Failed to create comment.", ex);
			}
		}

		#endregion

		private Comment ConvertComment(DATABASE.Comment value)
		{
			var comment = new Comment
			{
				ByUserId = value.ByUserId,
				Id = value.Id,
				Text = value.Text,
				Date = value.Date,
			};

			return comment;
		}

		private DATABASE.Comment ConvertComment(Comment value)
		{
			var comment = new DATABASE.Comment
			{
				ByUserId = value.ByUserId,
				Id = value.Id,
				Text = value.Text,
				Date = value.Date,
			};

			return comment;
		}
	}
}
