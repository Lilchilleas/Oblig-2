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
 

  discussion: any = { title: '', content: '' };
  discussionId =+ this.route.snapshot.paramMap.get('id')!;

  constructor(private discussionService: DiscussionService, private snackBar: MatSnackBar,private router: Router,private route: ActivatedRoute, ) { }

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
    })
  }
    




}
