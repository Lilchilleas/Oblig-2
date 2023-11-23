import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})


export class NavbarComponent implements OnInit {


  //Attributes


  //Constructor 
  constructor(public authService: AuthService) { }


  //Methods
  ngOnInit() {
  }


  loggedIn(){
    return this.authService.loggedIn();
    
  }

  logout(){
    localStorage.removeItem('token');
    console.log("logged out");
  }
}
