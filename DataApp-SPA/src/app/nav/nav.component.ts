import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyServiceService } from '../_services/alertifyService.service';
import {  Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model: any = {};
  constructor(public authService: AuthService, private alertify:AlertifyServiceService
    ,private route:Router) { }

  ngOnInit() {
  }
 login() {
   console.log(this.model);
this.authService.login(this.model).subscribe
(
 next => { this.alertify.success("success")},
 error => { this.alertify.error(error);},
 () =>{this.route.navigate(['/memberlist']) }
);

 }

 LoggedIn()
 {
   return this.authService.loggedIn();

 }

 LogOut()
 {
   localStorage.removeItem('token');
   this.alertify.message('logged out');
   this.route.navigate(['/home'])
 }
}
