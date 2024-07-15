using Demo.DAL.Data;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly AppDbContext _context;
        public ContactUsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ContactUs(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contactUsModel = new ContactUs
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Message = model.Message
                };

                _context.ContactUs.Add(contactUsModel);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

    }
}
