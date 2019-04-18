import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  constructor(private http: HttpService) { }

  public getEvents(): Observable<ScrappedEvent[]>{
    return this.http.get<ScrappedEvent[]>("events")
  }

  public getEventsHistory(): Observable<ScrappedEvent[]>{
    return this.http.get<ScrappedEvent[]>("events/history")
  }
}
