import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService, User } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'BlogCMS - Zemoga';
  isUserSignedIn: Observable<boolean>;
  user: User = { userName: ""};

  constructor(private authService: AuthService, private router: Router) {
    this.isUserSignedIn = this.authService.isSignedIn();
  }

  ngOnInit(): void {
    this.user = this.authService.getCurrentUser();
  }

  signOut() {
    this.authService.signOut();
    this.router.navigate(['/signin']);
  }
}
