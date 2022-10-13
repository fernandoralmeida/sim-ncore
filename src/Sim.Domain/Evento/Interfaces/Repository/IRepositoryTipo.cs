﻿using System.Linq.Expressions;

namespace Sim.Domain.Evento.Interfaces.Repository
{
    using Model;
    public interface IRepositoryTipo : IRepositoryBase<ETipo>
    {
        Task<ETipo> GetIdAsync(Guid id);
        Task<IEnumerable<ETipo>> DoListAsync(Expression<Func<ETipo, bool>>? filter = null);
    }
}
