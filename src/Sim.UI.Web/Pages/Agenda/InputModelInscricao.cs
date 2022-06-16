
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Agenda
{
    public class InputModelInscricao
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [DisplayName("Numero")]
        public int Numero { get; set; }

        public string AplicationUser_Id { get; set; }

        [DisplayName("Data")]
        [DataType(DataType.DateTime)]
        public DateTime? Data_Inscricao { get; set; }

        [DisplayName("Presente")]
        public bool Presente { get; set; }

        public virtual Pessoa Participante { get; set; }
        public virtual Empresas Empresa { get; set; }
        public virtual Evento Evento { get; set; }
    }
}
