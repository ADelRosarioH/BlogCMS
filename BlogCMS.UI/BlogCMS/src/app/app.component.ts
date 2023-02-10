import { Component, OnInit } from '@angular/core';
import { AuthService, User } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'BlogCMS - Zemoga';
  isUserSignedIn: boolean = false;
  user: User = { userName: ""};

  constructor(private authService: AuthService) {
    
  }

  ngOnInit(): void {
    this.isUserSignedIn = this.authService.isLoggedIn();
    this.user = this.authService.getCurrentUser();
  }
}
