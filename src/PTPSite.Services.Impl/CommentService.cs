﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

		public async Task Edit(Comment comment, CancellationToken cancellationToken)
		{
			if (comment == null)
			{
				throw new ArgumentNullException(nameof(comment));
			}

			try
			{
				DATABASE.Comment dbComment = await _context.Comments
					.FirstOrDefaultAsync(x => x.Id == comment.Id);

				if (dbComment == null)
				{
					throw new ServiceException("Invalid comment to edit");
				}

				dbComment.Text = comment.Text;
				dbComment.Date = DateTime.Now;

				await _context.SaveChangesAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				throw new ServiceException($"Failed to edit comment.", ex);
			}
		}

		public async Task<Comment> Get(string id, CancellationToken cancellationToken)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id));
			}

			try
			{
				DATABASE.Comment dbComment = await _context.Comments
					.FirstOrDefaultAsync(x => x.Id == id);

				if (dbComment == null)
				{
					throw new ServiceException("Invalid comment to get");
				}

				return ConvertComment(dbComment);
			}
			catch (Exception ex)
			{
				throw new ServiceException($"Failed to get comment.", ex);
			}
		}

		public async Task Remove(string id, CancellationToken cancellationToken)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id));
			}

			try
			{
				DATABASE.Comment dbComment = await _context.Comments
					.FirstOrDefaultAsync(x => x.Id == id);

				if (dbComment == null)
				{
					throw new ServiceException("Invalid comment to remove");
				}

				_context.Comments.Remove(dbComment);
				await _context.SaveChangesAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				throw new ServiceException($"Failed to remove comment.", ex);
			}
		}

		public async Task<Comment[]> List(CancellationToken cancellationToken = default)
		{
			try
			{
				var entities = await (
					from e in _context.Comments
					join u in _context.Users
						on e.ByUserId equals u.Id
					orderby e.Date descending, u.Name
					select new
					{
						Comment = e,
						UserName = u.Name,
					}
				).ToArrayAsync(cancellationToken);

				Comment[] comments = entities
					.Select(x => ConvertComment(x.Comment, x.UserName))
					.ToArray();

				return comments;
			}
			catch (Exception ex)
			{
				throw new ServiceException($"Failed to list comments.", ex);
			}
		}

		#endregion

		private Comment ConvertComment(DATABASE.Comment value, string userName = null)
		{
			var comment = new Comment
			{
				ByUserId = value.ByUserId,
				Id = value.Id,
				Text = value.Text,
				Date = value.Date,
				ByUserName = userName,
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
