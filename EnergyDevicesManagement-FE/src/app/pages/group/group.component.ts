import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserDto } from 'src/app/api-management/client-user-management';
import { AuthService } from 'src/app/service/auth/auth.service';
import { GroupService } from 'src/app/service/group/group.service';
import { UsersService } from 'src/app/service/users/users.service';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css'],
})
export class GroupComponent implements OnInit {
  public createGroupForm!: FormGroup;
  users: UserDto[] = [];

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly userService: UsersService,
    private readonly authService: AuthService,
    private readonly groupService: GroupService
  ) {}

  ngOnInit(): void {
    this.userService.getUsers().subscribe({
      next: (response) =>
        (this.users = response.filter(
          (u) => u.id !== this.authService.getIdFromToken()
        )),
    });
    this.createGroupForm = this.formBuilder.group({
      usersId: ['', Validators.required],
      groupName: ['', Validators.required],
    });
  }
  createGroup(): void {
    this.groupService
      .createGroup(
        this.createGroupForm.value.groupName,
        this.createGroupForm.value.usersId
      )
      .subscribe({
        next: () => this.createGroupForm.reset(),
      });
  }
}
