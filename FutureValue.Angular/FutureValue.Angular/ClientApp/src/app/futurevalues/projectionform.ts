import {ProjectionYear} from "./projectionYear"
export class projectionForm{
    id:number=0;
    presetValue:number=0;
    name:string="unnamed";
    lowerBoundInterest:number=0;
    upperBoundInterest:number=0;
    incrementalRate:number=0;
    maturityYears:number=1;
    aspUserId:any;
    dateCreated:Date=new Date();
    projections:ProjectionYear[]=[];
}