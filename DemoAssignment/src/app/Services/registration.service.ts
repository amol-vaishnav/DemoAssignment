import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
  })
  export class RegistrationService {

    uri = "";
  
    constructor(private http: HttpClient) { }
    insert(form: FormData) {
      return this.http.post(`https://localhost:44315/api/Registration/RegisterUser`, form);
    }
  }