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
        public string Square(int a, int h)
        {
            double s = a * h / 2;
            return "<h2>Площадь треугольника с основанием " + a +
                    " и высотой " + h + " равна " + s + "</h2>";
        }
        public string Square()
        {
            int a = Int32.Parse(Request.Params["a"]);
            int h = Int32.Parse(Request.Params["h"]);
            double s = a * h / 2;
            return "<h2>Площадь треугольника с основанием " + a + " и высотой " + h + " равна " + s + "</h2>";
        }
    }
}
