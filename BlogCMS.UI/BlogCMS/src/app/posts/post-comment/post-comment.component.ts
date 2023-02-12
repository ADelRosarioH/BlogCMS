import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { EditorOption } from 'angular-markdown-editor';
import { MarkdownService } from 'ngx-markdown';
import { ToastrService } from 'ngx-toastr';
import { Post, PostStatus, PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-post-comment',
  templateUrl: './post-comment.component.html',
  styleUrls: ['./post-comment.component.scss']
})
export class PostCommentComponent {
  @Input() post: Partial<Post>;
  newComment: string = '';
  editorOptions: EditorOption;
  PostStatus = PostStatus;

  constructor(private markdownService: MarkdownService,
    private postService: PostService,
    private toastrService: ToastrService) {
    this.editorOptions = {
      parser: (val) => this.markdownService.parse(val.trim())
    };
    this.post = {};
  }

  save() {
    const postId = this.post?.id as string;
    this.postService.comment(postId, this.newComment)
      .subscribe((comment: any) => {
        if (this.post.comments) {
          this.post.comments.push(comment);
        } else {
          this.post.comments = [];
          this.post.comments.push(comment);
        }
        this.newComment = "";
        this.toastrService.success("comment published.");
      })
  }
}
