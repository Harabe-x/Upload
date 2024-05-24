import { writable } from "svelte/store";

const theme = localStorage.getItem('theme');

const themeStore = writable(theme ? theme : 'dark');


export function getThemeStore() {
    return themeStore;
}
