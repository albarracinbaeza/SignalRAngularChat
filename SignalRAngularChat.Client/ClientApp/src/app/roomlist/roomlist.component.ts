import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { DataService } from '../services/data.service';
import { Room } from '../models/room';
import * as signalR from "@aspnet/signalr";
import { SignalRService } from '../services/signal-r.service';
import { Chat } from '../models/chat';
import { User } from '../models/user';

@Component({
  selector: 'app-roomlist',
  templateUrl: './roomlist.component.html',
  styleUrls: ['./roomlist.component.scss']
})
export class RoomlistComponent implements OnInit {

  user: User = new User();
  displayedColumns: string[] = ['RoomName'];
  rooms: Room[] = [];
  isLoadingResults = true;

  private hubConnection: signalR.HubConnection

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dataService: DataService,
    private signalRService: SignalRService,
    public datepipe: DatePipe
  ) {
    this.user = JSON.parse(sessionStorage.getItem('userSAChat'));
    this.subscribeToEvents(); 
  }

  ngOnInit(): void {
    this.listRooms();
  }

  private subscribeToEvents(): void {
    this.signalRService.onListRooms.subscribe(() => {
      this.listRooms();
    });
  }  

  listRooms() {
    this.isLoadingResults = true;
    this.dataService.listRooms().subscribe(rooms => {
      this.rooms = rooms;
      this.isLoadingResults = false;
    });
  }

  enterChatRoom(roomId: number) {
    this.dataService.getRoomUserByRoomIdAndUserId(roomId, this.user.UserId).subscribe((roomUser) => {      
      if (roomUser) {
        roomUser.Status = 'online';
        this.dataService.updateRoomUser(roomUser).subscribe(() => {
          this.goToChatRoom(roomId);
        })
      } else {
        roomUser = {
          RoomId: roomId,
          UserId: this.user.UserId,
          Status: 'online'
        };        
        this.dataService.insertRoomUser(roomUser).subscribe(() => {
          this.goToChatRoom(roomId);
        });
      }
    });
  }

  goToChatRoom(roomId: number) {
    const chat = new Chat();
    chat.RoomId = roomId;
    chat.UserId = this.user.UserId;
    chat.Date = new Date();
    chat.Message = `${this.user.UserName} enter the room`;
    chat.Type = 'join';

    this.signalRService.SendChat(chat).then(() => {
      this.dataService.insertChat(chat).subscribe(() => {
        this.router.navigate(['/chatroom', roomId]);
      });
    });    
  }

  logout(): void {
    sessionStorage.removeItem('userSAChat');
    this.router.navigate(['/login']);
  }
}
