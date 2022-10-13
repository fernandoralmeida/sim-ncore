using System.Linq.Expressions;
using Sim.Domain.Organizacao.Model;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceCanal : IAppServiceBase<ECanal>
    {
        Task<ECanal> GetIdAsync(Guid id);
        Task<IEnumerable<ECanal>> DoListAsync(Expression<Func<ECanal, bool>>? filter = null);
        Task<IEnumerable<(string canal, string value)>> DoListJson(string setor);
    }
}
