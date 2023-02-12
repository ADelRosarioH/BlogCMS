import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, switchMap } from 'rxjs';
import { AuthService, User, Roles } from 'src/app/services/auth.service';
import { Post, PostService, PostStatus } from 'src/app/services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {
  post?: Post;

  constructor(private route: ActivatedRoute, 
    private postService: PostService) {
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

  addNewFeedback() {
    
  }


}
