using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DATABASE = PTPSite.Database;

namespace PTPSite.Services.Impl
{
	public class UserService : IUserService
	{
		private DATABASE.ApplicationDbContext _context;

		public UserService(
			DATABASE.ApplicationDbContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		#region IUserService members

		public async Task Create(ApplicationUser user, CancellationToken cancellationToken = default)
		{
			try
			{
				#region Validation

				if (user == null)
				{
					throw new ArgumentNullException(nameof(user));
				}

				if (user.UserName == default)
				{
					throw new ArgumentException("Invalid user name.");
				}

				if (user.Email == null)
				{
					throw new ArgumentException("Invalid user email..");
				}

				if (user.PasswordHash == null)
				{
					throw new ArgumentException("Invalid user password hash.");
				}

				#endregion

				DATABASE.ApplicationUser dbUser = ConvertUser(user);

				await _context.Users.AddAsync(dbUser, cancellationToken);
				await _context.SaveChangesAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				throw new ServiceException("Failed to create user.", ex);
			}
		}

		public async Task<ApplicationUser> Get(string id, CancellationToken cancellationToken = default)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id));
			}

			try
			{
				DATABASE.ApplicationUser dbUser = await _context.Users
					.FirstAsync(x => x.Id == id, cancellationToken);

				ApplicationUser user = ConvertUser(dbUser);

				return user;
			}
			catch (Exception ex)
			{
				throw new ServiceException($"Failed to get user with identifier `{id}`.", ex);
			}
		}

		public async Task<ApplicationUser> GetNormalized(string normalizedUserName, CancellationToken cancellationToken = default)
		{
			if (normalizedUserName == null)
			{
				throw new ArgumentNullException(nameof(normalizedUserName));
			}

			try
			{
				DATABASE.ApplicationUser dbUser = await _context.Users
					.FirstAsync(x => x.NormalizedUserName == normalizedUserName, cancellationToken);

				ApplicationUser user = ConvertUser(dbUser);

				return user;
			}
			catch (Exception ex)
			{
				throw new ServiceException($"Failed to get user with normalized user name `{normalizedUserName}`.", ex);
			}
		}

		#endregion

		private ApplicationUser ConvertUser(DATABASE.ApplicationUser user)
		{
			return new ApplicationUser
			{
				Id = user.Id,
				Email = user.Email,
				Name = user.Name,
				NormalizedUserName = user.NormalizedUserName,
				PasswordHash = user.PasswordHash,
				Role = ConvertRole(user.Role),
				UserName = user.UserName,
			};
		}

		private DATABASE.ApplicationUser ConvertUser(ApplicationUser user)
		{
			return new DATABASE.ApplicationUser
			{
				Id = user.Id,
				Email = user.Email,
				Name = user.Name,
				NormalizedUserName = user.NormalizedUserName,
				PasswordHash = user.PasswordHash,
				Role = ConvertRole(user.Role),
				UserName = user.UserName,
			};
		}

		private ApplicationRole ConvertRole(DATABASE.ApplicationRole role)
		{
			switch (role)
			{
				case DATABASE.ApplicationRole.Administrator:
					return ApplicationRole.Administrator;
				default:
					return ApplicationRole.None;
			}
		}

		private DATABASE.ApplicationRole ConvertRole(ApplicationRole role)
		{
			switch (role)
			{
				case ApplicationRole.Administrator:
					return DATABASE.ApplicationRole.Administrator;
				default:
					return DATABASE.ApplicationRole.None;
			}
		}
	}
}
