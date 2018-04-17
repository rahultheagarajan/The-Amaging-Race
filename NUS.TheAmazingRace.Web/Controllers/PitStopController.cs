﻿using Microsoft.AspNet.Identity;
using NUS.TheAmagingRace.BAL;
using NUS.TheAmagingRace.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NUS.TheAmazingRace.Web.Controllers
{
    public class PitStopController : Controller
    {
        private PitStopBAL pitStopBAL = new PitStopBAL();
        private EventBAL eventBAL = new EventBAL();
        //private EventManagement eventManagement = new EventManagement();
        // GET: PitStop
       
        public JsonResult Index(int eventId=0)
        {
            List<PitStop> pitstop = pitStopBAL.getPitStopOfEvent(eventId);
            return Json(pitstop,JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult CreatePitStop()
        {
           return PartialView("_CreatePitStop");
        }

        //[HttpPost]
        //public ActionResult AddPitStop(PitStop pitStop)
        //{
        //    String currentUser = User.Identity.GetUserName();
        //    eventManagement.PitStops = pitStopBAL.CreatePitStopList(pitStop, currentUser);
        //    eventManagement.Events = eventBAL.GetEventList();
        //    return View("~/Views/Event/Index.cshtml", eventManagement);
        //}
    }
}