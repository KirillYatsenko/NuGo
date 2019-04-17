import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventsComponent } from './routes/events/events.component';

const routes: Routes = [
  { path: '', component: EventsComponent },
  { path: 'event', component: EventsComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
