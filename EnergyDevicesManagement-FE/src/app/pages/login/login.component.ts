import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/service/auth/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public authForm!: FormGroup;

  constructor(private readonly formBuilder: FormBuilder,
    private readonly authService: AuthService) { }

  ngOnInit(): void {
    this.authForm = this.formBuilder.group({
      'email': ['', Validators.required],
      'password': ['', Validators.required]
    })
  }

  login() {
    if (!this.authForm.valid) {
      alert("Provided informations are not valid");
      return;
    }
    var loginDto = this.authForm.value;
    this.authService.authenticateUser(loginDto);
    this.authForm.reset();
  }
}
