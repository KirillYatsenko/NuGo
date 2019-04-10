using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdminPageMVC.ViewModels;
using AdminPageMVC.Services;

namespace AdminPageMVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventsService _eventsService;
        private readonly IEventsCategoryService _categoryService;

        public EventsController(IEventsService eventsService, IEventsCategoryService categoryService)
        {
            _eventsService = eventsService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventsService.GetRelevantAsync();
            return View(events);
        }

        public async Task<IActionResult> History()
        {
            var events = await _eventsService.GetHistoryAsync();
            return View("Index", events);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var @event = await _eventsService.GetAsync(id);
            var allCategories = await _categoryService.GetAsync();

            @event.AllCategories = allCategories;

            return View(@event);
        }

        // public async Task<IActionResult> Details(long eventId)
        // {
        //     var event = await _eventsService.GetAsync(eventId);

        //     return View(event);
        // }

    }
}
