import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { PetListComponent } from './pet/pet-list/pet-list.component';
import { RegisterPageComponent } from './Authentication/register-page/register-page.component';
import { LoginPageComponent } from './Authentication/login-page/login-page.component';
import { AppointmentListComponent } from './appointment/appointment-list/appointment-list.component';
import { AddAppointmentComponent } from './appointment/add-appointment/add-appointment.component';
import { AddPetComponent } from './pet/add-pet/add-pet.component';
import { AddExaminationComponent } from './appointment/add-examination/add-examination.component';
import { TokenInterceptor } from '../interceptors/auth-token.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    PetListComponent,
    RegisterPageComponent,
    LoginPageComponent,
    AppointmentListComponent,
    AddAppointmentComponent,
    AddPetComponent,
    AddExaminationComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'register', component: RegisterPageComponent },
      { path: 'login', component: LoginPageComponent },
      { path: 'pets', component: PetListComponent },
      { path: 'appointments', component: AppointmentListComponent },
      { path: 'addappointment', component: AddAppointmentComponent },
      { path: 'addpet', component: AddPetComponent },
      { path: 'addexamination', component: AddExaminationComponent },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
