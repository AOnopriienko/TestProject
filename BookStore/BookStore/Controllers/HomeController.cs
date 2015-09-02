using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        BookContext db = new BookContext();

        public ActionResult Index()
        {
            //Получаем из БД все объекты Book
            IEnumerable<Book> books = db.Books;
            //Передаем все полученные объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            //возвращаем представление
            return View();
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            //добавляем информацию о покупке в бд
            db.Purchases.Add(purchase);
            //сохраняем в бд изменения
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку";
        }
    }
}
