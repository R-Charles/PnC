using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductsAndCategories.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
    [HttpGet("/products")]
    public IActionResult Index()
    {
        List<Product> AllProducts = _context.Products.OrderBy(p => p.Name).ToList();
        ViewBag.Products = AllProducts;
        return View("Index");
    }

    [HttpGet("/Products/all")]
    public IActionResult All()
    {
        List<Product> AllProducts = _context.Products.ToList();

        return View("All", AllProducts);
    }

    [HttpPost("/create/product")]
    public IActionResult Create(Product newProduct)
    {
        if (ModelState.IsValid == false)
        {
            return View("Index");
        }

        _context.Add(newProduct);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpGet("/products/{id}")]
    public IActionResult ProductDetail(int id)
    {
        // ViewBag.AllListedCategories = _context.Categories.Include(c => c.CatWithProducts).ThenInclude(p => p.Product).Where(c => !c.CatWithProducts.Any(p => p.ProductId == ProductId));

        Product? CatInfoListedCategories = _context.Products.Include(c => c.AllCategories).ThenInclude(c => c.Categories).FirstOrDefault(c => c.ProductId == id);
        
        ViewBag.ListedCategories = _context.Categories.Include(c => c.AllProducts).ThenInclude(c => c.Products).Where(p => !p.AllProducts.Any(c => c.ProductId == id)).ToList();

        ViewBag.AssociatedCategories = _context.Categories.Include(c => c.AllProducts).ThenInclude(c => c.Products).Where(c => c.AllProducts.Any(c => c.ProductId == id)).ToList();


        ViewBag.SingleProduct = _context.Products.FirstOrDefault(p => p.ProductId == id);

        return View("ProductsViewOne", CatInfoListedCategories);
    }
    
    [HttpPost("/products/{id}/add")]
    public IActionResult AddProduct(Product product)
    {
        if(ModelState.IsValid)
        {
            product.UpdatedAt = DateTime.Now;
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("ProductsViewOne");
        }
        return RedirectToAction("ProductsViewOne");
    }

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