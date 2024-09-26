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
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IUserBlogPostRepository _userBlogPostRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly UserManager<User> _userManager;

        public ProfileController(
            IUserRepository userRepository,
            IBlogPostRepository blogPostRepository,
            IUserBlogPostRepository userBlogPostRepository,
            IReviewRepository reviewRepository,
            UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _blogPostRepository = blogPostRepository;
            _userBlogPostRepository = userBlogPostRepository;
            _reviewRepository = reviewRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            string userId = _userManager.GetUserId(User);
            var user = await _userRepository.GetAsync(userId);

            var collections = _userBlogPostRepository.GetAllUserBlogs(userId);

            int pageSize = 3;
            var allReviews = _reviewRepository.GetAllByUserId(userId);
            var totalReviews = allReviews.Count();
            var totalPages = (int)Math.Ceiling(totalReviews / (double)pageSize);

            var pagedReviews = allReviews
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new UserProfileViewModel
            {
                User = user,
                Collections = collections.ToList(),
                Reviews = pagedReviews,
                PageNumber = pageNumber,
                TotalPages = totalPages
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveToProfile(Guid blogId, string collection)
        {
            string userId = _userManager.GetUserId(User);

            var blog = _blogPostRepository.Get(blogId);
            if (blog == null)
            {
                return NotFound();
            }

            var userBlogPost = new UserBlogPost
            {
                UserId = userId,
                BlogPostId = blogId,
                Status = collection
            };

            _userBlogPostRepository.Add(userBlogPost);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid blogId)
        {
            string userId = _userManager.GetUserId(User);

            var blog = _blogPostRepository.Get(blogId);
            if (blog == null)
            {
                return NotFound();
            }

            _userBlogPostRepository.Delete(blogId, userId);

            return RedirectToAction("Index");
        }
    }
}
