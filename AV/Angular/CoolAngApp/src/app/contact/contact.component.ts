import { FormGroup, FormControl } from '@angular/forms';
import { Component, Pipe, PipeTransform, OnInit } from '@angular/core';
import { PhoneType } from './PhoneType'

@Component({
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})

export class ContactComponent implements OnInit 
{
  contactForm:FormGroup;
  phoneEnum = PhoneType;

  // ctor.
  constructor() { }

  // LifeCycle hook.
  ngOnInit(): void 
  {
    // Form - Root FormGroup - Form Control[s]
    this.contactForm = new FormGroup({
      txtFirstName: new FormControl(),
      txtLastName: new FormControl(),
      txtEmail: new FormControl(),
      txtPhone: new FormControl(),
      txtAddress1: new FormControl(),
      txtCity: new FormControl(),
      txtState: new FormControl(),
      txtZipCode: new FormControl()
    });
  }
}

// Pipe.
@Pipe({
  name: 'enumPhone'
})
export class EnumToArrayPipe implements PipeTransform 
{
  transform(data: Object) 
  {
    const keys = Object.keys(data);
    return keys.slice(keys.length / 2);
  }
}