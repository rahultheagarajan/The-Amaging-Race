﻿using NUS.TheAmagingRace.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace NUS.TheAmagingRace.BAL
{
    public class EventBAL
    {
        private TARDBContext db = new TARDBContext();
        private Event events = new Event();

        public List<Event> GetEventList()
        {
            return db.Events.OrderBy(o => o.StartDate).ToList(); ;
        }

        public List<Event> EditEventList(Event eventModel,string currentUser)
        {
            if (eventModel.EventID > 0)
            {
                Event editEvents = db.Events.Include("Teams").Include("PitStops").SingleOrDefault(x => x.EventID == eventModel.EventID);
                
                editEvents.EventName = eventModel.EventName;
                editEvents.EventDescription = eventModel.EventDescription;
                editEvents.EventCountry = eventModel.EventCountry;
                editEvents.EventCity = eventModel.EventCity;
                editEvents.StartDate = eventModel.StartDate;
                editEvents.EndDate = eventModel.EndDate;
                editEvents.TotalPitStops = eventModel.TotalPitStops;
                editEvents.TotalTeams = eventModel.TotalTeams;
                editEvents.LastModifiedBy = currentUser;
                editEvents.LastModifiedAt = DateTime.Now;
                editEvents.ImagePath = eventModel.ImagePath;
                db.SaveChanges();
            }
            else
            {
                eventModel.CreatedBy = currentUser;
                db.Events.Add(eventModel);
                
                db.SaveChanges();
            }

            return db.Events.OrderBy(o => o.StartDate).ToList(); ;
        }

        public Event GetEditingValues(int eventId)
        {
            Event editEvents = db.Events.SingleOrDefault(x => x.EventID == eventId);
           
            return editEvents;

        }

        public Event GetSelectedEvent(int eventId)
        {
            Event currentEvent = db.Events.SingleOrDefault(x => x.EventID == eventId);
            return currentEvent;

        }


        public List<Event> DeleteEventfromList(int eventId)
        {
           
            var events = db.Events.Find(eventId);
            db.Events.Remove(events);
            db.SaveChanges();
           
            return GetEventList();
        }

        public List<Event> SearchEvent(string searchString)
        {
            var events = from s in db.Events
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(s => s.EventName.Contains(searchString));
            }
            return events.OrderBy(o => o.StartDate).ToList();
        }

        public List<Event> CurrentEvent()
        {
            var events = from s in db.Events
                         select s;
            
            
           events = events.Where(s => s.StartDate<=DateTime.Now && s.EndDate>=DateTime.Now);
            
            return events.OrderBy(o => o.StartDate).ToList();
        }

        public List<Event> UpcomingEvents()
        {
            var events = from s in db.Events
                         select s;


            events = events.Where(s => s.StartDate > DateTime.Now);

            return events.OrderBy(o=>o.StartDate).ToList();
        }


        

    }
}
