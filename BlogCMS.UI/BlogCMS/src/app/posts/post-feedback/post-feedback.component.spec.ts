import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostFeedbackComponent } from './post-feedback.component';

describe('PostFeedbackComponent', () => {
  let component: PostFeedbackComponent;
  let fixture: ComponentFixture<PostFeedbackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PostFeedbackComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PostFeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
