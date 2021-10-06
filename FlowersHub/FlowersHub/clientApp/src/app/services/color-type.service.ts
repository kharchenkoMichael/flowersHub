import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ColorTypeService {

  constructor(private http: HttpClient) { }

  get(): Observable<string[]> {
    return this.http.get<string[]>(`/api/ColorType`);
  }
}
