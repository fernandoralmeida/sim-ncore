using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;
using Sim.Data.Context;

namespace Sim.Data.Config
{
     
    public class GeradorProtocolo : ValueGenerator<string>
    {
        private readonly ApplicationContext _context;
        public override bool GeneratesTemporaryValues => false;

        public GeradorProtocolo()
        {
            _context = new  ApplicationContext();
        }

        public override string Next([NotNullAttribute] EntityEntry entry)
        {
            var startprotocol = string.Format("{0}{1}", DateTime.Now.Year, "00000");

            var lastprotocol = _context.Contador
                .AsNoTracking()
                .OrderBy(c => c.Numero)
                .LastOrDefault()?.Numero;

            if (lastprotocol == null)
                lastprotocol = startprotocol;

            int currentvalue = Convert.ToInt32(lastprotocol);

            currentvalue ++;

            var contadorTable = _context.Contador.FirstOrDefault();

            if (contadorTable == null)
                return currentvalue.ToString();

            contadorTable.Numero = currentvalue.ToString();

            //_context.SaveChanges();

            return currentvalue.ToString();
        }

    }
}
