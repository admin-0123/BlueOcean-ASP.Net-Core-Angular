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
using Microsoft.AspNetCore.Mvc.Rendering;
using Virta.Data.Interfaces;
using Virta.Models;
using Virta.MVC.ViewModels;
using Virta.Services.Interfaces;

namespace Virta.MVC.Controllers
{
    [Route("Admin")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AdminController : Controller
    {
        public const string INCORRECT_CREDENTIALS = "Email or Password is Incorrect.";

        private readonly UserManager<Virta.Entities.User> _userManager;
        private readonly SignInManager<Virta.Entities.User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productRepo;
        private readonly ICategoriesRepository _categoriesRepo;
        private readonly IProductService _productService;

        public AdminController(
            UserManager<Virta.Entities.User> userManager,
            SignInManager<Virta.Entities.User> signInManager,
            IMapper mapper,
            IProductsRepository productRepo,
            ICategoriesRepository categoriesRepo,
            IProductService productService
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _productRepo = productRepo;
            _productService = productService;
            _categoriesRepo = categoriesRepo;
        }

        [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> Login(AdminVM data)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(data.Email);

                if (user == null)
                {
                    ViewBag.error = INCORRECT_CREDENTIALS;
                    return View("Index");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, data.Password, false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                    if (roles.Count > 0)
                    {
                        foreach (var r in roles)
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, r));
                        }
                    }
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                        new ClaimsPrincipal(identity));

                    return RedirectToAction("Index");
                }

                ViewBag.error = INCORRECT_CREDENTIALS;
            }

            return View("Index");
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
            var categories = await _categoriesRepo.GetCategories();

            ViewBag.Categories = _mapper.Map<IEnumerable<SelectListItem>>(categories);

            if (product == null)
                return RedirectToAction("Index");

            var res = _mapper.Map<ProductUpsertVM>(product);

            return View("Product", res);
        }

        [Route("product/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductUpsertVM productVM)
        {
            var product = _mapper.Map<ProductUpsert>(productVM);

            if (await _productService.UpsertProduct(product))
                return RedirectToAction("Index");

            return RedirectToAction("GetProduct");
        }

        private async Task<IEnumerable<ProductPLPVM>> GetProducts()
        {
            var products = await _productRepo.GetProducts();

            if (products == null)
                return null;

            return _mapper.Map<IEnumerable<ProductPLPVM>>(products);
        }
    }
}
