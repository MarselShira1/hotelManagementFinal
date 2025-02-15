using System.Diagnostics;
using HotelManagamentFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
namespace hotelManagamentFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StripeSettings _stripeSettings;

        public HomeController(ILogger<HomeController> logger,IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreateCheckoutSession(string amount)
        {
            var currency = "usd"; // Currency code
            var successUrl = Url.Action("Success", "Home", null, Request.Scheme);
            var cancelUrl = Url.Action("Cancel", "Home", null, Request.Scheme);
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
        {
            "card"
        },
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = currency,
                    UnitAmount = Convert.ToInt32(amount) * 100,  // Amount in smallest currency unit
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Product Name",
                        Description = "Product Description"
                    }
                },
                Quantity = 1
            }
        },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        public IActionResult Success()
        {
            return View("index");
        }
        

        public IActionResult Cancel()
        {
            return View("index");
        }

    }
}
