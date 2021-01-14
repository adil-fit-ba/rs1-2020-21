import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpravljanjeStudentiComponent } from './upravljanje-studenti.component';

describe('UpravljanjeStudentiComponent', () => {
  let component: UpravljanjeStudentiComponent;
  let fixture: ComponentFixture<UpravljanjeStudentiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpravljanjeStudentiComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpravljanjeStudentiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
