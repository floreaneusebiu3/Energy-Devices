import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { UsersComponent } from './pages/users/users.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TableComponent } from './components/table/table.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { TokenInterceptor } from './service/auth/interceptor/auth.interceptor';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule, MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { DevicesComponent } from './pages/devices/devices.component';
import { UserDevicesComponent } from './pages/user-devices/user-devices.component';
import { MapUseDeviceComponent } from './pages/map-use-device/map-use-device.component';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { DialogCreateUserComponent } from './components/dialogCreateUser/dialog-create-user/dialog-create-user.component';
import { DialogCreateDeviceComponent } from './components/dialogCreateDevice/dialog-create-device/dialog-create-device.component';
import { DialogEditDeviceComponent } from './components/dialogEditDevice/dialog-edit-device/dialog-edit-device.component';
import { DialogEditUserComponent } from './components/dialogEditUser/dialog-edit-user/dialog-edit-user.component';
import { ClientComponent } from './pages/client/client.component';
import { ChartDialogComponent } from './components/chart-dialog/chart-dialog.component';
import { CanvasJSAngularChartsModule } from '@canvasjs/angular-charts';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ChatComponent } from './pages/chat/chat.component';
import { GroupComponent } from './pages/group/group.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavbarComponent,
    UsersComponent,
    TableComponent,
    DevicesComponent,
    UserDevicesComponent,
    MapUseDeviceComponent,
    DialogCreateUserComponent,
    DialogCreateDeviceComponent,
    DialogEditDeviceComponent,
    DialogEditUserComponent,
    ClientComponent,
    ChartDialogComponent,
    ChatComponent,
    GroupComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatIconModule,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatCardModule,
    MatInputModule,
    MatOptionModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    CanvasJSAngularChartsModule,
    MatFormFieldModule,
    MatInputModule,
    MatNativeDateModule,
    MatDatepickerModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
