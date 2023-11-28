import { Component, OnInit } from '@angular/core';
import { DiscussionService } from '../../service/discussion.service';
import { Discussion } from './discussions';

@Component({
  selector: 'app-discussions-list',
  templateUrl: './discussions-list.component.html',
  styleUrls: ['./discussions-list.component.css']
})
export class DiscussionsListComponent implements OnInit {

  // Attributes
  discussions: Discussion[] = [];
  filteredDiscussions: Discussion[] = [];

  // Constructor
  constructor(private discussionService: DiscussionService) { }

  // Methods
  ngOnInit(): void {
    this.discussionService.getDiscussions().subscribe(data => {
      this.discussions = data;
      this.filteredDiscussions = this.discussions;
    });
  }

  private _listFilter: string = '';
  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    this.filteredDiscussions = this.performFilter(value);
  }

  performFilter(filterBy: string): Discussion[] {
    if (!filterBy) {
      return this.discussions;
    }

    filterBy = filterBy.toLocaleLowerCase();
    return this.discussions.filter((discussion: Discussion) =>
      discussion.title.toLocaleLowerCase().includes(filterBy) || discussion.content.toLocaleLowerCase().includes(filterBy)
    );
  }

  minimizeText(text: string, length: number = 600): string {
    if(text.length > length){
      return text.substring(0,length)+'...';
    }else {
      return text
    }
  }
}
