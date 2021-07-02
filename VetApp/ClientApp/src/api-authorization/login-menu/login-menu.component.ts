import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from '../authorize.service';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { AuthService } from '../../app/Authentication/service/auth-service';

@Component({
  selector: 'app-login-menu',
  templateUrl: './login-menu.component.html',
  styleUrls: ['./login-menu.component.css']
})
export class LoginMenuComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;

  userLoggedIn: boolean;

  constructor(private authorizeService: AuthorizeService,
    private authSvc: AuthService) {
    this.setUserLoggedIn();
  }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
    this.setUserLoggedIn();
  }

  setUserLoggedIn() {
    this.userLoggedIn = this.authSvc.userLogedIn();
  }

  logOut() {
    this.authSvc.removeToken();
    this.setUserLoggedIn();
  }
}
