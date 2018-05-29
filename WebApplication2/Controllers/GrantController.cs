using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class GrantController : Controller
    {
        // GET: Grant
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        public ActionResult Index()
        {

            if (Session["NewPersonKey"] == null)
            {

                Message msg = new Message();
                msg.MessageText = "You must be logged in to apply for a grant";
                return RedirectToAction("Result", msg);
            }
            ViewBag.GrantList = new SelectList(db.GrantTypes, "GrantTypeKey", "GrantTypeName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include ="GrantApplicationKey, PersonKey, GrantApplicationDate, GrantApplicationReason, GrantApplicationStatusKey, GrantTypeKey" 
            )]GrantApplication r)
        {
            try
            {
                r.PersonKey = (int)Session["NewPersonKey"];
                r.GrantAppicationDate = DateTime.Now;
                db.GrantApplications.Add(r);
                db.SaveChanges();
                Message m = new Message();
                m.MessageText = "Thank you for applying";
                return RedirectToAction("Result", m);
            }
            catch (Exception e)
            {
                Message m = new Message();
                m.MessageText = e.Message;
                return RedirectToAction("Result", m);
            }
        }
        public ActionResult Result(Message msg)
        {
            return View(msg);
        }
    }
}
    