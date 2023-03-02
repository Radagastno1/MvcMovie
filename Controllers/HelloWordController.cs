using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers;

public class HelloWorldController : Controller
{
    //
    // GET: /HelloWorld/
    public IActionResult Index()
    {
        return View();
    }

    // GET: /HelloWorld/Welcome/
    // Requires using System.Text.Encodings.Web;
    public IActionResult Welcome(string name, int numTimes = 1)
    {
        ViewData["Message"] = "Hello " + name;
        ViewData["NumTimes"] = numTimes;
        return View();
    }
    public string Joke(string category)
    {
        switch (category)
        {
            case "dark":
                return HtmlEncoder.Default.Encode($"This is a dark joke");
            case "animal":
                return HtmlEncoder.Default.Encode($"This is an animal joke");
            default:
                return HtmlEncoder.Default.Encode($"This is a joke");
        }
    }
}
