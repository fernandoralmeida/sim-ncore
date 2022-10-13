﻿using System.Linq.Expressions;

namespace Sim.Domain.Evento.Interfaces.Repository;

using Model;
public interface IRepositoryParceiro: IRepositoryBase<EParceiro>
{
        Task<EParceiro> GetIdAsync(Guid id);
        Task<IEnumerable<EParceiro>> DoListAsync(Expression<Func<EParceiro, bool>>? filter = null);
}

