import { Component, OnInit } from '@angular/core';
import { Flower } from './Model/flower'
import { FlowerService } from './services/flower.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  toggle = false;
  flowers: Flower[] = [];
  current: number = 0;
  countInPage: number = 24;

  constructor(private flowerService: FlowerService) {

  }

  switch() {
    this.toggle = !this.toggle;
  }

  ngOnInit(): void {
    this.flowerService.getWithArguments(this.current, this.countInPage).subscribe(items => {
      this.flowers = items;
      console.log(this.flowers);
    });

    this.current += this.countInPage;
  }

  addNewItems() {
    this.flowerService.getWithArguments(this.current, this.countInPage).subscribe(flowers => {
      for (let flower of flowers) {
        this.flowers.push(flower);
      }
      console.log(this.flowers);
    });

    this.current += this.countInPage;
  }
}
