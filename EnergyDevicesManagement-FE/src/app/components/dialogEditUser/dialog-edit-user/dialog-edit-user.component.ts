import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserDto } from 'src/app/api-management/client-user-management';
import { UsersService } from 'src/app/service/users/users.service';

@Component({
  selector: 'app-dialog-edit-user',
  templateUrl: './dialog-edit-user.component.html',
  styleUrls: ['./dialog-edit-user.component.css'],
})
export class DialogEditUserComponent implements OnInit {
  public editUserForm!: FormGroup;

  constructor(
    private readonly formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: UserDto,
    private readonly userService: UsersService
  ) {}

  ngOnInit(): void {
    console.log(this.data);
    this.editUserForm = this.formBuilder.group({
      name: [this.data.name, Validators.required],
      email: [this.data.email, Validators.required],
      role: [this.data.role, Validators.required],
    });
  }

  editUser() {
    var user = this.editUserForm.value;
    this.userService.updateUser(this.data.id!, user);
  }
}
