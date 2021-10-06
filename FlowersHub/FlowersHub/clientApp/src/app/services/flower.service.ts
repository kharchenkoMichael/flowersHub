import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Flower } from '../Model/flower';


@Injectable({
  providedIn: 'root'
})
export class FlowerService {

  constructor(private http: HttpClient) { }

  get(): Observable<Flower[]> {
    return this.http.get<Flower[]>(`api/Flower`);
  }

  getWithArguments(skip: number, take: number): Observable<Flower[]> {
    return this.http.get<Flower[]>(`api/Flower?skip=${skip}&take=${take}`);
  }
}
