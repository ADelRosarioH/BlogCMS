import { Component } from '@angular/core';
import { map } from 'rxjs';
import { Post, PostService, PostStatus } from 'src/app/services/post.service';

@Component({
  selector: 'app-pending-list',
  templateUrl: './pending-list.component.html',
  styleUrls: ['./pending-list.component.scss']
})
export class PendingListComponent {
  posts: Post[] = [];
  
  constructor(private postService: PostService) {
    
  }

  ngOnInit(): void {
    this.postService.getPostsByStatus(PostStatus.Pending)
      .pipe(
        map((posts: Post[]) => {
          this.posts = posts;
        })
      )
      .subscribe()
  }
}
