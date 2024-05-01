import { writable } from "svelte/store";


const store = writable("Dashboard");

export default function()
{
    return store
}


