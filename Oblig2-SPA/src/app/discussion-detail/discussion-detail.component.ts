import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DiscussionService } from '../service/discussion.service';
import { AuthService } from '../service/auth.service';


@Component({
  selector: 'app-discussion-detail',
  templateUrl: './discussion-detail.component.html',
  styleUrls: ['./discussion-detail.component.css']
})


export class DiscussionDetailComponent implements OnInit {

  //Attributes
  discussion: any;
  discussionId!: number;
  createdByUser: any;
  isOwner = false;


  //Constructor
  constructor(private discussionService : DiscussionService, private route: ActivatedRoute, private authService: AuthService) { }


  //Methods
  ngOnInit(): void {
    this.discussionId = +this.route.snapshot.paramMap.get('id')!;

    
    this.discussionService.getDiscussion(this.discussionId).subscribe(data => {       
      this.discussion = data;
      this.createdByUser = data.createdBy;
      this.updatedStatus();
      console.log('Fetched Discussion:', this.discussion);
    },
    error => {
      console.error('Error loading discussion', error);
    }
    );
  }


  updatedStatus(): void{
    const currentUser = this.authService.getUser();

    if(currentUser.nameid == this.createdByUser.id){
      this.isOwner = true;
    } 
  } 


     
  

}
