import { writable,get} from "svelte/store";

const navBarStore = writable(false);


export function getNavBarStateStore()
{
    return navBarStore;
}

export function toggleNavBar()
{
    const storeValue = get(navBarStore);
    navBarStore.set(!storeValue)
}


