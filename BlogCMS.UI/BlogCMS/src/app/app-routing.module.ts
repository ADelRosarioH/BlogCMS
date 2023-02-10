import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInComponent } from './auth/sign-in/sign-in.component';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { DraftListComponent } from './posts/draft-list/draft-list.component';
import { EditPostComponent } from './posts/edit-post/edit-post.component';
import { PostListComponent } from './posts/post-list/post-list.component';
import { PostComponent } from './posts/post/post.component';
import { AuthGuardService as AuthGuard } from './services/auth.service';

const routes: Routes = [
  { path: '', component: PostListComponent, canActivate: [AuthGuard] },
  { path: 'posts', component: PostListComponent, canActivate: [AuthGuard] },
  { path: 'posts/:postId', component: PostComponent, canActivate: [AuthGuard] },
  { path: 'posts/:postId/edit', component: EditPostComponent, canActivate: [AuthGuard] },
  { path: 'drafts', component: DraftListComponent, canActivate: [AuthGuard] },
  { path: 'signin', component: SignInComponent },
  { path: 'signup', component: SignUpComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
