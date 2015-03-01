using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using registration001.Models;
using System.Web.Helpers;

// контролер с таким именем будет default те мы на него без всяких указаний попадем!

namespace registration001.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        // теперь когда мы попадает по адресу в этот контролер, он вернет View()
        [HttpGet]
        public ViewResult Index()
        {
            // пример передать из Controller во View
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";

            return View();
        }

        [HttpPost]
        public String Index(User userResponse)
        {
            // здесь зачитываем данные о регистрации из файла. 
            string[] allLines = System.IO.File.ReadAllLines(@"c:/temp/allUsers.txt");
            // Теперь превращаем строки в объекты
            User[] listUsers = new User[allLines.Length];
            for (int i = 0; i < allLines.Length; i++ )
            {
                listUsers[i] = System.Web.Helpers.Json.Decode<User>(allLines[i]);
            }
            // Затем нужна проверка есть ли такая пара логин и пароль
            Console.WriteLine("hello");
            foreach (User suspect in listUsers)
            {
                if (suspect.Email == userResponse.Email && suspect.Password == userResponse.Password)
                {
                    return "success entrance";
                }
                
            }
            // Нужна новая View, причем не привязанная к модели
            
            return "pair Log&pass no found";
        }



        // если пользователь хочет заполнить форму, то это get
        [HttpGet]
        public ViewResult RegistrationForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RegistrationForm(User userResponse)
        {
            // на этом моменте я получил данные с формы регистрации, здесь они должны куда-то сохраниться
            // txt + Json пойдет

            // Пишем в файлы данные с формы в формате Json, добавляем в конец
            List<string> lines = new List<string>();
            lines.Add(System.Web.Helpers.Json.Encode(userResponse));
            string[] slot = lines.ToArray();
            System.IO.File.AppendAllLines(@"c:/temp/allUsers.txt", slot);

            // TODO: Email response to the party organizer
            return View("Thanks", userResponse);
        }

    }
}
