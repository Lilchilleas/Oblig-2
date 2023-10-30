import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})


export class RegisterComponent implements OnInit {
  
  //Attributes
  model = {
    username: '',
    password: ''
  }
  
  //Constructor
  constructor(private authService: AuthService) { }


  //Methods
  ngOnInit() {
  }


  register(){
    this.authService.register(this.model).subscribe(
      () => {
        console.log("Registration in successfully");
      },
      error => {
        console.log(error);
      }
    );
  }

  cancel(){
    console.log("canceled");
  }

}
