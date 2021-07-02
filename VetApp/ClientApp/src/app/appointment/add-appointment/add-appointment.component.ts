import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Pet } from '../../pet/models/pet';
import { Appointment } from '../models/appointment';

@Component({
  selector: 'app-add-appointment',
  templateUrl: './add-appointment.component.html',
  styleUrls: ['./add-appointment.component.css']
})
export class AddAppointmentComponent implements OnInit {

  appointment: Appointment = new Appointment();
  public pets: Pet[];
  public pet: Pet = new Pet();
  errors;
  public url: string;

  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string,
    private router: Router  ) {
    this.url = `${this.apiUrl}appointments`;
    this.loadPets();
    this.appointment.finished = false;
  }

  loadPets() {
    this.http.get<Pet[]>(`${this.apiUrl}pets`).subscribe(result => {
      console.log(result);
      this.pets = result;
      this.errors = "";
    }, (err) => {
      this.errors = JSON.stringify(err);
    })
  }

  addAppointment() {
    this.http.post(this.url, this.appointment).subscribe(
      () => { this.router.navigateByUrl('appointments'); },
      (err) => {
        this.errors = JSON.stringify(err);
      })
  }

  ngOnInit() {
  }

}
