import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { DataService } from '../services/data.service';
import { User } from '../models/user';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  nickname = '';
  matcher = new MyErrorStateMatcher();

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private dataService: DataService
  ) { }

  ngOnInit(): void {
    if (sessionStorage.getItem('userSAChat')) {
      this.goToRoomList();
    }
    this.loginForm = this.formBuilder.group({
      'nickname': [null, Validators.required]
    });
  }

  onFormSubmit(form: any) {
    const login = form;
    this.dataService.getUserByUserName(login.nickname).subscribe(user => {
      if (user) {
        sessionStorage.setItem('userSAChat', JSON.stringify(user));
        this.goToRoomList();
      } else {
        const newUser: User = {
          UserName: login.nickname
        };
        this.dataService.insertUser(newUser).subscribe((user) => {
          sessionStorage.setItem('userSAChat', JSON.stringify(user));
          this.goToRoomList();
        });
      }
    });
  }

  goToRoomList() {
    this.router.navigate(['/roomlist']);
  }

}
