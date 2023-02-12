import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService, User, Roles } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'BlogCMS - Zemoga';
  isUserSignedIn: Observable<boolean>;
  user: User;
  Roles = Roles;

  constructor(private authService: AuthService, private router: Router) {
    this.isUserSignedIn = this.authService.isSignedIn();
    this.user = this.authService.getCurrentUser();
  }

  signOut() {
    this.authService.signOut();
    this.router.navigate(['/signin']);
  }
}
