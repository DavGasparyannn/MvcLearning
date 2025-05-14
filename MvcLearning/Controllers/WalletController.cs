using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcLearning.Business.Services;
using MvcLearning.Data.Entities;

namespace MvcLearning.Controllers
{
    public class WalletController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly BalanceService _balanceService;
        public WalletController(UserManager<User> userManager, BalanceService balanceService)
        {
            _userManager = userManager;
            _balanceService = balanceService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            return View(user.Balance);
        }
        [HttpPost]
        public async Task<IActionResult> Deposit(decimal amount, string description)
        {
            if (amount <= 0)
            {
                TempData["ErrorMessage"] = "Amount must be greater than zero.";
                return RedirectToAction("Index");
            }
            var user = await _userManager.GetUserAsync(User);
            try
            {
                await _balanceService.DepositToBalanceAsync(user.Id, amount, description); // Делаем пополнение через BalanceService

                TempData["SuccessMessage"] = "Deposit successful!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
