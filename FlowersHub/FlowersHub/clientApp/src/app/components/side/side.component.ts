import { Component, OnInit } from '@angular/core';
import { ColorTypeService } from 'src/app/services/color-type.service';
import { FlowerTypeService } from 'src/app/services/flower-type.service';
import { GroupTypeService } from 'src/app/services/group-type.service';

@Component({
  selector: 'app-side',
  templateUrl: './side.component.html',
  styleUrls: ['./side.component.scss']
})
export class SideComponent implements OnInit {

  colors: string[] = [];
  flowers: string[] = [];
  groups: string[] = [];

  constructor(
    private flowerTypeService: FlowerTypeService,
    private colorTypeService: ColorTypeService,
    private groupTypeService: GroupTypeService,
  ) { }

  ngOnInit(): void {
    this.flowerTypeService.get().subscribe(item => {
      this.flowers = item;
      console.log(this.flowers);
    });

    this.colorTypeService.get().subscribe(item => {
      this.colors = item;
      console.log(this.colors);
    });

    this.groupTypeService.get().subscribe(item => {
      this.groups = item;
      console.log(this.colors);
    });
  }
}
