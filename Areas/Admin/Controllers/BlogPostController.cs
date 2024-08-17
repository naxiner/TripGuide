﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using TripGuide.Data;
using TripGuide.Models;
using TripGuide.Repositories;
using TripGuide.Repository;
using TripGuide.Utility;

namespace TripGuide.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = (StaticDetail.Role_Admin + "," + StaticDetail.Role_Moderator))]
    public class BlogPostController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IImageRepository imageRepository;
        private readonly ITouristObjectRepository touristObjectRepository;
        private readonly TripGuideDbContext context;

        [BindProperty]
        public BlogPost BlogPost { get; set; }

        [BindProperty]
        public IFormFile? FeaturedImage { get; set; }

        [BindProperty]
        public string Tags { get; set; }

        public BlogPostController(IBlogPostRepository blogPostRepository, IImageRepository imageRepository, ITouristObjectRepository touristObjectRepository, TripGuideDbContext context)
        {
            this.blogPostRepository = blogPostRepository;
            this.imageRepository = imageRepository;
            this.touristObjectRepository = touristObjectRepository;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var touristObjects = context.TouristObjects.ToList();
            ViewBag.TouristObjects = touristObjects;
            return View();
        }

        [HttpPost]
        public IActionResult Add()
        {
            var tagList = Tags.Split(',').Select(x => x.Trim()).ToList();

            if (tagList.Count > 10)
            {
                ModelState.AddModelError("Tags", "You cannot add more than 10 tags.");
            }

            if (ModelState.IsValid)
            {
                var blogPost = new BlogPost()
                {
                    PageTitle = BlogPost.PageTitle,
                    Content = BlogPost.Content,
                    ShortDescription = BlogPost.ShortDescription,
                    FeaturedImageUrl = BlogPost.FeaturedImageUrl,
                    UrlHandle = BlogPost.UrlHandle,
                    PublishedDate = BlogPost.PublishedDate,
                    UserId = BlogPost.UserId,
                    TouristObjectId = BlogPost.TouristObjectId,
                    Tags = tagList.Select(x => new Tag() { Name = x }).ToList()
                };
                blogPostRepository.Add(blogPost);

                return RedirectToAction("List");
            }

            ViewBag.TouristObjects = touristObjectRepository.GetAll();

            return View("Create");
        }

        public IActionResult Update(Guid id)
        {
            var blogPost = blogPostRepository.Get(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            if (blogPost.Tags != null)
            {
                Tags = string.Join(',', blogPost.Tags.Select(x => x.Name));
            }

            ViewBag.TouristObjects = blogPostRepository.GetAllTouristObjects();

            return View(blogPost);
        }

        [HttpPost]
        public IActionResult Update(Guid id, BlogPost updatedBlogPost)
        {
            var tagList = Tags.Split(',').Select(x => x.Trim()).ToList();

            if (tagList.Count > 10)
            {
                ModelState.AddModelError("Tags", "You cannot add more than 10 tags.");
            }

            if (ModelState.IsValid)
            {
                var existingBlogPost = blogPostRepository.Get(id);
                if (existingBlogPost == null)
                {
                    return NotFound();
                }

                existingBlogPost.PageTitle = updatedBlogPost.PageTitle;
                existingBlogPost.Content = updatedBlogPost.Content;
                existingBlogPost.ShortDescription = updatedBlogPost.ShortDescription;
                existingBlogPost.UrlHandle = updatedBlogPost.UrlHandle;
                existingBlogPost.FeaturedImageUrl = updatedBlogPost.FeaturedImageUrl;
                existingBlogPost.PublishedDate = updatedBlogPost.PublishedDate;
                existingBlogPost.UserId = updatedBlogPost.UserId;
                existingBlogPost.TouristObjectId = updatedBlogPost.TouristObjectId;
                existingBlogPost.Reviews = updatedBlogPost.Reviews;

                existingBlogPost.Tags = tagList.Select(x => new Tag() { Name = x }).ToList();

                blogPostRepository.Update(existingBlogPost);

                return RedirectToAction("List");
            }

            return View(updatedBlogPost);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var result = blogPostRepository.Delete(id);
            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var blogPosts = blogPostRepository.GetAll();
            return View(blogPosts);
        }
    }
}