import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators' ;
import {JwtHelperService} from '@auth0/angular-jwt';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
baseurl = 'http://localhost:5000/api/auth/';
jwtHelper = new JwtHelperService();
decodedToken:any;
constructor(private http: HttpClient) { }
login(model: any) 
{ 
 return this.http.post(this.baseurl + 'login' , model)
 .pipe(
   map(
     (response: any) =>
     {
       const user = response;
       if (user) 
       {
         localStorage.setItem('token', user.token);
       }
     }
     )
 )
}

register(model:any)
{
  return this.http.post(this.baseurl + 'register',model);
}
loggedIn()
{
  var token= localStorage.getItem('token');
  this.decodedToken= this.jwtHelper.decodeToken(token);
  return !this.jwtHelper.isTokenExpired(token);
}
}
