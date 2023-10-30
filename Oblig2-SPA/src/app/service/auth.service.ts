import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})


export class AuthService {

  //Attributes
  private apiUrl = 'http://localhost:5000/api/auth';

  //Constructor
  constructor(private http: HttpClient) { }

  //Method
  login(model: any){
    return this.http.post(`${this.apiUrl}/login`,model).pipe(
      map((response: any) => {
        const user = response;
        if(user){
          localStorage.setItem('token',user);
        }
      })
    );
  }


  register(model: any){
    return this.http.post(`${this.apiUrl}/register`,model);
  }
 

}
