import { Input, Component, OnInit } from '@angular/core';
import { Flower } from 'src/app/Model/flower';

@Component({
  selector: 'app-flower-item',
  templateUrl: './flower-item.component.html',
  styleUrls: ['./flower-item.component.scss']
})
export class FlowerItemComponent implements OnInit {

  @Input()
  flower: Flower = new Flower(null);

  constructor() { }

  ngOnInit(): void {
  }

  cardClick() {
    if (this.flower.url != null)
      window.open(this.flower.url, "_blank");
  }
}
