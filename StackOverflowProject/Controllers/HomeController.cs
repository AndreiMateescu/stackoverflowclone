using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowProject.ServiceLayer;

namespace StackOverflowProject.Controllers
{
    public class HomeController : Controller
    {
        IQuestionsService qs;

        public HomeController(IQuestionsService qs)
        {
            this.qs = qs;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View(this.qs.GetQuestions().Take(10).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}