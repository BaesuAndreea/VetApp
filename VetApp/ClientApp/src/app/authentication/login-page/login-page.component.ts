import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthResponse } from '../models/auth-model';
import { Login } from '../models/login-model';
import { AuthService } from '../service/auth-service';

@Component({
  selector: 'app-login',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  loginData = new Login();

  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string,
    private router: Router,
    private authSvc: AuthService) { }

  ngOnInit() {
    if (this.authSvc.userLogedIn()) {
      this.router.navigateByUrl('');
    }
  }

  logIn(userName, password) {
    this.loginData = {
      email: userName.value,
      password: password.value
    }
    this.http.post(this.apiUrl + 'authentication/login', this.loginData)
      .subscribe((response: AuthResponse) => {
        this.authSvc.saveToken(response.token);
        window.location.reload();
        
      }, error => console.error(error));
  }

}
