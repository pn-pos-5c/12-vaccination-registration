import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import Registration from "../models/Registration";
import Vaccination from "../models/Vaccination";

@Injectable({
  providedIn: 'root'
})
export class DataProviderService {
  rootUrl = 'http://localhost:5000/api'

  constructor(private http: HttpClient) {
  }

  getTimeslots(date: string): Observable<string[]> {
    return this.http.get<string[]>(`${this.rootUrl}/Registrations/timeslots?date=${date}`);
  }

  validateRegistration(ssn: number, pin: number): Observable<Registration | null> {
    return this.http.get<Registration | null>(`${this.rootUrl}/Registrations/validate?ssn=${ssn}&pin=${pin}`);
  }

  addVaccination(vaccination: Vaccination): Observable<any> {
    return this.http.post<any>(`${this.rootUrl}/Vaccinations`, vaccination);
  }
}
