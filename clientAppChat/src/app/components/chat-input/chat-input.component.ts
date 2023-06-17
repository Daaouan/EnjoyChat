import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-chat-input',
  templateUrl: './chat-input.component.html',
  styleUrls: ['./chat-input.component.css']
})
export class ChatInputComponent {
  content: string='';
  @Output() contentEmitter = new EventEmitter();

sendMessage() {
  if(this.content.trim() !=="")
  {
    this.contentEmitter.emit(this.content);
  }

  this.content='';
}

}
