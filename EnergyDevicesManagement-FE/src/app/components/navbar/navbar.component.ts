import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/service/auth/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  role: string = '';
  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.role = this.authService.getRoleFromToken();
  }
}
