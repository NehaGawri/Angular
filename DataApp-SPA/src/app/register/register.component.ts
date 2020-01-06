import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
// @Input ()valueFromHome :any;
@Output () cancelRegister=new EventEmitter();
  constructor(private authservice:AuthService) { }
  model: any = {} ;
  ngOnInit() {
  }
  Register()
  {
    console.log(this.model);
    this.authservice.register(this.model).subscribe(()=>{console.log("registered successfully")}
    ,(error)=>{console.log(error);});
  }
  Cancel()
  {
    console.log('cancelled');
    this.cancelRegister.emit(false);
  }

}
