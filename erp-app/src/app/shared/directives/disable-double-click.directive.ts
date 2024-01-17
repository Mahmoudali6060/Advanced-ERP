import {Directive, ElementRef, EventEmitter, HostListener, Input, OnDestroy, OnInit} from '@angular/core';
import {Subject, Subscription} from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Directive({
  selector: '[appDisableDoubleClick]'
})
export class DisableDoubleClickDirective implements OnInit, OnDestroy {

  constructor(private el: ElementRef) { }

  @Input()
  debounceTime = 1000;

  private clicks = new Subject<any>();
  private subscription: Subscription;


  ngOnInit() {
    debugger
    this.subscription = this.clicks.pipe(
      debounceTime(this.debounceTime)).subscribe(
      event => {
        event.target.classList.remove('disable-button');
      });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }


  @HostListener('click', ['$event'])
  clickEvent(event:any) {
    debugger;
    event.target.classList.add('disable-button');
    this.clicks.next(event);
    return true;
  }


}

