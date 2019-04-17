import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventsComponent } from './events/events.component';

@NgModule({
  declarations: [
    EventsComponent
  ],
  imports: [
    CommonModule
  ],
  exports:[
    EventsComponent
  ]
})
export class RoutesModule { }
