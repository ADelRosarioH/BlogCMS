import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) { }

  getPostsByStatus(status: PostStatus): Observable<Post[]> {
    var url = new URL(`${environment.apiUrl}/posts/all`);

    url.searchParams.append("status", status.toString());

    return this.http.get<Post[]>(url.href);
  }

  getPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(`${environment.apiUrl}/posts`);
  }

  getPostById(postId: string): Observable<Post> {
    return this.http.get<Post>(`${environment.apiUrl}/posts/${postId}`);
  }

  createNew(data: CreateOrUpdatePost): Observable<Post> {
    return this.http.post<Post>(`${environment.apiUrl}/posts/`, data);
  }

  update(postId: string, data: CreateOrUpdatePost): Observable<Post> {
    return this.http.put<Post>(`${environment.apiUrl}/posts/${postId}`, data);
  }

  submit(postId: string): Observable<{}> {
    return this.http.post(`${environment.apiUrl}/posts/${postId}/submit`, {});
  }

  approve(postId: string): Observable<{}> {
    return this.http.post(`${environment.apiUrl}/posts/${postId}/approve`, {});
  }

  reject(postId: string, comment: string): Observable<{}> {
    return this.http.post(`${environment.apiUrl}/posts/${postId}/reject`, {
      comment
    });
  }


  comment(postId: string, content: string): Observable<Comment> {
    return this.http.post<Comment>(`${environment.apiUrl}/posts/${postId}/comments`, {
      content
    });
  }
}

export class Post {
  id: string = "";
  title: string = "";
  content: string = "";
  status: PostStatus = PostStatus.Draft;
  statusDescription: string = "";
  createdBy: string = "";
  createdAt: string = "";
  feedbacks: Feedback[] = [];
  comments: Comment[] = [];
}

export class CreateOrUpdatePost {
  title: string = "";
  content: string = "";
}

export class Comment {
  content: string = "";
  createdBy: string = "";
  createdAt: string = "";
}

export class NewComment {
  content: string = "";
}

export class Feedback {
  comment: string = "";
}

export enum PostStatus {
  Draft,
  Pending,
  Approved,
  Rejected,
}