import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Post, PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.scss']
})
export class NewPostComponent {
  newPost: Post;

  constructor(private postService: PostService, private router: Router) {
    this.newPost = {
      title: '',
      content: ''
    } as Post;
  }

  save() {
    this.postService.createNew({
      title: this.newPost.title,
      content: this.newPost.content
    }).subscribe((post: Post) => {
      this.router.navigate(['posts', post.id])
    });
  }
}
