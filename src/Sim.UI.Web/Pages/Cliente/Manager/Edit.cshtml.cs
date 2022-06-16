using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Pages.Cliente
{
    using Sim.Domain.SDE.Entity;
    using Sim.Application.SDE.Interface;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IAppServicePessoa _pessoa;
        private readonly IMapper _mapper;

        public EditModel(IAppServicePessoa pessoa, IMapper mapper)
        {
            _pessoa = pessoa;
            _mapper = mapper;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelPessoa Input { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var t = Task.Run(
                () => _pessoa.GetById((Guid)id));

            await t;


            Input = _mapper.Map<InputModel>(t.Result);

            if (Input.Deficiencia != null)
            {

                if (t.Result.Deficiencia.Contains("Física"))
                    Input.Fisica = true;

                if (t.Result.Deficiencia.Contains("Visual"))
                    Input.Visual = true;

                if (t.Result.Deficiencia.Contains("Auditiva"))
                    Input.Auditiva = true;

                if (t.Result.Deficiencia.Contains("Intelectual"))
                    Input.Intelectual = true;
            }


            if (Input == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (ModelState.IsValid)
                {

                    var task = Task.Run(() => {

                        var pessoa = _mapper.Map<Cliente>(Input);

                        pessoa.Deficiencia = string.Empty;

                        if (Input.Fisica)
                            pessoa.Deficiencia += "Física;";

                        if (Input.Visual)
                            pessoa.Deficiencia += "Visual;";

                        if (Input.Auditiva)
                            pessoa.Deficiencia += "Auditiva;";

                        if (Input.Intelectual)
                            pessoa.Deficiencia += "Intelectual;";

                        _pessoa.Update(pessoa);

                    });

                    await task;

                    return RedirectToPage("./Index");
                }

                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }
        }

        private Pessoa PessoaExists(Guid id)
        {
            return _pessoa.GetById(id);
        }

    }
}
