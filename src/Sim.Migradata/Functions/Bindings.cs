using Sim.Domain.Customer.Models;
using Sim.Data.Repository;
using Sim.Data.Context;

namespace Sim.Migradata.Functions;

public static class Bindings
{
    private static ApplicationContext _app = new ApplicationContext();
    private static RepositoryBindings _bindings = new RepositoryBindings(_app);
    private static RepositoryPessoa _pessoa = new RepositoryPessoa(_app);
    private static RepositoryEmpresa _empresa = new RepositoryEmpresa(_app);

    public static async Task Starting()
    {
        foreach (var p in await _pessoa.DoListOnlyUnlinkeds())
            foreach (var e in await _empresa.DoList(s => s.Nome_Empresarial!.Contains(p.CPF!.MaskRemove()) && s.Situacao_Cadastral != "BAIXADA"))
            {
                var _validate = await _bindings.DoListAsync(s => s.Pessoa!.CPF == p.CPF && s.Empresa!.CNPJ == e.CNPJ);
                if (_validate.Count() < 1)
                {
                    await _bindings.AddAsync(new EBindings() { Empresa = e, Pessoa = p, Vinculo = TBindings.Proprietario });
                    Console.WriteLine($"Vinculado CPF:***.***{p.CPF!.Remove(0,7)} | CNPJ:**.***{e.CNPJ!.Remove(0,6)} => {TBindings.Proprietario}");
                }
            }
        var _c = await _bindings.DoListAsync();
        Console.WriteLine($"{_c.Count()} Vinculos realizados!");
    }
    public static async Task DoListAsync()
    {
        foreach (var item in await _bindings.DoListAsync())
            Console.WriteLine($"{item.Pessoa!.CPF} - {item.Vinculo.ToString()} - {item.Empresa!.CNPJ}");

    }

}