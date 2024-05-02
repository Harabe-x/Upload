import { Chance } from "chance";

const chance = new Chance;

export function getRandomNumber(min,max)
{
    let number = chance.integer({min:min,max:max})
    console.log(number);   
    return number;
}
export function getRandomGuid()
{
    return chance.guid()
}

