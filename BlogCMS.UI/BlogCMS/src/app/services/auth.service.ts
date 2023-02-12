import { HttpClient, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable } from 'rxjs';
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

  isSignedInInSubject = new BehaviorSubject<boolean>(this.hasValidToken());

  isSignedIn(): Observable<boolean> {
    return this.isSignedInInSubject.asObservable();
  }
  
  hasValidToken(): boolean {
    const token = localStorage.getItem('token');
    // Check whether the token is expired and return
    // true or false
    return !this.jwtHelper.isTokenExpired(token);
  }

  signOut() {
    localStorage.removeItem('token');
    this.isSignedInInSubject.next(false);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  getCurrentUser(): User {
    const token = localStorage.getItem('token') as string;
    const decoded = this.jwtHelper.decodeToken(token);
    const nameClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    const roleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

    const userName = decoded[nameClaim];
    const role = decoded[roleClaim];

    return { userName, role };
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
    if (!this.auth.hasValidToken()) {
      this.router.navigate(['signin']);
      return false;
    }
    return true;
  }
}

export class User {
  userName: string = "";
  role: string = "";
}

export enum Roles {
  Writer = "Writer",
  Editor = "Editor",
  Public = "Public"
}
