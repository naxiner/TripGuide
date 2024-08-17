using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TripGuide.Models;
using TripGuide.Models.ViewModels;
using TripGuide.Repositories;

namespace TripGuide.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRepository tagRepository;

        public List<BlogPost> Blogs { get; set; }

        [BindProperty]
        public List<Tag> Tags { get; set; }

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
            this.tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Blogs = blogPostRepository.GetAll().ToList(),
                Tags = tagRepository.GetAll().ToList()
            };

            return View(viewModel);
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