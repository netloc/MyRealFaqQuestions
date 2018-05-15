using System.Web.Mvc;
using System.Collections.Generic;
using QuestionsAboutMe.Models;
using System;
using BLL.FaqDataWork;
using BLL.FaqDataWork.Interface;

namespace QuestionsAboutMe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<FAQ> faqResult = GetMyFAQ();

            if (faqResult == null)
            {
                // Handle with Error?
                return View(new List<FAQ>());
            }
            else
            {
                return View(faqResult);
            }
        }

        public ActionResult About()
        {
            List<FAQ> faqResult = GetMyFAQ();

            if (faqResult == null)
            {
                // Handle with Error?
                return View(new List<FAQ>());
            }
            else
            {
                return View(faqResult);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Return All FAQ, we could move this to a helper class
        /// </summary>
        /// <returns></returns>
        private List<FAQ> GetMyFAQ()
        {
            List<FAQ> faqResult = new List<FAQ>();

            IFaqRetriever faqRetriever = new FaqRetriever();
            Dictionary<string, string> faqToConvert = faqRetriever.GetFaqQuestions();

            if (faqToConvert != null)
            {
                foreach (KeyValuePair<string, string> faq in faqToConvert)
                {
                    faqResult.Add(new FAQ { Question = faq.Key, Answer = faq.Value });
                }
            }

            return faqResult;
        }
    }
}