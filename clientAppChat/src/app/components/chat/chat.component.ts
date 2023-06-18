import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ChatService } from 'src/app/services/chat.service';
import { PrivateChatComponent } from '../private-chat/private-chat.component';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, OnDestroy{

  @Output() closeChatEmmitter=new EventEmitter();

  constructor(public chatService: ChatService , private modalService: NgbModal){}

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

  openPrivateChat(toUser: string){
    const modalRef = this.modalService.open(PrivateChatComponent);
    modalRef.componentInstance.toUser = toUser;
  }
}
