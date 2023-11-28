import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, map } from 'rxjs';
import { LoginDto } from '../models/login-dto';
import { RegisterDto } from '../models/register-dto';

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

  //Methods

  //The Login angular implementation with the use of token is created based on the implemntation from:
  //"Angular Authentication With JSON Web Tokens (JWT): The Complete Guide" by ANGULAR UNIVERSITY.
  login(model: LoginDto): Observable<void>{
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


  register(model: RegisterDto): Observable<any>{
    return this.http.post(`${this.apiUrl}/register`,model);
  }
 

  //Helper methods
  loggedIn(){
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  getUser(){
    const token = localStorage.getItem('token');
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      this.decodedToken = this.jwtHelper.decodeToken(token);
      return this.decodedToken; 
    }
    return null;
  }

}

// Source Reference:
// ------------------------------------------------------------------------
// - [Title: Angular Authentication With JSON Web Tokens (JWT): The Complete Guide ]
// - Author: [ANGULAR UNIVERSITY]
// - URL: [https://blog.angular-university.io/angular-jwt-authentication/]