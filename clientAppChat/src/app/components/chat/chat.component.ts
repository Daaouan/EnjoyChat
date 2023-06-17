import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { ChatService } from 'src/app/services/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, OnDestroy{

  @Output() closeChatEmmitter=new EventEmitter();

  constructor(public chatService: ChatService){}

  ngOnInit(): void {
    this.chatService.createChatConnection();
  }

  ngOnDestroy(): void {
    this.chatService.stopChatConnection();
  }
  
  backToHome(){
    this.closeChatEmmitter.emit();
  }

  sendMessage(content: string) {
     this.chatService.sendMessage(content);
  }
}
