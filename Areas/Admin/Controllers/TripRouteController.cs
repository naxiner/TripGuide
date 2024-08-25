using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripGuide.Data;
using TripGuide.Models;
using TripGuide.Repositories;
using TripGuide.Repository;
using TripGuide.Utility;

namespace TripGuide.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = (StaticDetail.Role_Admin + "," + StaticDetail.Role_Moderator))]
    public class TripRouteController : Controller
    {
        private readonly TripGuideDbContext context;
        private readonly ITripRouteRepository tripRouteRepository;
        private readonly ITouristObjectRepository touristObjectRepository;

        [BindProperty]
        public TripRoute TripRoute { get; set; }
        public List<Guid> TouristObjectIds { get; set; }

        public TripRouteController(TripGuideDbContext context, ITripRouteRepository tripRouteRepository, ITouristObjectRepository touristObjectRepository)
        {
            this.context = context;
            this.tripRouteRepository = tripRouteRepository;
            this.touristObjectRepository = touristObjectRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var touristObjects = touristObjectRepository.GetAll().ToList();
            ViewBag.TouristObjects = touristObjects;
            return View();
        }

        [HttpPost]
        public IActionResult Add(TripRoute tripRoute, List<Guid> TouristObjectIds)
        {
            if (TouristObjectIds != null && TouristObjectIds.Count >= 2)
            {
                tripRoute = tripRouteRepository.Add(tripRoute);

                foreach (var touristObjectId in TouristObjectIds)
                {
                    var tripRouteTouristObject = new TripRouteTouristObject
                    {
                        TripRouteId = tripRoute.Id,
                        TouristObjectId = touristObjectId
                    };
                    context.TripRouteTouristObjects.Add(tripRouteTouristObject);
                }
                context.SaveChanges();
            }

            ViewBag.TouristObjects = touristObjectRepository.GetAll();
            return View("Create");
        }
    }
}