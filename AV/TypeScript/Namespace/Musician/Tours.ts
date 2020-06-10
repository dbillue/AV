/*******************
 Author:        Duane Billue
 Date:          2020-06-10
 Description:   Typescript namespace OOP artist example
 *******************/

 namespace Artist {
     export class Tours {
        date:Date
        venue:string
        city:string
        showStartTime:Date
        showEndTime:Date
        _bandName:string

        constructor(bandname:string) {
            this._bandName = bandname;
        }
     }
 }
