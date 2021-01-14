import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostavkeComponent } from './postavke.component';

describe('PostavkeComponent', () => {
  let component: PostavkeComponent;
  let fixture: ComponentFixture<PostavkeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PostavkeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PostavkeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
