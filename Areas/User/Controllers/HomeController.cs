using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TripGuide.Models;
using TripGuide.Repositories;

namespace TripGuide.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;

        public IEnumerable<BlogPost> Blogs { get; set; }

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository)
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
        }

        public IActionResult Index()
        {
            var blogs = blogPostRepository.GetAll();
            return View(blogs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Area("User")]
        [Route("blog/{urlHandle}")]
        public IActionResult BlogDetails(string urlHandle)
        {
            var blog = blogPostRepository.GetByUrl(urlHandle);
            if (blog == null)
            {
                return NotFound();
            }
            return View("BlogDetails", blog);
        }
    }
}