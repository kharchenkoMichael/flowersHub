import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  toggle = false;

  switch(event: any) {
    this.toggle = !this.toggle;
    event.stopPropagation();

    console.log("switch")
    console.log(event)
  }

}
