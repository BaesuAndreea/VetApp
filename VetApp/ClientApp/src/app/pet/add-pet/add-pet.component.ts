import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Pet, SPECIES } from '../models/pet';

@Component({
  selector: 'app-add-pet',
  templateUrl: './add-pet.component.html',
  styleUrls: ['./add-pet.component.css']
})
export class AddPetComponent implements OnInit {

  public pet: Pet = new Pet();
  errors;
  SPECIES = SPECIES;
  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string,
    private router: Router  ) { }

  addPet() {
    this.http.post(`${this.apiUrl}pets`, this.pet).subscribe(
      () => {
        this.errors = "";
        this.router.navigateByUrl('pets');
      },
      (err) => {
        this.errors = JSON.stringify(err);
      })
  }

  ngOnInit() {
  }

}
