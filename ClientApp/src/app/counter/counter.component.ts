import { Component } from '@angular/core';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html',
})
export class CounterComponent {
  public currentCount = 10;

  public incrementCounter() {
    this.currentCount++;
  }
    public func() {
        console.log("This function is working");
        alert("this is a function");
    }
}
