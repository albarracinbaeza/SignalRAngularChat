import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { User } from '../models/user';
import { Room } from '../models/room';
import { Observable } from 'rxjs';
import { Chat } from '../models/chat';
import { RoomUser } from '../models/room-user';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  usersURL = 'https://localhost:44304/api/Users';
  roomsURL = 'https://localhost:44304/api/Rooms';
  chatsURL = 'https://localhost:44304/api/Chats';
  roomUsersURL = 'https://localhost:44304/api/RoomUsers';

  constructor(private httpClient: HttpClient) { }

  public getUserByUserName(userName: string): Observable<User> {
    return this.httpClient.get<User>(`${this.usersURL}/` + userName);
  }
  public listUsersByRoomIdAndStatus(roomId: number, status: string): Observable<User[]> {
    let params = new HttpParams();
    params = params.append('roomId', roomId.toString());
    params = params.append('status', status);
    return this.httpClient.get<User[]>(`${this.usersURL}/`, { params: params });
  }
  public listUsersByRoomIdAndUserIdAndStatus(roomId: number, userId: number, status: string): Observable<User[]> {
    let params = new HttpParams();
    params = params.append('roomId', roomId.toString());
    params = params.append('userId', userId.toString());
    params = params.append('status', status);
    return this.httpClient.get<User[]>(`${this.usersURL}/`, { params: params });
  }
  public listUsersByRoomId(roomId: number): Observable<User[]> {
    let params = new HttpParams();
    params = params.append('roomId', roomId.toString());
    return this.httpClient.get<User[]>(`${this.usersURL}/`, { params: params });
  }
  public insertUser(user: User):Observable<User> {
    return this.httpClient.post<User>(`${this.usersURL}/`,user);
  }

  public getRoomByRoomId(roomId: number): Observable<Room> {
    return this.httpClient.get<Room>(`${this.roomsURL}/` + roomId);
  }
  public getRoomByRoomName(roomName: string): Observable<Room> {
    let params = new HttpParams();
    params = params.append('roomName', roomName);
    return this.httpClient.get<Room>(`${this.roomsURL}/`, { params: params });
  }
  public listRooms(): Observable<Room[]> {
    return this.httpClient.get<Room[]>(`${this.roomsURL}/`);
  }
  public insertRoom(room: Room): Observable<Room> {
    return this.httpClient.post<Room>(`${this.roomsURL}/`, room);
  }

  public listChatsByRoomId(roomId: number): Observable<Chat[]> {
    let params = new HttpParams();
    params = params.append('roomId', roomId.toString());
    return this.httpClient.get<Chat[]>(`${this.chatsURL}/`, { params: params });
  }
  public insertChat(chat: Chat): Observable<Chat> {
    return this.httpClient.post<Chat>(`${this.chatsURL}/`, chat);
  }

  public getRoomUserByRoomIdAndUserId(roomId: number,userId: number): Observable<RoomUser> {
    let params = new HttpParams();
    params = params.append('roomId', roomId.toString());
    params = params.append('userId', userId.toString());
    return this.httpClient.get<RoomUser>(`${this.roomUsersURL}/`, { params: params });
  }
  public insertRoomUser(roomUser: RoomUser): Observable<RoomUser> {
    return this.httpClient.post<RoomUser>(`${this.roomUsersURL}/`, roomUser);
  }
  public updateRoomUser(roomUser: RoomUser): Observable<RoomUser> {
    return this.httpClient.put<RoomUser>(`${this.roomUsersURL}/`, roomUser);
  }

}
