namespace Sim.Domain.Organizacao.Service
{
    using Model;
    using Organizacao.Interfaces.Repository;
    using Organizacao.Interfaces.Service;
    public class ServiceCanal : ServiceBase<ECanal>, IServiceCanal
    {
        private readonly IRepositoryCanal _canal;
        public ServiceCanal(IRepositoryCanal repositoryCanal)
            :base(repositoryCanal)
        { _canal = repositoryCanal; }

        public async Task<ECanal> GetIdAsync(Guid id)
        {
            return await _canal.GetIdAsync(id);
        }

        public async Task<IEnumerable<ECanal>> ListAllAsync()
        {
            return await _canal.ListAllAsync();
        }

        public async Task<IEnumerable<ECanal>> ListCanalOwner(string setor)
        {
            return await _canal.ListCanalOwner(setor);
        }

        public async Task<IEnumerable<(string canal, string value)>> ToListJson(string setor)
        {
            var list = await ListCanalOwner(setor);
            var canallist = new List<(string canal, string value)>();

            foreach(var item in list)
            {
                canallist.Add(new() { canal = item.Nome, value = item.Nome });
            }

            return canallist;
        }
    }
}
