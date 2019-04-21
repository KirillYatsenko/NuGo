import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventsComponent } from './events/events.component';
import { ServicesModule } from '../services/services.module';
import { SharedModule } from '../shared/shared.module';
import { DataTablesModule } from 'angular-datatables';
import { MDBBootstrapModule } from 'angular-bootstrap-md';

@NgModule({
  declarations: [
    EventsComponent
  ],
  imports: [
    CommonModule,
    ServicesModule,
    SharedModule,
    DataTablesModule,
    MDBBootstrapModule.forRoot()
  ],
  exports:[
    EventsComponent
  ]
})
export class RoutesModule { }
