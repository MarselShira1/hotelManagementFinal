//sing hotelManagamentFinal.Models.DTO.Profile;
using hotelManagamentFinal.Models1;
using HotelManagementFinal.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using hotelManagement.BLL.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace hotelManagamentFinal.Controllers
{

    public class Profile1Controller : Controller
    {
        private readonly IUserService _userService;
        private readonly IBookingService _bookingService;

        public Profile1Controller(IUserService userService, IBookingService bookingService)
        {
            _userService = userService;
            _bookingService = bookingService;
        }

        public async Task<IActionResult> ProfileView()
        {
            //var user = await _userService.GetCurrentUserAsync(); // Fetch the current user
            //var reservations = await _bookingService.GetUserReservationsAsync(user.Id); // Fetch user's reservations

            //var viewModel = new ProfileViewModel
            //{
            //    UserName = user.UserName,
            //    Email = user.Email,
            //    Reservations = reservations
            //};

            var viewModel = new ProfileViewModel();


            return View(viewModel);
        }

        //    [HttpPost]
        //    public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View("ProfileView", new ProfileViewModel { ChangePasswordModel = model });
        //        }

        //        var result = await _userService.ChangePasswordAsync(User.Identity.Name, model.CurrentPassword, model.NewPassword);

        //        if (!result)
        //        {
        //            ModelState.AddModelError("", "Password change failed. Please check your current password.");
        //            return View("ProfileView", new ProfileViewModel { ChangePasswordModel = model });
        //        }

        //        TempData["SuccessMessage"] = "Password changed successfully!";
        //        return RedirectToAction("ProfileView");
        //    }

        //    public IActionResult Logout()
        //    {
        //        _userService.Logout();
        //        return RedirectToAction("Login", "Auth");
        //    }
        //}
    }
}


