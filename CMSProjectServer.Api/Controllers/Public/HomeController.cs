using Microsoft.AspNetCore.Mvc;

namespace CMSProjectServer.Api.Controllers.Public;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}