import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

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

  errorMessage: any;

  //Constructor
  constructor(private authService: AuthService,private snackBar: MatSnackBar,private router: Router) { }


  //Methods
  ngOnInit(): void {
    
  }


  login() {
      this.authService.login(this.model).subscribe(
        () => {
          console.log("Logged in successfully");
          this.errorMessage = '';
          this.snackBar.open('Login successfull', 'Close', {
            duration: 5000,  // Duration 5 seconds
          });
          this.router.navigate(['/discussions']); 
        },
        error => {
          console.log("Failed to login");
          console.log(error);
          this.errorMessage = error + '| Wrong username or password';
        }
      );
  }

  

  

}
