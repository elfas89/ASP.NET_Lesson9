using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Less_9.Controllers
{
    public class RedirectController : Controller
    {

        //Напишите метод действия контроллера, который принимает X(число). 
        //Если X меньше или равно 0, происходит переадресация на другой метод действия, который возвращает X * 2, 
        //в противном случае идет переадресация на метод действия, который возвращает X * X.


        // GET: Redirect
        public ActionResult Index(string i = "-1")
        {
            int j = Int32.Parse(i);
            if (j <= 0)
            {
                //return Redirect("/Redirect/M2");
                return RedirectToAction("M2", "Redirect", new { k = j });
            }
            else
            {
                return RedirectToAction("M3", "Redirect", new { k = j });
            }

        }


        public int M2 (int k)
        {
            //int j = Int32.Parse(k);

            return k * 2;

        }


        public int M3(int k)
        {
            //int j = Int32.Parse(k);

            return k * k;

        }


        //Напишите метод действия контроллера, который принимает имя файла и возвращает сам файл пользователю. 
        //Если файл не найден на сервере, метод действия должен возвращать ошибку 404. 
        //Файлы необходимо искать в папке Content.

        //localhost:49332/Redirect/GetFileError?file=51.pdf

        public ActionResult GetFileError(string file)
        {


            // Путь к файлу
           // string FileName = "~/files/"+file;
            string path = Server.MapPath("~/Content/" + file);
            // Тип файла - content-type
            string type = "application/pdf";
            // Имя файла - необязательно    // с которым отдаем
            string name = "PDFIcon.pdf";

            // проверка существования файла
            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Exists)
            {
                return File(path, type, name);
            }
            else
            {
                return HttpNotFound();
            }

            
        }



        //Напишите метод действия контроллера, который возвращает метод (GET, POSTи т.д.) 
        //и список заголовков (Connection, Cookie и т.д.) запроса - Request!

        public string Method()
        {
            string browser = HttpContext.Request.Browser.Browser;
            string userAgent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;

            string method = HttpContext.Request.HttpMethod;

            string cook = HttpContext.Request.Cookies.ToString();

            string headers = "";
            foreach (string s in HttpContext.Request.Headers.Keys)
            {
                headers += s + ": " + Request.Headers[s] + "</br>";
            }


            return 
                "<p>Browser: " + browser + "</p><p>User-Agent: " + userAgent + "</p><p>Url запроса: " + url +
          "</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + 
                "</p><p>Метод запроса: " + method + "</p><p>Куки: " + cook + "</p><p>Заголовки: </br>" + headers + "</p>";


        }


        // Напишите метод действия контроллера и представление, которые считают количество кликов по кнопке

        public ViewResult Cook ()
        {
            int res;
            string num="";  // для записи значения из куки / сессии

            //куки
            //if (HttpContext.Request.Cookies["id"] == null)  // первое обращение, нет такого ключа
            //{
            //    HttpContext.Response.Cookies["id"].Value = "1";
            //    ViewBag.Res = "Ни разу не нажато";
            //}
            //else
            //{
            //    num = HttpContext.Request.Cookies["id"].Value;
            //    res = Int32.Parse(num);
            //    res++;
            //    HttpContext.Response.Cookies["id"].Value = res.ToString();
            //    ViewBag.Res = "Кликнуто раз: " + num;
            //}


            //сессия
            if (HttpContext.Session.IsNewSession)   // первое обращение
            {
                HttpContext.Session["id"] = "1";
                ViewBag.Res = "Ни разу не нажато";
            }
            else
            {
                num = (string)HttpContext.Session["id"];

                int intRes;
                if(Int32.TryParse(num, out intRes)) 
                {
                    res = intRes;

                    res++;
                    HttpContext.Session["id"] = res.ToString();
                    ViewBag.Res = "Кликнуто раз: " + num;
                }

                


            }


            return View();
           

        }


    }
}