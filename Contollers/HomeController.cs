using Microsoft.AspNetCore.Mvc;
using Num2Words.Services;

namespace Num2Words.Contollers;

public class HomeController : Controller
{
    private readonly INumConvSvc _numConvSvc;

    public HomeController(INumConvSvc numConvSvc)
    {
        _numConvSvc = numConvSvc;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Convert([FromBody] string input)
    {
        try
        {
            var result = await _numConvSvc.ConvertAsync(input);
            return Content(result); // Return plain string
        }
        catch (ArgumentException ex)
        {
            return Content($"Error: {ex.Message}"); // Return plain string
        }
    }
}