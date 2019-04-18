import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventsComponent } from './events/events.component';
import { ServicesModule } from '../services/services.module';

@NgModule({
  declarations: [
    EventsComponent
  ],
  imports: [
    CommonModule,
    ServicesModule
  ],
  exports:[
    EventsComponent
  ]
})
export class RoutesModule { }
