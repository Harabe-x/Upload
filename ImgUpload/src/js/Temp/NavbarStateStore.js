import { writable } from "svelte/store";

const navBarStore = writable(false);


export function getNavBarStateStore()
{
    return navBarStore;
}

