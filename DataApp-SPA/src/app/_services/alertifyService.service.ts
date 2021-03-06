import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';
@Injectable({
  providedIn: 'root'
})
export class AlertifyServiceService {

constructor() { }

confirm(message: any , okCallback: () => any)
{
  alertify.confirm(message, (e: any) => {
    if (e) { okCallback(); }
    else {}
  })
}

success(message: any)
{
  alertify.success(message);
}
warning(message: any)
{
  alertify.warning(message);
}
message(message: any)
{
  alertify.message(message);
}
error(message: any)
{
  alertify.error(message);
}
}
