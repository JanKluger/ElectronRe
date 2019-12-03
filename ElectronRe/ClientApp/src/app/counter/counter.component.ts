import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent implements OnInit {
  constructor() {
    console.log('constructor');
  }

  ngOnInit(): void {
    console.log('OnInit');
  }

  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }
}
