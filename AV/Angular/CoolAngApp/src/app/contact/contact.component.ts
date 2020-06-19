import { Component, Pipe, PipeTransform, OnInit } from '@angular/core';
import { PhoneType } from './PhoneType'

@Component({
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  phoneEnum = PhoneType;

  // ctor.
  constructor() { }

  // LifeCycle hook.
  ngOnInit(): void { }

}

@Pipe({
  name: 'enumPhone'
})
export class EnumToArrayPipe implements PipeTransform {
  transform(data: Object) {
    const keys = Object.keys(data);
    return keys.slice(keys.length / 2);
  }
}