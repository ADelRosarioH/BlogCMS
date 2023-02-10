import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent {
  signUpForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required]),
    role: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });

  constructor(
    private authService: AuthService, 
    private router: Router, 
    private toastr: ToastrService) { }

  ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['']);
    }
  }

  doSignUp() {
    const newUser = this.signUpForm.value as { userName: string, email: string, role: string, password: string };    
    this.authService.signUp(newUser).subscribe((): void => {      
      this.router.navigate(["signin"]);
    }, (err) => {
      this.toastr.error(err.error.message, "Oops! Something went wrong, sorry...");
    });
  }
}
