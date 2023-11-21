import { Component, OnInit } from '@angular/core';
import { DiscussionService } from '../service/discussion.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-update-discussion',
  templateUrl: './update-discussion.component.html',
  styleUrls: ['./update-discussion.component.css']
})
export class UpdateDiscussionComponent implements OnInit{

  discussion: any = { title: '', content: '' };

  constructor(private discussionService: DiscussionService,private route: ActivatedRoute) { }

  ngOnInit(): void {
    const discussionId = +this.route.snapshot.paramMap.get('id')!;
    this.discussionService.getDiscussion(discussionId).subscribe(
      (data) => {
        this.discussion = data;
        console.log(this.discussion);
      },
      (error) => {
        console.error('Failed to fetch discussion:', error);
      }
    );
  }

  onSubmit(){
    this.updateDiscussion(this.discussion.id, this.discussion);
  }

  updateDiscussion(id: number, updatedDiscussion: any){
    this.discussionService.updateComment(id,updatedDiscussion).subscribe(
      (updatedDiscussion) => {
        console.log('Discussion updated successfully:', updatedDiscussion);
      },
      (error) => {
        console.log(error);
      }
    );

  }
  
  

}
