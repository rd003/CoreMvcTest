using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreMvcTest.Controllers
{
    public class SharedController : Controller
    {
        public IActionResult Error()
        {
            return this.View(this.Response.StatusCode);
        }

    }
}
