using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductsAndCategories.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProductsAndCategories.Controllers;
public class CategoriesController : Controller
{
private MyContext _context;
    public CategoriesController(MyContext context)

    {
        _context = context;
    }

    //here we can "inject" our context service into the constructor


    [HttpGet("Category")]
    public IActionResult Category()
    {
        return View("Category");
    }

    [HttpGet("/Categories/all")]
    public IActionResult All()
    {
        List<Category> AllCategories = _context.Categories.ToList();
        ViewBag.Categories = AllCategories;

        return View("Category");
    }
    //returning a view = returning a .cshtml file vs a method

    [HttpPost("/create_category")]
    public IActionResult Create(Category newCategory)
    {
        if (ModelState.IsValid == false)
        {
            return View("Category");
        }

        _context.Add(newCategory);
        _context.SaveChanges();

        return RedirectToAction("All");
    }

    // [HttpGet("/categories/{id}")]
    // public IActionResult CategoryDetail(int id)
    // {
    //     // ViewBag.AllListedCategories = _context.Categories.Include(c => c.CatWithProducts).ThenInclude(p => p.Product).Where(c => !c.CatWithProducts.Any(p => p.ProductId == ProductId));

    //     Category? CatInfoListedProducts = _context.Categories.Include(c => c.AllProducts).ThenInclude(c => c.Products).FirstOrDefault(c => c.CategoryId == id);
        
    //     ViewBag.ListedProducts = _context.Products.Include(c => c.AllCategories).ThenInclude(c => c.Categories).Where(p => !p.AllCategories.Any(c => c.CategoryId == id)).ToList();

    //     ViewBag.AssociatedProducts = _context.Products.Include(c => c.AllCategories).ThenInclude(c => c.Categories).Where(c => c.AllCategories.Any(c => c.CategoryId == id)).ToList();


    //     ViewBag.SingleCategory = _context.Categories.FirstOrDefault(p => p.CategoryId == id);

    //     return View("CategoryViewOne", CatInfoListedProducts);
    // }

    [HttpGet("/categories/{id}")]
    public IActionResult CategoryDetail(int Id)
    {

    Category? category = _context.Categories
        .Include(f => f.AllProducts)
        .ThenInclude(assoc => assoc.Product)
        .FirstOrDefault(category => category.CategoryId == Id);
        if (category == null)
        {
            return RedirectToAction("All");
        }

        ViewBag.UnrelatedProds = _context.Products
        .Include(c => c.AllCategories)
        .ThenInclude(a => a.Category)
        .Where(c => !c.AllCategories
        .Any(p => p.CategoryId == categoryId));

        return View("OneCategory", category);

    [HttpPost("/categories/add")]
    public IActionResult AddCatToProduct(Association newAss, int CategoryId)
    {
        if(ModelState.IsValid)
        {
            _context.Categories.Add( newAss );
            _context.SaveChanges();
            return RedirectToAction("CategoryViewOne");
        }
        return RedirectToAction("CategoryViewOne", new{CategoryId = CategoryId});
    }

//     [HttpPost("/login")]
//     public IActionResult Login(LoginProduct loginProduct)
//     {
//         if (ModelState.IsValid == false)
//         {
//             return Index();
//         }

//         Product? _contextProduct = _context.Categories.FirstOrDefault(Product=> Product.Email == loginProduct.LoginEmail);

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