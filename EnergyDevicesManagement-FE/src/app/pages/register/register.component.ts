import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegisterUserDto } from 'src/app/api-management/client-user-management';
import { AuthService } from 'src/app/service/auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public registerForm!: FormGroup;
  constructor(private readonly authService: AuthService,
    private readonly formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      'email': ['', Validators.required],
      'password': ['', Validators.required],
      'name': ['', Validators.required],
      'role': ['', Validators.required]
    })
  }


  registerUser() {
    if (!this.registerForm.valid) {
      alert("Provided information are not valid")
    }
    var registerDto = this.registerForm.value;
    this.authService.registerUser(registerDto);
    this.registerForm.reset();
  }
}
