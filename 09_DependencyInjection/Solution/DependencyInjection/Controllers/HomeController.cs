using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DependencyInjection.Services;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Controllers
{
    public class HomeController : Controller
    {
        public readonly ISampleTransient _sampleTransient;
        public readonly ISampleScoped _sampleScoped;
        public readonly ISampleSingleton _sampleSingleton;
        private readonly ILogger _logger;

        public HomeController(ISampleTransient sampleTransient, ISampleScoped sampleScoped, ISampleSingleton sampleSingleton, ILoggerFactory loggerFactory)
        {
            _sampleTransient = sampleTransient;
            _sampleScoped = sampleScoped;
            _sampleSingleton = sampleSingleton;
            _logger = loggerFactory.CreateLogger<ManageController>();
        }

        public IActionResult Index()
        {
            var message = $"<tr><td>Transient</td><td>{_sampleTransient.GetNumber()}</td></tr>"
                       + $"<tr><td>Scoped</td><td>{_sampleScoped.GetNumber()}</td></tr>"
                       + $"<tr><td>Singleton</td><td>{_sampleSingleton.GetNumber()}</td></tr>";
            return View(model: message);

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public int Number()
        {
            return _sampleTransient.GetNumber();
        }
    }
}
