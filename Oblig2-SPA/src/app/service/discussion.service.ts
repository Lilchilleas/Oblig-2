import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';



@Injectable({
  providedIn: 'root'
})




export class DiscussionService {

  //Attributes
  private apiUrl = 'http://localhost:5000/api/discussion';

  //Constructor
  constructor(private http: HttpClient) { }

  //Methods
  getDiscussions(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}`).pipe(
      map(response => response)
    );
  }

  getDiscussion(id: number): Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  createDiscussion(discussion: any): Observable<any>{
    return this.http.post<any>(`${this.apiUrl}/CreateDiscussion`,discussion);
  }

  createComment(id: number, comment: any): Observable<any>{
    return this.http.post<any>(`${this.apiUrl}/${id}/CreateComment`,comment);
  }

  updateComment(id: number, updatedDiscussion: any){
    return this.http.put<any>(`${this.apiUrl}/${id}`,updatedDiscussion);
  }

  deleteDiscussion(id: number){
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }

}
