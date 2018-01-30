using System.Threading;
using System.Threading.Tasks;

namespace PTPSite.Services
{
	public interface ICommentService
	{
		Task Create(Comment comment, CancellationToken cancellationToken);
	}
}
