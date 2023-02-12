import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { EditorOption } from 'angular-markdown-editor';
import { MarkdownService } from 'ngx-markdown';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AuthService, Roles, User } from 'src/app/services/auth.service';
import { Post, PostService, PostStatus } from 'src/app/services/post.service';

@Component({
  selector: 'app-post-feedback',
  templateUrl: './post-feedback.component.html',
  styleUrls: ['./post-feedback.component.scss']
})
export class PostFeedbackComponent {
  @Input() post: Partial<Post>;
  newFeedback: string = '';
  editorOptions: EditorOption;
  Roles = Roles;
  PostStatus = PostStatus;
  user: Observable<User | undefined>;

  constructor(private markdownService: MarkdownService,
    private postService: PostService,
    private authService: AuthService,
    private toastrService: ToastrService,
    private router: Router) {
    this.editorOptions = {
      parser: (val) => this.markdownService.parse(val.trim())
    };
    this.post = {};
    this.user = this.authService.currentUser();
  }

  reject() {
    const postId = this.post?.id as string;
    this.postService.reject(postId, this.newFeedback)
      .subscribe((feedback: any) => {
        if (this.post.feedbacks) {
          this.post.feedbacks.push(feedback);
        } else {
          this.post.feedbacks = [];
          this.post.feedbacks.push(feedback);
        }
        this.newFeedback = "";
        this.toastrService.success("Feedback provided.");
      })
  }

  approve() {
    const postId = this.post?.id as string;
    this.postService.approve(postId)
      .subscribe(() => {
        this.router.navigate(['posts']);
        this.toastrService.success("Post approved!");
      });
  }
}
