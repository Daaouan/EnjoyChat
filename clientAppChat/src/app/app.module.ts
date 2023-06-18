import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { HomeComponent } from './components/home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@microsoft/signalr';
import{HttpClientModule} from '@angular/common/http';
import { ChatComponent } from './components/chat/chat.component';
import { ChatInputComponent } from './components/chat-input/chat-input.component';
import { MessagesComponent } from './components/messages/messages.component';
import { PrivateChatComponent } from './components/private-chat/private-chat.component'

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    HomeComponent,
    ChatComponent,
    ChatInputComponent,
    MessagesComponent,
    PrivateChatComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
