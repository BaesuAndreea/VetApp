import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../Authentication/service/auth-service';
import { AppointmentWithExaminations } from '../../examination/models/examination-model';

@Component({
  selector: 'app-add-examination',
  templateUrl: './add-examination.component.html',
  styleUrls: ['./add-examination.component.css']
})
export class AddExaminationComponent implements OnInit {

  appointmentId: number;
  examinations: AppointmentWithExaminations;
  notes: string;

  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string,
    private route: ActivatedRoute, private authSvc: AuthService) {
    this.route.queryParams.subscribe(params => {
      this.appointmentId = params['id'];

      http.get<AppointmentWithExaminations>(apiUrl + 'examinations/' + this.appointmentId ).subscribe(result => {
        this.examinations = result;
      }, error => console.error(error));
    });}

  addExamination() {
    const examinationDetails = {
      appointmentId: this.appointmentId,
      notes: this.notes
    };
    this.http.post(this.apiUrl + `examinations`, examinationDetails)
      .subscribe((response) => {
        window.location.reload();
      }, error => console.error(error));
  }

  ngOnInit() {
  }

}
