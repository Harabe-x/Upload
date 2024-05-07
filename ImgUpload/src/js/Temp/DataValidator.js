import { parse } from "svelte/compiler"



export function validateName(str)
{
    return typeof str === 'string' && str.trim().length >  0    
}
export function validateStorage(number)
{
    var parsedNumber = parseInt(number)
    console.log(parsedNumber)
    return !isNaN(parsedNumber) && parsedNumber >= 1 && parsedNumber <= 10000
}