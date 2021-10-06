import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SideComponent } from './components/side/side.component';
import { FlowerItemComponent } from './components/flower-item/flower-item.component';

@NgModule({
  declarations: [
    AppComponent,
    SideComponent,
    FlowerItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
