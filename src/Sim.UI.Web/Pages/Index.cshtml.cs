﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sim.UI.Web.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {

        [TempData]
        public string StatusMessage{get;set;}
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGetSetTheme(string userid, string theme) {  
            
            return RedirectToPage();
        }
        public void OnGet()
        {

        }
    }
}
