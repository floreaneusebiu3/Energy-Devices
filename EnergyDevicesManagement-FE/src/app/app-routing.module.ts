import { NgModule } from '@angular/core';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { UsersComponent } from './pages/users/users.component';
import { RouterModule, Routes } from '@angular/router';
import { DevicesComponent } from './pages/devices/devices.component';
import { UserDevicesComponent } from './pages/user-devices/user-devices.component';
import { MapUseDeviceComponent } from './pages/map-use-device/map-use-device.component';
import { ClientComponent } from './pages/client/client.component';
import { AuthenticationGuard } from './guards/authentication/authentication.guard';
import { AuthorizationGuard } from './guards/authorization/authorization.guard';
import { ChartDialogComponent } from './components/chart-dialog/chart-dialog.component';
import { ChatComponent } from './pages/chat/chat.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'chart', component: ChartDialogComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'admin/users',
    component: UsersComponent,
    canActivate: [AuthenticationGuard, AuthorizationGuard],
  },
  {
    path: 'admin/devices',
    component: DevicesComponent,
    canActivate: [AuthenticationGuard, AuthorizationGuard],
  },
  {
    path: 'admin/user-devices',
    component: UserDevicesComponent,
    canActivate: [AuthenticationGuard, AuthorizationGuard],
  },
  {
    path: 'admin/map-user-devices',
    component: MapUseDeviceComponent,
    canActivate: [AuthenticationGuard, AuthorizationGuard],
  },
  {
    path: 'chat',
    component: ChatComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'client/devices',
    component: ClientComponent,
    canActivate: [AuthenticationGuard],
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login', pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
