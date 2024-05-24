import { writable } from "svelte/store";

const theme = localStorage.getItem('theme');

const themeStore = writable(theme ? theme : 'dark');

themeStore.subscribe(value => {
    localStorage.setItem('theme', value);
});

export function getThemeStore() {
    return themeStore;
}

export function isDarkMode() {
    let currentTheme;
    themeStore.subscribe(value => currentTheme = value)();
    return currentTheme === 'dark';
}