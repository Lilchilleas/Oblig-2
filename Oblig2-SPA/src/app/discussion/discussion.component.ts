import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-discussion',
  templateUrl: './discussion.component.html',
  styleUrls: ['./discussion.component.css']
})






export class DiscussionComponent implements OnInit {

  //Attributes
  discussions: any;

  //Consturctor
  constructor(private http: HttpClient) { }



  //Methods
  ngOnInit() {
    this.getDiscussions();
  }

  getDiscussions(){
    this.http.get('http://localhost:5000/api/Discussion/getDiscussions').subscribe(response => {
      this.discussions = response;
      
    },error => {
      console.log(error);
    });


  }


}
