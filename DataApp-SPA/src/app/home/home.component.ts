import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private http:HttpClient) { }
  values: any;
 RegisterModel = false;
  ngOnInit() {
    // this.getValues();
  }
RegisterToggle()

{
this.RegisterModel = true;
}
// getValues()
//    {
//      this.http.get('http://localhost:5000/api/values').subscribe( response => {
//       this.values = response;
//      }, erroe => {
//         console.log(console.error);
//      });
//    }
   cancelRegisterMode(registerModel: boolean)
   {
this.RegisterModel = registerModel;
   }
}
