import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { RegisterResponse } from "../models/login-model";
import { AuthService } from "../service/auth-service";

@Component({
  selector: 'app-register.page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent implements OnInit  {

  errors: string;

  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string,
    private router: Router,
    private authSvc: AuthService) { }

  ngOnInit() {
  }

  register(name, email, specialization, passWord, passWord2) {

    if (passWord.value != passWord2.value) {
      this.errors = "The password and confirmation password do not match.";
    }
    else {
      this.http.post(this.apiUrl + 'authentication/register',
        { name:name.value, email: email.value, specialization: specialization.value, password: passWord.value, confirmPassword: passWord2.value })
      .subscribe((response: RegisterResponse) => {
        this.http.post(this.apiUrl + 'authentication/confirm', { email: email.value, confirmationToken: response.confirmationToken })
          .subscribe(() => {
            this.router.navigateByUrl('');
          }, error => this.errors = error);
      }, error => this.errors = error);
    }
  }
}
