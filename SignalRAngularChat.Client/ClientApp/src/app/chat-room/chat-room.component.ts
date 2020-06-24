import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormControl, FormGroupDirective, NgForm, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ErrorStateMatcher } from '@angular/material/core';
import { DatePipe } from '@angular/common';
import { DataService } from '../services/data.service';
import { User } from '../models/user';
import { Room } from '../models/room';
import { Chat } from '../models/chat';
import { SignalRService } from '../services/signal-r.service';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}


@Component({
  selector: 'app-chat-room',
  templateUrl: './chat-room.component.html',
  styleUrls: ['./chat-room.component.scss']
})
export class ChatRoomComponent implements OnInit {
  @ViewChild('chatcontent') chatcontent: ElementRef;
  scrolltop: number = null;

  chatForm: FormGroup;
  userLogin: User = null;
  room: Room = null;
  message = '';
  usersOnLine: User[] = [];
  users: User[] = [];
  chats: Chat[] = [];
  matcher = new MyErrorStateMatcher();

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    public datepipe: DatePipe,
    private dataService: DataService,
    private signalRService: SignalRService
  ) {
    this.userLogin = JSON.parse(sessionStorage.getItem('userSAChat'));
    this.subscribeToEvents();
    this.getRoom();
  }

  ngOnInit(): void {
    this.chatForm = this.formBuilder.group({
      'Message': [null, Validators.required]
    });
  }

  private subscribeToEvents(): void {
    this.signalRService.onReciveChat.subscribe((chat: Chat) => {
      this.reciveChat(chat);
    });

    this.signalRService.onListUsersByRoomId.subscribe((roomId: number) => {
      if (this.room.RoomId === roomId) {
        this.listUsers();
        this.listUsersOnLine();
      }
    });
  }

  onFormSubmit(form: any) {
    const chatForm = form;

    const newChat: Chat = new Chat();
    newChat.RoomId = this.room.RoomId;
    newChat.UserId = this.userLogin.UserId;
    newChat.Date = new Date();
    newChat.Type = 'message';
    newChat.Message = chatForm.Message

    this.dataService.insertChat(newChat).subscribe(chat => {
      this.signalRService.SendChat(chat).then(() => {
        this.chatForm = this.formBuilder.group({
          'Message': [null, Validators.required]
        });
      });
    });
  }

  exitChat() {
    const newChat: Chat = new Chat();
    newChat.RoomId = this.room.RoomId;
    newChat.UserId = this.userLogin.UserId;
    newChat.Date = new Date();
    newChat.Type = 'exit';
    newChat.Message = `${this.userLogin.UserName} leave the room`;

    this.dataService.insertChat(newChat).subscribe((chat) => {
      this.signalRService.SendChat(chat).then(() => {
        this.dataService.getRoomUserByRoomIdAndUserId(chat.RoomId, chat.UserId).subscribe((roomUser) => {
          if (roomUser) {
            roomUser.Status = 'offline';
            this.dataService.updateRoomUser(roomUser).subscribe(() => {
              this.router.navigate(['/roomlist']);
            })
          }
        });
      });      
    });
  }

  listChats() {
    this.chats = [];
    this.dataService.listChatsByRoomId(this.room.RoomId).subscribe(chats => {
      this.chats = chats;
      setTimeout(() => {
        this.scrolltop = this.chatcontent.nativeElement.scrollHeight
      }, 500);
    });
  }

  reciveChat(chat: Chat) {
    this.chats.push(chat);
    setTimeout(() => {
      this.scrolltop = this.chatcontent.nativeElement.scrollHeight
    }, 500);
  }

  listUsersOnLine() {
    this.usersOnLine = [];
    this.dataService.listUsersByRoomIdAndUserIdAndStatus(this.room.RoomId, this.userLogin.UserId, 'online').subscribe(users => {
      this.usersOnLine = users;
    });
  }

  listUsers() {
    this.users = [];
    this.dataService.listUsersByRoomId(this.room.RoomId).subscribe(users => {
      this.users = users;
    });
  }

  getRoom() {
    this.dataService.getRoomByRoomId(this.route.snapshot.params.roomId).subscribe(room => {
      this.room = room;
      this.listUsers();
      this.listUsersOnLine();
      this.listChats();      
    });
  }

  getUserByUserId(userId: number): User {
    let user: User = this.users.find(u => u.UserId === userId);
    if (!user) {
      user = new User();
    }
    return user;
  }

}

