import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { switchMap } from 'rxjs';
import { CreateOrUpdatePost, Post, PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-edit-post',
  templateUrl: './edit-post.component.html',
  styleUrls: ['./edit-post.component.scss']
})
export class EditPostComponent {
  post?: Post;
  
  constructor(private route: ActivatedRoute, 
    private postService: PostService,
    private router: Router,
    private toastrService: ToastrService) {
       
  }

  ngOnInit(): void {
    this.route.params
      .pipe(switchMap((params => {
        const postId = params["postId"];
        return this.postService.getPostById(postId);
      })))
      .subscribe((post: Post) => {
        this.post = post;
      });
  }

  save() {
    const postId = this.post?.id as string;
    this.postService.update(postId, {
      title: this.post!.title,
      content: this.post!.content
    }).subscribe(() => {
      this.toastrService.success("Post saved.");
    });
  }

  submitToReview() {
    const postId = this.post?.id as string;
    this.postService.submit(postId)
    .subscribe(() => {
      this.toastrService.success("Post was sent to review!");
      this.router.navigate(['drafts']);
    });
  }
}
