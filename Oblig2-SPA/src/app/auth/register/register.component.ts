import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';


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
  errorMessage: any;
  //Constructor
  constructor(private authService: AuthService,private snackBar: MatSnackBar,private router: Router) { }
  

  //Methods
  ngOnInit() {
  }


  register(){
    this.authService.register(this.model).subscribe(
      (response) => {
        console.log("Registration in successfully: " +response);
        this.errorMessage = '';
        this.snackBar.open('User created successfully!', 'Close', {
          duration: 5000,  
        });
        this.router.navigate(['/login']); 
      },
      error => {
        console.log(error);
        this.errorMessage =error;
      }
    );
  }

  cancel(){
    console.log("canceled");
    window.history.back();
  }


}
