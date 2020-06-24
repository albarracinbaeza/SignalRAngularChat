import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroupDirective, NgForm, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DataService } from '../services/data.service';


export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-addroom',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})
export class AddRoomComponent implements OnInit {

  roomForm: FormGroup;
  nickname = '';
  roomname = '';
  matcher = new MyErrorStateMatcher();

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private dataService: DataService
  ) { }

  ngOnInit(): void {
    this.roomForm = this.formBuilder.group({
      'RoomName': [null, Validators.required]
    });
  }

  onFormSubmit(form: any) {
    const roomForm = form;
    this.dataService.getRoomByRoomName(roomForm.RoomName).subscribe(room => {
      if (room) {
        this.snackBar.open('Room name already exist!',null, {
          duration: 3000,
        });
      } else {
        room = {
          RoomName: roomForm.RoomName
        };
        this.dataService.insertRoom(room).subscribe(() => {
          this.goToRoomList();
        });
      }
    });
  }

  goToRoomList() {
    this.router.navigate(['/roomlist']);
  }

}
