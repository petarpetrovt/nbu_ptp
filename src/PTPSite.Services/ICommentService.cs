using System.Threading;
using System.Threading.Tasks;

namespace PTPSite.Services
{
	public interface ICommentService
	{
		Task Create(Comment comment, CancellationToken cancellationToken);

		Task Edit(Comment comment, CancellationToken cancellationToken);

		Task<Comment> Get(string id, CancellationToken cancellationToken);

		Task Remove(string id, CancellationToken cancellationToken);

		Task<Comment[]> List(CancellationToken cancellationToken);
	}
}
