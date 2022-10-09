using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductsAndCategories.Models;
using Microsoft.AspNetCore.Identity;

namespace ProductsAndCategories.Controllers;
public class ProductsController : Controller
{
private MyContext _context;
    public ProductsController(MyContext context)

    {
        _context = context;
    }

    //here we can "inject" our context service into the constructor


    [HttpGet("")]
    public IActionResult Index()
    {
        return View("Index");
    }

    [HttpGet("/Products/all")]
    public IActionResult All()
    {
        List<Product> AllProducts = _context.Products.ToList();

        return View("All", AllProducts);
    }

    // [HttpPost("/register")]
    // public IActionResult Register(Product newProduct)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         if (_context.Products.Any(Product => Product.Email == newProduct.Email))
    //         {
    //             ModelState.AddModelError("Email", "is taken");
    //         }
    //     }
    //     if (ModelState.IsValid == false)
    //     {
    //         return Index();
    //     }

    //     //now we have to hash
    //     PasswordHasher<Product> hashBrowns = new PasswordHasher<Product>();
    //     newProduct.Password = hashBrowns.HashPassword(newProduct, newProduct.Password);

    //     _context.Add(newProduct);
    //     _context.SaveChanges();

    //     //now that we've run SaveChanges() we have access to the ProductId from our SQL
    //     HttpContext.Session.SetInt32("UUID", newProduct.ProductID);
    //     HttpContext.Session.SetString("Email", newProduct.Email);

    //     return RedirectToAction("All", "Product");
    // }

//     [HttpPost("/login")]
//     public IActionResult Login(LoginProduct loginProduct)
//     {
//         if (ModelState.IsValid == false)
//         {
//             return Index();
//         }

//         Product? _contextProduct = _context.Products.FirstOrDefault(Product=> Product.Email == loginProduct.LoginEmail);

//         if (_contextProduct == null)
//         {
//             //make validations obscure for the sake of security(ie fishing for passwords)
//             //only specific for testing
//             ModelState.AddModelError("LoginEmail", "not found");
//             return Index();
//         }

//         PasswordHasher<LoginProduct> hashBrowns = new PasswordHasher<LoginProduct>();
//         PasswordVerificationResult pwCompareResult = hashBrowns.VerifyHashedPassword
//         (loginProduct, _contextProduct.Password, loginProduct.LoginPassword);

//         if (pwCompareResult == 0 )
//         {
//             ModelState.AddModelError("LoginPassword", "is not correct");
//             return Index();
//         }
//         //no returns, therefore no errors
//         HttpContext.Session.SetInt32("UUID", _contextProduct.ProductID);
//         HttpContext.Session.SetString("Name", _contextProduct.FullName());
//         return RedirectToAction("All", "Product");
//     }
//     [HttpPost("/logout")]
//     public IActionResult Logout()
//     {
//         HttpContext.Session.Clear();
//         return RedirectToAction("Index");
//     }
        
}