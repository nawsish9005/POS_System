import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseItemComponent } from './purchase-item.component';

describe('PurchaseItemComponent', () => {
  let component: PurchaseItemComponent;
  let fixture: ComponentFixture<PurchaseItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PurchaseItemComponent]
    });
    fixture = TestBed.createComponent(PurchaseItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
