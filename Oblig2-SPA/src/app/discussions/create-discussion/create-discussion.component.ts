import { Component, OnInit } from '@angular/core';
import { DiscussionService } from '../../service/discussion.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-discussion',
  templateUrl: './create-discussion.component.html',
  styleUrls: ['./create-discussion.component.css']
})
export class CreateDiscussionComponent {

  //Attributes
  discussion = {
    title: '',
    content: ''
  };

  //Constructor
  constructor(private discussionService: DiscussionService, private snackBar: MatSnackBar,private router: Router ) { }

 

  //Methods
  onSubmit(): void {

    if(this.discussion.title && this.discussion.content){
      this.discussionService.createDiscussion(this.discussion).subscribe(
        () => {
          this.snackBar.open('Discussion created successfully!', 'Close', {
            duration: 5000,  
          });
          this.router.navigate(['/discussions']); 
        },
        error => {
          this.snackBar.open('Error creating discussion: ' + error, 'Close', {
            duration: 5000,
          });
        }
      );
    }

     
  }

}
