import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyServiceService } from '../_services/alertifyService.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model: any = {};
  constructor(public authService: AuthService, private alertify:AlertifyServiceService) { }

  ngOnInit() {
  }
 login() {
   console.log(this.model);
this.authService.login(this.model).subscribe
(
 next => { this.alertify.success("success")},
 error => { this.alertify.error(error);}
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
 }
}
