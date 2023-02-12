import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignInComponent } from './auth/sign-in/sign-in.component';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { PostListComponent } from './posts/post-list/post-list.component';
import { PostComponent } from './posts/post/post.component';
import { NewPostComponent } from './posts/new-post/new-post.component';
import { JwtModule } from '@auth0/angular-jwt';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthGuardService, AuthInterceptor } from './services/auth.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DraftListComponent } from './posts/draft-list/draft-list.component';
import { PostEditorComponent } from './posts/post-editor/post-editor.component';
import { PostFeedbackComponent } from './posts/post-feedback/post-feedback.component';
import { MarkdownModule } from 'ngx-markdown';
import { AngularMarkdownEditorModule } from 'angular-markdown-editor';
import { EditPostComponent } from './posts/edit-post/edit-post.component';
import { PendingListComponent } from './posts/pending-list/pending-list.component';

const AuthInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: AuthInterceptor,
  multi: true
};

@NgModule({
  declarations: [
    AppComponent,
    SignInComponent,
    SignUpComponent,
    PostListComponent,
    PostComponent,
    NewPostComponent,
    DraftListComponent,
    PostEditorComponent,
    PostFeedbackComponent,
    EditPostComponent,
    PendingListComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter(request) {
          return localStorage.getItem("token");
        },
      },
    }),
    NgbModule,
    AngularMarkdownEditorModule.forRoot({ iconlibrary: 'fa' }),
    MarkdownModule.forRoot()
  ],
  providers: [
    AuthInterceptorProvider, 
    AuthGuardService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
