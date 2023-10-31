import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})


export class AuthService {

  //Attributes
  private apiUrl = 'http://localhost:5000/api/auth';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  //Constructor
  constructor(private http: HttpClient) { }

  //Method
  login(model: any){
    return this.http.post(`${this.apiUrl}/login`,model).pipe(
      map((response: any) => {
        const user = response;
        if(user){
          localStorage.setItem('token',user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          console.log(this.decodedToken);
        }
      })
    );
  }


  register(model: any){
    return this.http.post(`${this.apiUrl}/register`,model);
  }
 
  loggedIn(){
    const token = localStorage.getItem('token');
    
    return !this.jwtHelper.isTokenExpired(token);
  }

}
