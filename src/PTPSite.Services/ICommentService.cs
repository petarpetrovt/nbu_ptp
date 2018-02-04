using System.Threading;
using System.Threading.Tasks;

namespace PTPSite.Services
{
	public interface ICommentService
	{
		Task Create(Comment comment, CancellationToken cancellationToken);

		Task Edit(Comment comment, CancellationToken cancellationToken);

		Task<Comment> Get(int id, CancellationToken cancellationToken);

		Task Remove(int id, CancellationToken cancellationToken);

		Task<Comment[]> List(CancellationToken cancellationToken);
	}
}
