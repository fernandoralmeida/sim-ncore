﻿using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{

    public class ServiceSetor: ServiceBase<Setor>, IServiceSetor
    {
        private readonly IRepositorySetor _setor;
        public ServiceSetor(IRepositorySetor repositorySetor)
            :base(repositorySetor)
        {
            _setor = repositorySetor;
        }

        public async Task<IEnumerable<Setor>> ListSetorOwnerAsync(string secretaria)
        {
            return await _setor.ListSetorOwnerAsync(secretaria);
        }
    }
}