import { HttpClient, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  signIn(userName: string, password: string) {
    return this.http.post(`${environment.apiUrl}/auth/signin`, {
      userName, password
    });
  }

  signUp({ userName, email, role, password }: { userName: string, email: string, role: string, password: string}) {
    return this.http.post(`${environment.apiUrl}/auth/signup`, {
      userName, email, role, password
    });
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    // Check whether the token is expired and return
    // true or false
    return !this.jwtHelper.isTokenExpired(token);
  }

  signOut() {
    localStorage.removeItem('token')
  }

  getToken() {
    return localStorage.getItem('token');
  }

  getCurrentUser(): User {
    const token = localStorage.getItem('token') as string;
    const decoded = this.jwtHelper.decodeToken(token);
    const nameClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    const userName = decoded[nameClaim];

    return { userName };
  }
}

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {

    const token = localStorage.getItem("token");

    if (token) {
      const cloned = req.clone({
        headers: req.headers.set("Authorization",
          `Bearer ${token}`)
      });

      return next.handle(cloned);
    }

    return next.handle(req);
  }
}

@Injectable()
export class AuthGuardService implements CanActivate {

  constructor(public auth: AuthService, public router: Router) { }

  canActivate(): boolean {
    if (!this.auth.isLoggedIn()) {
      this.router.navigate(['signin']);
      return false;
    }
    return true;
  }
}

export class User {
  userName: string = "";
}
