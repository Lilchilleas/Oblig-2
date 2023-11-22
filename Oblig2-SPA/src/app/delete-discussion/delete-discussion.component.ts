import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DiscussionService } from '../service/discussion.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-delete-discussion',
  templateUrl: './delete-discussion.component.html',
  styleUrls: ['./delete-discussion.component.css']
})
export class DeleteDiscussionComponent implements OnInit {
 
  //Attributes
  discussion: any = { title: '', content: '' };
  discussionId =+ this.route.snapshot.paramMap.get('id')!;


  //Constructor
  constructor(private discussionService: DiscussionService, private snackBar: MatSnackBar,private router: Router,private route: ActivatedRoute, ) { }


  //Methods
  ngOnInit(): void {
     
    this.discussionService.getDiscussion(this.discussionId).subscribe(
      (data) => {
        this.discussion = data;
        console.log(this.discussion);
        
      },
      (error) => {
        console.error('Failed to fetch discussion:', error);
         
      }
    );
  }




  onSubmit() {
    this.discussionService.deleteDiscussion(this.discussionId).subscribe(
      ()=>{
        this.snackBar.open('Discussion deleted successfully!', 'Close', {
          duration: 5000,
        });
        this.router.navigate(['/discussions'])
      },
        (error) => {
          console.error('Failed to fetch discussion:', error);
          this.snackBar.open('Error occured during delete ' + error.message , 'Close', {
            duration: 5000,
          });
        }
    )
  }
    




}
