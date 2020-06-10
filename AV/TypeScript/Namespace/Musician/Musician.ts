/*******************
 Author:        Duane Billue
 Date:          2020-06-10
 Description:   Typescript namespace OOP artist example
 *******************/

namespace Artist{
    export class Musician  {
        _stageName:string
        _bandName:string
        position:string

        constructor(stageName:string, bandName:string) {
            this._stageName = stageName;
            this._bandName = bandName;
        }

        showStageName():void {
            console.log(`Instrument type: ${this._stageName}`);
        }          

        showBandName():void {
            console.log(`Instrument name: ${this._bandName}`);
        }        
        
        showInstrument(instrumentName):void {
            console.log(`Instrument name: ${instrumentName}`);
        }
    }
}
