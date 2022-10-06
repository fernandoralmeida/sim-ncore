﻿namespace Sim.Domain.Organizacao.Service;

using Model;
using Organizacao.Interfaces.Repository;
using Organizacao.Interfaces.Service;
public class ServiceSecretaria : ServiceBase<EOrganizacao>, IServiceSecretaria
{
    private readonly IRepositorySecretaria _secretaria;

    public ServiceSecretaria(IRepositorySecretaria repositorySecretaria)
        :base(repositorySecretaria)
    {
        _secretaria = repositorySecretaria;
    }
    public async Task<IEnumerable<EOrganizacao>> DoListHierarquia0Async(IEnumerable<EOrganizacao> lista)
    {
        return await Task.Run(() => lista.Where(m => m.IsMatriz(m)));
    }
    public async Task<IEnumerable<EOrganizacao>> DoListHierarquia1Async(IEnumerable<EOrganizacao> lista)
    {
        return await Task.Run(() => lista.Where(m => m.IsSecretaria(m)));
    }
    public async Task<IEnumerable<EOrganizacao>> DoListHierarquia2Async(IEnumerable<EOrganizacao> lista){
        return await Task.Run(() => lista.Where(m => m.IsSetor(m)));
    }
    public async Task<IEnumerable<EOrganizacao>> DoListHierarquia1from0Async(IEnumerable<EOrganizacao> lista, Guid id)
    {
        return await Task.Run(() => lista.Where(m => m.IsSecOfOrganizacao(m, id)));
    }
    public async Task<IEnumerable<EOrganizacao>> DoListHierarquia2from0Async(IEnumerable<EOrganizacao> lista, Guid id)
    {
        return await Task.Run(() => lista.Where(m => m.IsSecOfOrganizacao(m, id)));
    }
    public async Task<IEnumerable<EOrganizacao>> DoListHierarquia2from1Async(IEnumerable<EOrganizacao> lista, Guid id)
    {
        return await Task.Run(() => lista.Where(m => m.IsSetorOfSecretaria(m, id)));
    }
    public async Task<EOrganizacao> GetIdAsync(Guid id)
    {
        return await _secretaria.GetIdAsync(id);
    }
    public async Task<IEnumerable<EOrganizacao>> ListAllAsync()
    {
        return await _secretaria.ListAllAsync();
    }
    public async Task<IEnumerable<EOrganizacao>> ListSecretariaOwnerAsync(string setor)
    {
        return await _secretaria.ListSecretariaOwnerAsync(setor);
    }
}

