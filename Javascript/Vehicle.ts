export default class Vehicle {
    private type: string;
    constructor(type: string) { 
        this.type = type; 
    }
    getType(){
        return this.type;
    }
}