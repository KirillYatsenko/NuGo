import { Component, OnInit } from '@angular/core';
import { EventsService } from 'src/app/services/events.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {

  public loading = true;
  public events: ScrappedEvent[];

  constructor(private eventService: EventsService) { }

  ngOnInit() {
    this.eventService.getEventsHistory().subscribe(
      events => {
        this.events = events;
        this.loading = false;
      },
      error => {
        this.loading = false;
      }
    )
  }

}
