import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs';
import { CreateOrUpdatePost, Post, PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-edit-post',
  templateUrl: './edit-post.component.html',
  styleUrls: ['./edit-post.component.scss']
})
export class EditPostComponent {
  post?: Post;
  
  constructor(private route: ActivatedRoute, private postService: PostService) {
       
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
    }).subscribe();
  }
}
