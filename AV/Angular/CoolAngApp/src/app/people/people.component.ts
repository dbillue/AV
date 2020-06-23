import { FormGroup, FormControl } from '@angular/forms';
import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { GenderType } from './GenderType'

@Component({
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.scss']
})

export class PeopleComponent implements OnInit 
{
  peopleForm:FormGroup;
  genderType = GenderType;
  // [Deprecated]
  ageArray = Array(100).fill(1).map((x,i)=>i); // array for person.age form control

  // ctor.
  constructor() { }

    // LifeCycle hook.
  ngOnInit(): void 
  { 
    this.peopleForm = new FormGroup({
      txtFirstName: new FormControl(),
      txtMiddleName: new FormControl(),
      txtLastName: new FormControl(),
      cboGender: new FormControl(),
      txtAge: new FormControl(),
      txtDOB: new FormControl(),
      txtCity: new FormControl(),
      cboState: new FormControl(),
      txtZipCode: new FormControl()
    });
  }
}

// Pipe
@Pipe({
  name: 'enumData'
})

export class EnumToGenderType implements PipeTransform 
{
  transform(data: Object) 
  {
    const keys = Object.keys(data);
    return keys.slice(keys.length / 2);
  }
}