import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../Authentication/service/auth-service';
import { Appointment } from '../models/appointment';

@Component({
  selector: 'app-appointment-list',
  templateUrl: './appointment-list.component.html',
  styleUrls: ['./appointment-list.component.css']
})
export class AppointmentListComponent implements OnInit {

  public appointments: Appointment[];
  public url: string;
  errors: string;
  showAll: boolean = false;
  owner: string;

  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string,
    private authSvc: AuthService, private router: Router  ) {
    this.url = `${this.apiUrl}appointments`;
    this.loadAppointments();
  }

  loadUnfinishedAppointments() {
    this.http.get<Appointment[]>(`${this.url}/unfinished`).subscribe(result => {
      this.appointments = result;
      this.errors = "";
    }, (err) => {
      this.errors = JSON.stringify(err);
    })
  }

  filterOwner() {
    this.http.get<Appointment[]>(`${this.url}/owner/${this.owner}`).subscribe(result => {
      this.appointments = result;
      this.errors = "";
    }, (err) => {
      this.errors = JSON.stringify(err);
    })
  }
  loadAllAppointments() {
    this.http.get<Appointment[]>(this.url).subscribe(result => {
      this.appointments = result;
      this.errors = "";
    }, (err) => {
      this.errors = JSON.stringify(err);
    })
  }

  loadAppointments() {
    if (this.showAll)
      this.loadAllAppointments();
    else
      this.loadUnfinishedAppointments();
  }

  updateAppointment(a: Appointment) {
    this.http.put(`${this.url}/${a.id}`, a).subscribe(result => {
      this.errors = "";
    }, (err) => {
      this.errors = JSON.stringify(err);
    })
  }

  deleteAppointment(id: number) {
    this.http.delete(`${this.url}/${id}`).subscribe(
      () => { this.loadAppointments(); this.errors = ""; },
      (err) => {
        this.errors = JSON.stringify(err);
      })
  }

  showAllAppointments(show: boolean) {
    this.showAll = show;
    this.loadAppointments();
  }

  userLoggedIn() {
    return this.authSvc.userLogedIn();
  }

  addExamination(id: number) {
    this.router.navigate(['addexamination'], { queryParams: { id: id } });
  }

  ngOnInit() {
  }

}
