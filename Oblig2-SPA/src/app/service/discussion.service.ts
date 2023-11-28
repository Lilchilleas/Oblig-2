import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { DiscussionDto } from '../models/discussion-dto';
import { CreateDiscussionDto } from '../models/createDiscussion-dto';
import { CreateCommentDto } from '../models/createComment-dto';
import { CommentDto } from '../models/comment-dto';
import { UpdateDiscussionDto } from '../models/updateDiscussion-dto';
import { Discussion } from '../discussions/discussions-list/discussions';


@Injectable({
  providedIn: 'root'
})


export class DiscussionService {

  //Attributes
  private apiUrl = 'http://localhost:5000/api/discussion';

  //Constructor
  constructor(private http: HttpClient) { }

  //Methods
  getDiscussions(): Observable<Discussion[]> {
    return this.http.get<Discussion[]>(`${this.apiUrl}`);
  }

  getDiscussion(id: number): Observable<DiscussionDto>{
    return this.http.get<DiscussionDto>(`${this.apiUrl}/${id}`);
  }

  createDiscussion(discussion: CreateDiscussionDto): Observable<DiscussionDto>{
    return this.http.post<DiscussionDto>(`${this.apiUrl}/CreateDiscussion`,discussion);
  }

  createComment(id: number, comment: CreateCommentDto): Observable<CommentDto>{
    return this.http.post<any>(`${this.apiUrl}/${id}/CreateComment`,comment);
  }

  updateComment(id: number, updatedDiscussion: UpdateDiscussionDto): Observable<DiscussionDto>{
    return this.http.put<any>(`${this.apiUrl}/${id}`,updatedDiscussion);
  }

  deleteDiscussion(id: number):Observable<void>{
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }

}
