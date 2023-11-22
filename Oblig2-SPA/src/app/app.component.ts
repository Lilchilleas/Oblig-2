import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from './service/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  //Attributes
  title = 'Oblig-2';
  jwtHelper = new JwtHelperService();


  //Constuctor
  constructor(private authService: AuthService) {}


  //Methods
  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if(token){
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }

}



