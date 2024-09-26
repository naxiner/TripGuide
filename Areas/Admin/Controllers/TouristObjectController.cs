using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using TripGuide.Models;
using TripGuide.Models.ViewModels;
using TripGuide.Repositories;
using TripGuide.Utility;

namespace TripGuide.Controllers
{
    [Area("Admin")]
    [Authorize(Roles=(StaticDetail.Role_Admin + "," + StaticDetail.Role_Moderator))]
    public class TouristObjectController : Controller
    {
        private readonly ITouristObjectRepository _touristObjectRepository;
        private readonly GoogleMapsSettings _googleMapsSettings;

        [BindProperty]
        public TouristObject TouristObject { get; set; }

        public TouristObjectController(ITouristObjectRepository touristObjectRepository, IOptions<GoogleMapsSettings> googleMapsSettings)
        {
            _touristObjectRepository = touristObjectRepository;
            _googleMapsSettings = googleMapsSettings.Value;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            ViewBag.GoogleMapsApiKey = _googleMapsSettings.ApiKey;
            return View();
        }

        [HttpPost]
        public IActionResult Add()
        {
            ValidateTouristObject();

            if (ModelState.IsValid)
            {
                var touristObject = new TouristObject()
                {
                    Name = TouristObject.Name,
                    Address = TouristObject.Address,
                    Latitude = TouristObject.Latitude,
                    Longitude = TouristObject.Longitude,
                    OpeningTime = TouristObject.OpeningTime,
                    ClosingTime = TouristObject.ClosingTime,
                };

                _touristObjectRepository.Add(touristObject);

                return RedirectToAction("Create");
            }

            return View("Create");
        }

        public IActionResult Update(Guid id)
        {
            var touristObject = _touristObjectRepository.Get(id);
            if (touristObject == null)
            {
                return NotFound();
            }
            ViewBag.GoogleMapsApiKey = _googleMapsSettings.ApiKey;
            return View(touristObject);
        }

        [HttpPost]
        public IActionResult Update(Guid id, TouristObject touristObject)
        {
            ValidateTouristObject();

            if (ModelState.IsValid)
            {
                var touristObjectToUpdate = _touristObjectRepository.Get(id);
                if (touristObjectToUpdate == null)
                {
                    return NotFound();
                }

                touristObjectToUpdate.Name = touristObject.Name;
                touristObjectToUpdate.Address = touristObject.Address;
                touristObjectToUpdate.Latitude = touristObject.Latitude;
                touristObjectToUpdate.Longitude = touristObject.Longitude;
                touristObjectToUpdate.OpeningTime = touristObject.OpeningTime;
                touristObjectToUpdate.ClosingTime = touristObject.ClosingTime;

                _touristObjectRepository.Update(touristObjectToUpdate);

                return RedirectToAction("List");
            }

            return View(touristObject);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var result = _touristObjectRepository.Delete(id);
            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("List");
        }

        public IActionResult List(int pageNumber = 1)
        {
            int pageSize = 5;
            var allTouristObjects = _touristObjectRepository.GetAll().ToList();

            int totalTouristObjects = allTouristObjects.Count();
            int totalPages = (int)Math.Ceiling(totalTouristObjects / (double)pageSize);

            var pagedTouristObjects = allTouristObjects
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new TouristObjectViewModel
            {
                TouristObjects = pagedTouristObjects,
                PageNumber = pageNumber,
                TotalPages = totalPages
            };

            return View(viewModel);
        }


        private void ValidateTouristObject()
        {
            if (TouristObject.OpeningTime > TouristObject.ClosingTime)
            {
                ModelState.AddModelError("TouristObject.OpeningTime", "Opening time cannot be later than closing time.");
            }
        }

        [HttpGet("api/touristobject/{id}")]
        public IActionResult GetTouristObject(Guid id)
        {
            var touristObject = _touristObjectRepository.Get(id);
            if (touristObject == null)
            {
                return NotFound();
            }
            return Json(touristObject);
        }
    }
}