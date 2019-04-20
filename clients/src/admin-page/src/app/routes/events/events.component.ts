import { Component, OnInit } from '@angular/core';
import { EventsService } from 'src/app/services/events.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {
  ngOnInit(): void {
    //throw new Error("Method not implemented.");
  }

  rows = [
    { name: 'Austin', gender: 'Male', company: 'Swimlane' },
    { name: 'Dany', gender: 'Male', company: 'KFC' },
    { name: 'Molly', gender: 'Female', company: 'Burger King' },
  ];
  columns = [
    { prop: 'name' },
    { name: 'Gender' },
    { name: 'Company' }
  ];

  // public loading = true;
  // public events: ScrappedEvent[];

  // constructor(private eventService: EventsService) { }

  // ngOnInit() {
  //   this.eventService.getEventsHistory().subscribe(
  //     events => {
  //       this.events = events;
  //     }
  //   )
  // }

}
