import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { Post, PostService, PostStatus } from 'src/app/services/post.service';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.scss']
})
export class PostListComponent implements OnInit {
  public posts: Post[] = [];

  constructor(private postService: PostService) {
    
  }

  ngOnInit(): void {
    this.postService.getPostsByStatus(PostStatus.Approved)
      .pipe(
        map((posts: Post[]) => {
          this.posts = posts;
        })
      )
      .subscribe()
  }

}
