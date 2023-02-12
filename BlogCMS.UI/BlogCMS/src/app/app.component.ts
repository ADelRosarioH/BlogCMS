import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService, User, Roles } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'BlogCMS - Zemoga';
  isUserSignedIn: Observable<boolean>; 
  user: Observable<User | undefined>;
  Roles = Roles;

  constructor(private authService: AuthService, private router: Router) {
    this.isUserSignedIn = this.authService.isSignedIn();
    this.user = this.authService.currentUser();
  }

  ngOnInit(): void {
  }

  signOut() {
    this.authService.signOut();
    this.router.navigate(['/signin']);
  }
}
