import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of, Subject,throwError } from 'rxjs';
import { Router } from '@angular/router';
import {Salle} from "../app/salle"
import {Heur} from "../app/heurs" 
import {Reservation} from "../app/reservation"

const URL_SERVER = 'http://localhost:53846/weatherforecast';
import { catchError, map, tap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class ApiService {

   optionRequete = {
    headers: new HttpHeaders({ 
      'content-type': 'application/json',
      'Access-Control-Allow-Origin':'*'
    })
  };

  constructor(private http: HttpClient) { }

  getAllsalles(): Observable<Salle[]> {

    return this.http.get<Salle[]>("http://localhost:53846/api/Salles",this.optionRequete).pipe(
     
      catchError(this.handleError<Salle[]>('getAllsalles', []))
    );
  }
  getAllheurs(): Observable<Salle[]> {

    return this.http.get<Heur[]>("http://localhost:53846/api/heurs",this.optionRequete).pipe(
     
      catchError(this.handleError<Heur[]>('getAllheurs', []))
    );
  }

//TODO : Delete this
  getAllreservation(): Observable<Reservation[]> {

    return this.http.get<Reservation[]>("http://localhost:53846/api/Reservation",this.optionRequete).pipe(
     
      catchError(this.handleError<Reservation[]>('getAllReservation', []))
    );
  }
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
  
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
  
      // TODO: better job of transforming error for user consumption
      //this.log(`${operation} failed: ${error.message}`);
  
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }


  public SaveReservation(UserName: string, dateReservation: string, HeurReservation,Salle: string): Observable<any>{

    const data = 'username=' + UserName  + '&DateReservation=' + dateReservation + '&HeurReservation=' + HeurReservation + '&Salle' + Salle;

    const headers = new HttpHeaders().set('content-type', 'application/json');  
   

    var res: Reservation = {
      id: "0",
      username: UserName,
      idsalle: Salle,
      idHeurReservation: HeurReservation,
      dateReservation: dateReservation

  }
   
    return this.http.post<string>("http://localhost:53846/api/Reservation" ,  JSON.stringify(res), {headers});
 }

 
}
