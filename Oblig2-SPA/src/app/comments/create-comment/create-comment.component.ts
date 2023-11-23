import { Component, Input, OnInit } from '@angular/core';
import { DiscussionService } from '../../service/discussion.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AuthService } from '../../service/auth.service'; 

@Component({
  selector: 'app-create-comment',
  templateUrl: './create-comment.component.html',
  styleUrls: ['./create-comment.component.css']
})


export class CreateCommentComponent {


  //Attributes
  @Input() discussionId!: number;
  @Input() parentCommentId!: number;
  content = '';


  //Constructor
  constructor(private discussionService: DiscussionService,private authService: AuthService, private snackBar: MatSnackBar, private router: Router ) { }


  //Methods
  onSubmit(): void {

    if (!this.authService.loggedIn()) {
      this.router.navigate(['/login']);  
      return; 
    }


    const comment = {
      content: this.content,
      parentCommentId: this.parentCommentId,
      subComments: []
    };

    
    this.discussionService.createComment(this.discussionId, comment).subscribe(
      () => {
        if (this.parentCommentId === null) {
          this.snackBar.open('Comment posted', 'Close', { duration: 5000 });
        } else {
          this.snackBar.open('Sub-comment posted', 'Close', { duration: 5000 });
        }
        location.reload();
      },
      (error) => {
        this.snackBar.open('Error creating comment: ' + error.message, 'Close', { duration: 5000 });
      }
    );



    
  }
}