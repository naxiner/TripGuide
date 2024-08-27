using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TripGuide.Models;
using TripGuide.Models.ViewModels;
using TripGuide.Repositories;
using TripGuide.Utility;

namespace TripGuide.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = (StaticDetail.Role_Admin + "," + StaticDetail.Role_Moderator))]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        [BindProperty]
        public User User { get; set; }

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            if (users == null)
            {
                return NotFound();
            }

            var userRolesViewModels = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userRepository.GetUserRolesAsync(user);
                userRolesViewModels.Add(new UserRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    AccountVerified = user.AccountVerified,
                    IsBanned = user.LockoutEnd != null && user.LockoutEnd > DateTime.UtcNow,
                    Roles = roles
                });
            }

            return View(userRolesViewModels);
        }

        public async Task<IActionResult> Update(string id)
        {
            var user = await _userRepository.GetAsync(id);
            var allRoles = await _userRepository.GetAllRolesAsync();
            if (user == null)
            {
                return NotFound();
            }

            var role = await _userRepository.GetUserRolesAsync(user);
            var userRolesVM = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                AccountVerified=user.AccountVerified,
                AvatarImageUrl = user.AvatarImageUrl,
                Roles = role,
                AllRoles = allRoles
            };

            return View(userRolesVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserRolesViewModel userRolesVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetAsync(userRolesVM.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                var userRoles = await _userRepository.GetUserRolesAsync(user);

                var rolesToAdd = userRolesVM.Roles.Except(userRoles).ToList();
                var rolesToRemove = userRoles.Except(userRolesVM.Roles).ToList();

                if (rolesToAdd.Any())
                {
                    await _userRepository.AddToRolesAsync(user, rolesToAdd);
                }

                if (rolesToRemove.Any())
                {
                    await _userRepository.RemoveFromRolesAsync(user, rolesToRemove);
                }

                user.UserName = userRolesVM.UserName;
                user.Email = userRolesVM.Email;
                user.PhoneNumber = userRolesVM.PhoneNumber;
                user.AccountVerified = userRolesVM.AccountVerified;
                user.AvatarImageUrl = userRolesVM.AvatarImageUrl;

                await _userRepository.UpdateAsync(user);

                return RedirectToAction(nameof(Index));
            }

            userRolesVM.AllRoles = await _userRepository.GetAllRolesAsync();
            return View(userRolesVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LockUnlock(string id, string banDuration)
        {
            var result = await _userRepository.LockUnlock(id, banDuration);
            return RedirectToAction("Index");
        }
    }
}
