using Application.model2;
using Application.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
      
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(s_user obj)

        {
            userloginEntities obj2 = new userloginEntities();

            var sign = obj2.s_user.Where(m => m.username == obj.username && m.u_password == m.u_password).Count();
            if (sign > 0)
            {
                return RedirectToAction("dashboard");
            }
            else
            {
                ViewBag.Message = "Your username and password incorrect";
                return View("sorry");

            }



         
        }
        public ActionResult sorry()
        {
            return View();
        }
        public ActionResult dashboard()
        {
            return View();
        }

      

        public ActionResult table()
        {
            applicationEntities dbobj = new applicationEntities();
            var tab = dbobj.applicationtables.ToList();
            return View(tab);
        }
        [HttpGet]
        public ActionResult forms()
        {
            

            return View();
        }
        [HttpPost]
        public ActionResult forms(applicationtable mdobj)
        {
            applicationEntities dbobj = new applicationEntities();
            applicationtable tbobj = new applicationtable();
            tbobj.name = mdobj.name;
            tbobj.email = mdobj.email;
            tbobj.mobile = mdobj.mobile;
            tbobj.address = mdobj.address;
            if (mdobj.id==0) {
                dbobj.applicationtables.Add(tbobj);
                dbobj.SaveChanges();

            }
            else
            {
                dbobj.Entry(tbobj).State = System.Data.Entity.EntityState.Modified;
                dbobj.SaveChanges();

            }
           
            return RedirectToAction("table");




        }


        public ActionResult delete(int id)
        {
            applicationEntities dbobj = new applicationEntities();
            var del = dbobj.applicationtables.Where(m => m.id == id).First();
            dbobj.applicationtables.Remove(del);
            dbobj.SaveChanges();
            return RedirectToAction("table");
        }

        public ActionResult edit(int id)
        {
            applicationEntities dbobj = new applicationEntities();
            var ed = dbobj.applicationtables.Where(m => m.id == id).First();
            applicationtable tbobj = new applicationtable();
            tbobj.id = ed.id;
            tbobj.name = ed.name;
            tbobj.email = ed.email;
            tbobj.mobile = ed.mobile;
            tbobj.address = ed.address;

            return View("forms",tbobj);
        }

    }
}