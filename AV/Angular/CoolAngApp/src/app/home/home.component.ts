import { Component, OnInit } from '@angular/core';

@Component({
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  today:Date = new Date();
  date:string = +(this.today.getMonth()+1)+'-'+this.today.getDate()+'-'+this.today.getFullYear();
  
  constructor() { }

  ngOnInit(): void {
  }

}
