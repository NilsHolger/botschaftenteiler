using MessageSharer.Data;
using MessageSharer.Models;
using MessageSharer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageSharer.Controllers
{
    public class HomeController : Controller
    {
        private IMailService _mail;
        private IMessageSharerRepository _repo;

        public HomeController(IMailService mail, IMessageSharerRepository repo)
        {
            _mail = mail;
            _repo = repo;
        }

        public ActionResult Index()
        {
            var topics = _repo.GetTopics()
                                .OrderByDescending(t => t.Created)
                                .Take(25)
                                .ToList();
            return View(topics);
        }

        public ActionResult Ueber()
        {
            ViewBag.Message = "Willkommen im Botschaften Teiler";

            return View();
        }

        public ActionResult Kontakt()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Kontakt(ContactModel contactModel)
        {
            var msg = string.Format("Comment From: {1}{0}Email: {2}{0}Website: {3}{0}Message:{4}{0}",
                Environment.NewLine, contactModel.Name, contactModel.Email, contactModel.Website, contactModel.Comment);
            if (_mail.SendMail("nils@nilsnaegele.com", "nils@nilsnaegele.com", "MessageSharer Contact", msg))
            {
                _repo.AddContact(contactModel);
                _repo.Save();
                ViewBag.MailSent = true;
            }
            return View();
        }

        [Authorize]
        public ActionResult MeineNachrichten()
        {
            return View();
        }
    }
}
