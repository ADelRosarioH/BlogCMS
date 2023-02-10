import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthService, User } from 'src/app/services/auth.service';
import { Post, PostService, PostStatus } from 'src/app/services/post.service';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.scss']
})
export class PostListComponent implements OnInit {
  posts: Post[] = [];
  isUserSignedIn: boolean = false;
  user: User = { userName: ""};
  
  constructor(private postService: PostService, 
    private authService: AuthService,
    private router: Router) {
    
  }

  ngOnInit(): void {
    this.user = this.authService.getCurrentUser();
    this.postService.getPostsByStatus(PostStatus.Approved)
      .pipe(
        map((posts: Post[]) => {
          this.posts = posts;
        })
      )
      .subscribe()
  }

  signOut() {
    this.authService.signOut();
    this.router.navigate(['signin']);
  }

}
