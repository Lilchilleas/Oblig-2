import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit  {

  //Attributes
  model = {
    username: '',
    password: ''
  }


  //Constructor
  constructor(private authService : AuthService) { }


  //Methods
  ngOnInit(): void {
    
  }


  login() {
      this.authService.login(this.model).subscribe(
        () => {
          console.log("Logged in successfully");
        },
        error => {
          console.log("Failed to login");
        }
      );
  }

 

  

}
