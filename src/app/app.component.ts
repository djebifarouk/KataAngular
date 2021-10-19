import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription, Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

import {ApiService} from '../app/api.service';
import {Salle} from "../app/salle";
import {Heur} from "../app/heurs";
import { Reservation } from './reservation';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'kata';
  ListSalle:Salle[] = [];
  ListHeur:Heur[] = [];
  ListResevation:Reservation[] = [];
  selectedHeur: string;
  submitted = false;
  registerForm: FormGroup;
  subscription: Subscription;
  postt: any;
  isLoginError: boolean;
  dateselected: string;
  salleselected: string ;
  MessageErreur: string[] = [];

  checkoutForm = this.formBuilder.group({
    name: '',
    heur: '',
    date: ''
  });

  constructor(private _apiservice: ApiService,  private formBuilder: FormBuilder,private router: Router) { }

    ngOnInit(): void {
      this.registerForm = this.formBuilder.group({
        username: ['', Validators.required],
        salle: ['', Validators.required],
        dateSale: ['', Validators.required]
    });
     this.get();
    }
    onClickHandler(idheur: string)
    {
     if (this.selectedHeur == null)  
     this.selectedHeur =  idheur;
     else
      this.selectedHeur = this.selectedHeur + "," + idheur;
    }
    public get()
    {
      

      this._apiservice.getAllsalles().subscribe(result => this.ListSalle = result);
      this._apiservice.getAllheurs().subscribe(result => this.ListHeur = result);
      this._apiservice.getAllreservation().subscribe((data: Reservation[])=>{
        
         this.ListResevation = data;
        
      });
    
    }
    onChangeDate($event, dateValue) {
      //alert(deviceValue);
      this.dateselected = dateValue;
      this.checkResrvation(dateValue,this.salleselected);
  }
  onChangeSalle($event, salleValue)
  {
    this.salleselected = salleValue;
    this.checkResrvation(this.dateselected,salleValue);
  }
  checkResrvation(datereservation: string,salle: string)
  {
    for(var i =0;i<24;i++)
    {
      (<HTMLInputElement>document.getElementById("demo4" + i)).checked = false;
      (<HTMLInputElement>document.getElementById("demo4" + i)).removeAttribute('disabled');
    }
    this._apiservice.getAllreservation().subscribe((data: Reservation[])=>{
     
      data.forEach(element => {
          if(element.dateReservation == datereservation && element.idsalle == salle ){
               (<HTMLInputElement>document.getElementById("demo4" + element.idHeurReservation)).checked = true;
               (<HTMLInputElement>document.getElementById("demo4" + element.idHeurReservation)).setAttribute('disabled', 'true');
          }
    });
   this.ListResevation = data;
  
});
  }
    async onSubmit() {
      this.submitted = true;
  
      // stop here if form is invalid
      if (this.registerForm.invalid) {
        this.MessageErreur = [];
        this.MessageErreur.push("Les élements suivants sont obligatoir : { - Nom  | Nom de sale | Date de la réunion}");
          return;
      }
      if(this.selectedHeur == null || this.selectedHeur == "")  {
        this.MessageErreur = [];this.MessageErreur.push("veuillez choisir l'heure de la réunion");
        return;
      }
      console.log('start');
      const salle = this.registerForm.value.salle;
      const username = this.registerForm.value.username;
      const heurReserv = this.selectedHeur;
      const DateReservation = this.registerForm.value.dateSale;

      if(heurReserv!= null && heurReserv !="" && username != null && username != ""){
      this._apiservice.SaveReservation(username,DateReservation,heurReserv,salle,).subscribe((data: Reservation) => {
        localStorage.setItem('idHeurReservation', data.idHeurReservation);
        console.log('dateReservation : ' + data.dateReservation);
       
      });
    }
    window.location.reload();
  
    }


}
