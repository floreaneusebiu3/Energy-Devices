import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UsersService } from 'src/app/service/users/users.service';

@Component({
  selector: 'app-dialog-create-user',
  templateUrl: './dialog-create-user.component.html',
  styleUrls: ['./dialog-create-user.component.css'],
})
export class DialogCreateUserComponent implements OnInit {
  public createUserForm!: FormGroup;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly userService: UsersService
  ) {}

  ngOnInit(): void {
    this.createUserForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      role: ['', Validators.required],
    });
  }

  insertUser() {
    if (!this.createUserForm.valid) {
      window.alert('Provided informations are not valid');
    }
    var user = this.createUserForm.value;
    var created = this.userService.insertUser(user);
    this.createUserForm.reset();
  }
}
