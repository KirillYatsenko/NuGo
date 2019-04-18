import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) { }

  public get<T>(url: string): Observable<T> {
    return this.http.get<T>("http://localhost:5000/api/" + url);
  }

  // private getRequestOptionArgs(options?: RequestOptionsArgs): RequestOptionsArgs {
  //   if (options == null) {
  //     options = new RequestOptions();
  //   }
  //   if (options.headers == null) {
  //     options.headers = new Headers();
  //     options.headers.append('Authorization', localStorage.getItem('token'));
  //   }

  //   options.headers.append('ApiKey', environment.apiKey);
  //   if (localStorage.getItem('impersonate_id')) {
  //     options.headers.append('impId', localStorage.getItem('impersonate_id'));
  //   }

  //   return options;
  // }

}
