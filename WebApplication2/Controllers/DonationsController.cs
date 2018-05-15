using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class DonationsController : Controller
    {
        // GET: Donations
        public ActionResult Index()
        {
            if (Session["NewPersonKey"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PersonKey, DonationDate,DonationAmount,DonationConfirmationCode")]Donation b)
        {
            b.DonationDate = DateTime.Now;
            b.PersonKey = (int)Session["NewPersonKey"];
            b.DonationConfirmationCode = new Guid();
            CommunityAssist2017Entities db = new CommunityAssist2017Entities();
            db.Donations.Add(b);
            db.SaveChanges();
            Message m = new Message();
            m.MessageText = "Donation has been added";
            return View("Result", m);
        }

        public ActionResult Result(Message msg)
        {
            return View(msg);
        }
    }
}
