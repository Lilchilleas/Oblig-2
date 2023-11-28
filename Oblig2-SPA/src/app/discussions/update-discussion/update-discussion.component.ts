import { Component, OnInit } from '@angular/core';
import { DiscussionService } from '../../service/discussion.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-update-discussion',
  templateUrl: './update-discussion.component.html',
  styleUrls: ['./update-discussion.component.css']
})
export class UpdateDiscussionComponent implements OnInit{


  //Attributes
  discussion: any = { title: '', content: '' };


  //Constructor
  constructor(private discussionService: DiscussionService,private route: ActivatedRoute,  private snackBar: MatSnackBar, private router: Router ) { }



  //Methods
  ngOnInit(): void {
    const discussionId = +this.route.snapshot.paramMap.get('id')!;
    this.discussionService.getDiscussion(discussionId).subscribe(
      (data) => {
        this.discussion = data;
      },
      (error) => {
        console.error('Failed to fetch discussion:', error);
      }
    );
  }

  onSubmit(){
    if(this.discussion.title && this.discussion.content){
      this.updateDiscussion(this.discussion.id, this.discussion);
    }
     
  }

  updateDiscussion(id: number, updatedDiscussion: any){
    this.discussionService.updateComment(id,updatedDiscussion).subscribe(
      (updatedDiscussion) => {
        console.log('Discussion updated successfully:', updatedDiscussion);
        this.snackBar.open('Discussion updated successfully!', 'Close', {
          duration: 5000,  
        });
        this.router.navigate(['/discussions']); 
      },
      (error) => {
        console.log(error);
        this.snackBar.open('Error updating discussion: ' + error, 'Close', {
          duration: 5000,
        });
      }
    );

  }
  
  

}
