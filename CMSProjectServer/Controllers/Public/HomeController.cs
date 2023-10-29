using Microsoft.AspNetCore.Mvc;

namespace CMSProjectServer.Controllers.Public;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}