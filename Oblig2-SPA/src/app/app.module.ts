import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http'
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DiscussionsListComponent } from './discussions-list/discussions-list.component';
import { DiscussionDetailComponent } from './discussion-detail/discussion-detail.component';
import { CommentComponent } from './comment/comment.component';
import { CreateDiscussionComponent } from './create-discussion/create-discussion.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CreateCommentComponent } from './create-comment/create-comment.component';
import { DiscussionService } from './service/discussion.service';
import { AuthService } from './service/auth.service';
import { ErrorInterceptor, ErrorInterceptorProvider } from './service/error.interceptor';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';





const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'discussions', component: DiscussionsListComponent },
  { path: 'discussion/:id', component: DiscussionDetailComponent },
  { path: 'create-discussion', component: CreateDiscussionComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

]

@NgModule({
  declarations: [																					
    AppComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
      DiscussionsListComponent,
      DiscussionDetailComponent,
      CommentComponent,
      CreateDiscussionComponent,
      CreateCommentComponent
   ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes),
    MatSnackBarModule,
    BrowserAnimationsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot()
  
  ],
  providers: [
    DiscussionService,
    AuthService,
    ErrorInterceptorProvider,
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
