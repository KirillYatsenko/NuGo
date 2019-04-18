import { Component, OnInit } from '@angular/core';
import { EventsService } from 'src/app/services/events.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {

  constructor(private eventService: EventsService) { }

  ngOnInit() {
    this.eventService.getEventsHistory().subscribe(
      events=>{
        console.log(events);
      }
    )
  }

}
