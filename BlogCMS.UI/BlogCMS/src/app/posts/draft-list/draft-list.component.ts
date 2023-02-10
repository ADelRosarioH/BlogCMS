import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { Post, PostService, PostStatus } from 'src/app/services/post.service';

@Component({
  selector: 'app-draft-list',
  templateUrl: './draft-list.component.html',
  styleUrls: ['./draft-list.component.scss']
})
export class DraftListComponent implements OnInit {
  PostStatus = PostStatus;
  posts: Post[] = [];
  
  constructor(private postService: PostService) {
    
  }

  ngOnInit(): void {
    this.postService.getPosts()
      .pipe(
        map((posts: Post[]) => {
          this.posts = posts;
        })
      )
      .subscribe()
  }
}
