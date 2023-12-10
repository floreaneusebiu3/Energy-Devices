import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UserDto } from 'src/app/api-management/client-user-management';
import { DialogCreateUserComponent } from 'src/app/components/dialogCreateUser/dialog-create-user/dialog-create-user.component';
import { DialogEditUserComponent } from 'src/app/components/dialogEditUser/dialog-edit-user/dialog-edit-user.component';
import { UsersService } from 'src/app/service/users/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
})
export class UsersComponent {
  public createUserText: string = 'Create new user';
  public displayedColumns: { [key: string]: string } = {
    name: 'Name',
    email: 'Email',
    role: 'Role',
  };

  public dataSource: any[] = [];

  constructor(
    private readonly usersService: UsersService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.usersService.getUsers().subscribe((users) => {
      this.dataSource = users;
    });
  }

  openCreateDialog() {
    var dialogRef = this.dialog.open(DialogCreateUserComponent);
    dialogRef.afterClosed().subscribe(() => this.refresh());
  }

  deleteUser(userDto: UserDto) {
    this.usersService.deleteUser(userDto.id!);
    this.refresh();
  }

  editUser(userDto: UserDto) {
    var dialogRef = this.dialog.open(DialogEditUserComponent, {
      data: userDto,
    });
    dialogRef.afterClosed().subscribe(() => this.refresh());
  }

  refresh() {
    window.location.reload();
  }
}
