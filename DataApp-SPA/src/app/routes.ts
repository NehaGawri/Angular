import {Routes} from '@angular/router'
import { HomeComponent } from './home/home.component';
import { MemberlistComponent } from './memberlist/memberlist.component';
import { ListComponent } from './list/list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path:'',
        runGuardsAndResolvers:'always',
        canActivate:[AuthGuard],
        children:[
            {path: 'memberlist', component: MemberlistComponent},
            {path: 'list', component: ListComponent},
            {path: 'messages', component: MessagesComponent}
        ]
    },

    {path: '**', redirectTo: '', pathMatch: 'full'}
];
