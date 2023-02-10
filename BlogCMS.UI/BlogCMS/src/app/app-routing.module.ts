import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInComponent } from './auth/sign-in/sign-in.component';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { PostEditorComponent } from './posts/post-editor/post-editor.component';
import { PostListComponent } from './posts/post-list/post-list.component';
import { PostComponent } from './posts/post/post.component';
import { AuthGuardService as AuthGuard } from './services/auth.service';

const routes: Routes = [
  { path: '', component: PostListComponent, canActivate: [AuthGuard] },
  { path: 'posts', component: PostListComponent, canActivate: [AuthGuard] },
  { path: 'posts/:postId', component: PostComponent, canActivate: [AuthGuard] },
  { path: 'posts/:postId/edit', component: PostEditorComponent, canActivate: [AuthGuard] },
  { path: 'signin', component: SignInComponent },
  { path: 'signup', component: SignUpComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
