import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DiscussionService } from '../service/discussion.service';


@Component({
  selector: 'app-discussion-detail',
  templateUrl: './discussion-detail.component.html',
  styleUrls: ['./discussion-detail.component.css']
})


export class DiscussionDetailComponent implements OnInit {

  //Attributes
  discussion: any;
  discussionId!: number;



  //Constructor
  constructor(private discussionService : DiscussionService, private route: ActivatedRoute) { }


  //Methods
  ngOnInit(): void {
    this.discussionId = +this.route.snapshot.paramMap.get('id')!;

    
    this.discussionService.getDiscussion(this.discussionId).subscribe(data => {
      
      
        
      this.discussion = data;
      console.log('Fetched Discussion:', this.discussion);
    },
    error => {
      console.error('Error loading discussion', error);
    }
    );
  }


     
  

}
