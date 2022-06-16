using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sim.UI.Web.Pages.Pat
{
    using Sim.Domain.SDE.Entity;
    public class InputModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [DisplayName("Data")]
        [DataType(DataType.Date)]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Ocupação exigida!")]
        [DisplayName("Ocupação")]
        public string Ocupacao { get; set; }

        [DisplayName("Experiência")]
        public bool Experiencia { get; set; }

        [DisplayName("Salário Médio")]
        [DataType(DataType.Currency)]
        public string Salario { get; set; }

        [DisplayName("Forma de pagamento")]
        public string Pagamento { get; set; }

        [Required(ErrorMessage = "Informe a quantidade de vagas!")]
        [DisplayName("Vagas Disponível")]
        public int Vagas { get; set; }
        public string Status { get;set; }
        public string AppUserID { get; set; }
        public virtual Empresas Empresa { get; set; }
    }
}
