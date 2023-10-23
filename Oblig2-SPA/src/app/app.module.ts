import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http'

import { AppComponent } from './app.component';
import { DiscussionComponent } from './discussion/discussion.component';
import { ListDiscussionComponent } from './ListDiscussion/ListDiscussion.component';
import { CreateDiscussionComponent } from './CreateDiscussion/CreateDiscussion.component';
import { DeleteDiscussionComponent } from './DeleteDiscussion/DeleteDiscussion.component';

@NgModule({
  declarations: [				
    AppComponent,
      DiscussionComponent,
      ListDiscussionComponent,
      CreateDiscussionComponent,
      DeleteDiscussionComponent
   ],
  imports: [
    BrowserModule,
    HttpClientModule

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
