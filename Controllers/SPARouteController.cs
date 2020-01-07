using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace MovieAppAPI.Controllers
{
    public class SPARouteController: Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}