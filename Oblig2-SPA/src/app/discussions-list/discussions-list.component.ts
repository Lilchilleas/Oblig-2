import { Component, OnInit } from '@angular/core';
import { DiscussionService } from '../service/discussion.service';

@Component({
  selector: 'app-discussions-list',
  templateUrl: './discussions-list.component.html',
  styleUrls: ['./discussions-list.component.css']
})
export class DiscussionsListComponent implements OnInit {

  //Attributes
  discussions: any;



  //Constructor
  constructor(private discussionService : DiscussionService) { }



  //Methods
  ngOnInit(): void {
    this.discussionService.getDiscussions().subscribe(data => {
      this.discussions = data;
    });
  }




}
