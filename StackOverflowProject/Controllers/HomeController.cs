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
        ICategoriesService cs;

        public HomeController(IQuestionsService qs, ICategoriesService cs)
        {
            this.qs = qs;
            this.cs = cs;
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

        public ActionResult Categories()
        {
            return View(this.cs.GetCategories());
        }

        [Route("allquestions")]
        public ActionResult Questions()
        {
            return View(this.qs.GetQuestions());
        }
    }
}