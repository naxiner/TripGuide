using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TripGuide.Models;
using TripGuide.Models.ViewModels;
using TripGuide.Repositories;
using TripGuide.Repository;

namespace TripGuide.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly UserManager<User> _userManager;

        public List<BlogPost> Blogs { get; set; }

        [BindProperty]
        public List<Tag> Tags { get; set; }

        public HomeController(ILogger<HomeController> logger,
                      IBlogPostRepository blogPostRepository,
                      ITagRepository tagRepository,
                      IReviewRepository reviewRepository,
                      UserManager<User> userManager)
        {
            _logger = logger;
            _blogPostRepository = blogPostRepository;
            _tagRepository = tagRepository;
            _reviewRepository = reviewRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Blogs = _blogPostRepository.GetAll().ToList(),
                Tags = _tagRepository.GetAll().ToList()
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
            var blog = _blogPostRepository.GetByUrl(urlHandle);
            if (blog == null)
            {
                return NotFound();
            }

            var reviews = _reviewRepository.GetByBlogPostId(blog.Id).ToList();

            foreach (var review in reviews)
            {
                var user = _userManager.FindByIdAsync(review.UserId).Result;
                review.UserName = user?.UserName;
            }

            var viewModel = new BlogDetailsViewModel
            {
                BlogPost = blog,
                Reviews = reviews
            };

            return View("BlogDetails", viewModel);
        }

        [HttpPost]
        [Area("User")]
        [Route("blog/{urlHandle}/add-review")]
        public IActionResult AddReview(string urlHandle, Review review)
        {
            var blog = _blogPostRepository.GetByUrl(urlHandle);
            if (blog == null)
            {
                return NotFound();
            }

            var user = _userManager.GetUserAsync(User).Result;
            if (user == null)
            {
                return Unauthorized();
            }

            review.Id = Guid.NewGuid();
            review.DateAdded = DateTime.UtcNow;
            review.BlogPostId = blog.Id;
            review.UserId = user.Id;

            _reviewRepository.Add(review);

            return RedirectToAction("BlogDetails", new { urlHandle = urlHandle });
        }

    }
}