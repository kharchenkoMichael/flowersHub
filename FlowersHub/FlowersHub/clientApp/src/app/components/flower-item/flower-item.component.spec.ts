import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlowerItemComponent } from './flower-item.component';

describe('FlowerItemComponent', () => {
  let component: FlowerItemComponent;
  let fixture: ComponentFixture<FlowerItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlowerItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlowerItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
