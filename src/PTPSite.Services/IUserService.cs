using System.Threading;
using System.Threading.Tasks;

namespace PTPSite.Services
{
	public interface IUserService
	{
		Task Create(
			ApplicationUser user,
			CancellationToken cancellationToken = default);

		Task<ApplicationUser> Get(
			string id,
			CancellationToken cancellationToken = default);

		Task<ApplicationUser> GetNormalized(
			string normalizedNserName,
			CancellationToken cancellationToken = default);
	}
}
