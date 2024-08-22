using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using TripGuide.Data;
using TripGuide.Models;
using TripGuide.Models.ViewModels;
using TripGuide.Repositories;
using TripGuide.Repository;
using TripGuide.Utility;

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
        private readonly GoogleMapsSettings _googleMapsSettings;
        private readonly TripGuideDbContext context;
        private readonly IUserRepository userRepository;

        public List<BlogPost> Blogs { get; set; }

        [BindProperty]
        public List<Tag> Tags { get; set; }

        public HomeController(ILogger<HomeController> logger,
                      IBlogPostRepository blogPostRepository,
                      ITagRepository tagRepository,
                      IReviewRepository reviewRepository,
                      UserManager<User> userManager,
                      IOptions<GoogleMapsSettings> googleMapsSettings,
                      TripGuideDbContext context,
                      IUserRepository userRepository)
        {
            _logger = logger;
            _blogPostRepository = blogPostRepository;
            _tagRepository = tagRepository;
            _reviewRepository = reviewRepository;
            _userManager = userManager;
            _googleMapsSettings = googleMapsSettings.Value;
            this.context = context;
            this.userRepository = userRepository;
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

            ViewBag.GoogleMapsApiKey = _googleMapsSettings.ApiKey;

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

        [HttpPost]
        [Area("User")]
        [Route("blog/{urlHandle}/edit-review")]
        public IActionResult EditReview(string urlHandle, Review review)
        {
            var existingReview = _reviewRepository.GetById(review.Id);
            if (existingReview == null)
            {
                return NotFound();
            }

            var user = _userManager.GetUserAsync(User).Result;
            if (user == null || user.Id != existingReview.UserId)
            {
                return Unauthorized();
            }

            existingReview.Content = review.Content;
            existingReview.Rating = review.Rating;

            _reviewRepository.Update(existingReview);

            return RedirectToAction("BlogDetails", new { urlHandle = urlHandle });
        }

        [HttpPost]
        [Area("User")]
        [Route("blog/{urlHandle}/delete-review")]
        public IActionResult DeleteReview(string urlHandle, Guid reviewId)
        {
            var review = _reviewRepository.GetById(reviewId);
            if (review == null)
            {
                return NotFound();
            }

            var user = _userManager.GetUserAsync(User).Result;
            if (user == null || user.Id != review.UserId)
            {
                return Unauthorized();
            }

            _reviewRepository.Delete(reviewId);

            return RedirectToAction("BlogDetails", new { urlHandle = urlHandle });
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser == null)
                {
                    return Unauthorized("User is not logged in.");
                }

                userId = currentUser.Id;
            }

            var user = await userRepository.GetAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var model = new UserRolesViewModel
            {
                UserName = user.UserName,
                AvatarImageUrl = user.AvatarImageUrl,
                UserId = user.Id
            };

            return View(model);
        }

    }
}