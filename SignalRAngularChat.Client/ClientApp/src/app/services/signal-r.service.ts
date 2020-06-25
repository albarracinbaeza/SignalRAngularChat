import { Injectable, EventEmitter } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { Chat } from '../models/chat';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  onListRooms = new EventEmitter();
  onReciveChat = new EventEmitter<Chat>();
  onListUsersByRoomId = new EventEmitter<number>();

  connectionEstablished = new EventEmitter<boolean>();

  private connectionIsEstablished = false;
  private _hubConnection: signalR.HubConnection;

  constructor() {
    this.createConnection();
    this.registerClientEventsOnServer();
    this.startConnection();  
  }

  private createConnection() {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44355/ChatHub')
      .build();
  }

  private startConnection(): void {
    this._hubConnection
      .start()
      .then(() => {
        this.connectionIsEstablished = true;
        console.log('Hub connection started');
        this.connectionEstablished.emit(true);
      })
      .catch(err => {
        console.log('Error while establishing connection, retrying...');
        setTimeout(function () { this.startConnection(); }, 5000);
      });
  }

  private registerClientEventsOnServer(): void {
    this._hubConnection.on('ListRoomsClient', () => {
      this.onListRooms.emit();
    });
    this._hubConnection.on('ReciveChatClient', (chat: Chat) => {
      this.onReciveChat.emit(chat);
    });
    this._hubConnection.on('ListUsersByRoomIdClient', (roomId: number) => {
      this.onListUsersByRoomId.emit(roomId);
    });
  }
  public SendChat(chat: Chat) {
    return this._hubConnection.send("SendChatServer", chat);
  }
}
