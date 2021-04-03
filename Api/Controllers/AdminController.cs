using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Virta.ViewModels;
using VirtaApi.Data.Interfaces;


namespace Virta.Controllers
{
    [Route("Admin")]
    [ApiExplorerSettings(IgnoreApi=true)]
    public class AdminController : Controller
    {
        public const string INCORRECT_CREDENTIALS = "Email or Password is Incorrect.";

        private readonly UserManager<VirtaApi.Models.User> _userManager;
        private readonly SignInManager<VirtaApi.Models.User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productRepo;

        public AdminController(
            UserManager<VirtaApi.Models.User> userManager,
            SignInManager<VirtaApi.Models.User> signInManager,
            IMapper mapper,
            IProductsRepository productRepo
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _productRepo = productRepo;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Products"] = await GetProducts();
            return View("Dashboard");
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Index");
        }

        [Route("login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Admin data)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(data.Email);

                if (user == null)
                    ViewBag.error = INCORRECT_CREDENTIALS;

                var result = await _signInManager.CheckPasswordSignInAsync(user, data.Password, false);

                if (result.Succeeded) {
                    var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                        new ClaimsPrincipal(identity));
                    return RedirectToAction("Index");
                }

                ViewBag.error = INCORRECT_CREDENTIALS;
            }

            return RedirectToAction("login");
        }

        [Route("logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // HttpContext.Session.Remove("email");
            await _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }

        [Route("product/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetProduct(Guid Id)
        {
            var product = await _productRepo.GetProduct(Id);

            if(product == null)
                return RedirectToAction("Index");

            var response = _mapper.Map<Product>(product);

            return View("Product", response);
        }

        [Route("product/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var product2 = await _productRepo.GetProduct(product.Id);

            if(product2 == null)
                return RedirectToAction("Index");

            var response = _mapper.Map<Product>(product2);

            return View("Product", response);
        }

        private async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _productRepo.GetProducts();

            if(products == null)
                return null;

            return _mapper.Map<IEnumerable<Product>>(products);
        }
    }
}
