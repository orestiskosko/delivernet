using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeliverNET.Models;
using System.Web.Mvc;

namespace DeliverNET.Controllers
{
    public class AuthController : Controller
    {
        List<VMBoi> Boiz = new List<VMBoi>()
        {
            new VMBoi{FullName="Stathis Pantazis", Age=25, mail="tokaluteropaidi@hotmail.com", Password="pok", Phone="6912345670", Username="Pantazakos"},
            new VMBoi{FullName="Orestis Koskoletos", Age=26, mail="cocksucker@hotmail.com", Password="1234", Phone="6912345671", Username="Orestiada"},
            new VMBoi{FullName="Marios Radis", Age=28, mail="iliasskourlas@hotmail.com", Password="12345", Phone="6912345672", Username="Radiosa"}
        };

        List<VMSlaver> Slavers = new List<VMSlaver>()
        {
            new VMSlaver{OwnerFullName="Vuronas Vuronas", Mail="kagkouras@hotmail.com", Password="0987", Phone="6912345673", Username="Byron", ShopName="Bootcamp"},
            new VMSlaver{OwnerFullName="Viola Giola", Mail="Dwse5MpuresPrwtaKaiVlepoume@hotmail.com", Password="7890", Phone="6912345674", Username="Gunaika", ShopName="Java"},
            new VMSlaver{OwnerFullName="Stauros Progouli", Mail="cringefest@hotmail.com", Password="12340987", Phone="6912345675", Username="CringeMaster", ShopName="9gag"}
        };


        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }
    }
}