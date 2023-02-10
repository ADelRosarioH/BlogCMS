import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
  signInForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });

  constructor(
    private authService: AuthService, 
    private router: Router, 
    private toastr: ToastrService) { }

  ngOnInit(): void {
    if (this.authService.hasValidToken()) {
      this.router.navigate(['']);
    }
  }

  doSignIn() {
    const { userName, password } = this.signInForm.value as { userName: string, password: string };
    this.authService.signIn(userName, password).subscribe(({ token }: { token?: string }): void => {
      localStorage.setItem("token", token as string);
      this.authService.isSignedInInSubject.next(true);
      this.router.navigate([""]);
    }, (err) => {
      this.toastr.error(err.error.message, "Oops! Invalid Username or Password.");
    });
  }
}
