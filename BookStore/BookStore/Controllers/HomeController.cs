using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.Util;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;

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

        public ActionResult GetHtml(int i)
        {
            HttpContext.Response.Cookies["ID"].Value = "Hello from MVC";
            if (i == 1)
            {
                //return new HttpStatusCodeResult(404);
                return new HttpUnauthorizedResult();
            }
            else if (i == 3) 
            {
                return new HtmlResult("<h2>" + IndexString() + "</h2>");
            }
            else
            {
                return new HtmlResult("<h2>Привет мир!</h2>");
            }
        }

        public ActionResult GetImage()
        {
            string path = "../Images/visualstudio.png";
            return new ImageResult(path);
        }
        public FileResult GetFile()
        {
            // Путь к файлу
            string file_path = Server.MapPath("~/Images/visualstudio.png");
            // Тип файла - content-type
            string file_type = "application/png";
            // Имя файла - необязательно
            string file_name = "visualstudio.png";
            return File(file_path, file_type, file_name);
        }
        // Отправка массива байтов
        public FileResult GetBytes()
        {
            string path = Server.MapPath("~/Images/visualstudio.png");
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string file_type = "application/png";
            string file_name = "visualstudio.png";
            return File(mas, file_type, file_name);
        }
        // Отправка потока
        public FileResult GetStream()
        {
            string path = Server.MapPath("~/Images/visualstudio.png");
            // Объект Stream
            FileStream fs = new FileStream(path, FileMode.Open);
            string file_type = "application/png";
            string file_name = "visualstudio.png";
            return File(fs, file_type, file_name);
        }

        public string IndexString()
        {
            string browser = HttpContext.Request.Browser.Browser;
            string user_agent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string cookiesID = HttpContext.Request.Cookies["ID"].Value;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            return "<p>Browser: " + browser + "</p><p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
                "</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p><p>ID from Cookies: " + cookiesID + "</p>";
        }

        // асинхронный метод
        public async Task<ActionResult> BookList()
        {
            IEnumerable<Book> books = await Task.Run(() => db.Books);
            ViewBag.Books = books;
            return View("Index");
        }
    }
}
