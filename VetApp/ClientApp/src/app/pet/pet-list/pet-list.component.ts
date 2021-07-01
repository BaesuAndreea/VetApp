import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Pet, SPECIES } from '../models/pet';

@Component({
  selector: 'app-pet-list',
  templateUrl: './pet-list.component.html',
  styleUrls: ['./pet-list.component.css']
})
export class PetListComponent implements OnInit {

  public pets: Pet[];
  public pet: Pet = new Pet();
  errors;
  public url: string;
  SPECIES = SPECIES;
  owner: string;

  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string) {
    this.url = `${this.apiUrl}pets`;
    console.log(this.url);
    this.loadPets();
  }

  loadPets() {
    this.http.get<Pet[]>(this.url).subscribe(result => {
      console.log(result);
      this.pets = result;
      this.errors = "";
    }, (err) => {
      this.errors = JSON.stringify(err);
    })
  }

  loadFilteredPets() {
    this.http.get<Pet[]>(`${this.url}/filter/${this.owner}`).subscribe(result => {
      this.pets = result;
      this.errors = "";
    }, (err) => {
      this.errors = JSON.stringify(err);
    })
  }

  addPet() {
    this.http.post(this.url, this.pet).subscribe(
      () => { this.loadPets(); this.errors = ""; },
      (err) => {
        this.errors = JSON.stringify(err);
      })
  }

  updatePet(pet: Pet) {
    this.http.put(`${this.url}/${pet.id}`, pet).subscribe(result => {
      this.errors = "";
    }, (err) => {
      this.errors = JSON.stringify(err);
    })
  }

  deletePet(id: number) {
    this.http.delete(`${this.url}/${id}`).subscribe(
      () => { this.loadPets(); this.errors = ""; },
      (err) => {
        this.errors = JSON.stringify(err);
      })
  }

  filterOwner() {
    console.log("owner");
    console.log(this.owner);
    if (this.owner == "")
      this.loadPets();
    else
      this.loadFilteredPets();      
  }

  ngOnInit() {
  }

}
