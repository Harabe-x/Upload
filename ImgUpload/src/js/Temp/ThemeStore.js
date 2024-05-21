import {writable} from "svelte/store";


const theme = localStorage.getItem('theme');

const themeStore = writable(theme == null ? 'dark' : theme)

export function getThemeStore()
{
    return themeStore;
}