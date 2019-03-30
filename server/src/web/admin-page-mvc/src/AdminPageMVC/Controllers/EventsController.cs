using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdminPageMVC.Models;
using AdminPageMVC.Services;

namespace AdminPageMVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventsService _eventsService;

        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventsService.GetAsync();

            return View(events);
        }   

        public event

        // public async Task<IActionResult> Details(long eventId)
        // {
        //     var event = await _eventsService.GetAsync(eventId);

        //     return View(event);
        // }

    }
}
