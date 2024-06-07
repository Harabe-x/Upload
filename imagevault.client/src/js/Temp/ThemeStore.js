import { writable } from "svelte/store";

let theme = localStorage.getItem('theme');

theme = theme === null ? 'dark' : theme

const themeStore = writable(theme) ;


function setDataTheme(value)
{
    document.documentElement.setAttribute('data-theme', value);
}

setDataTheme(theme);


export function getThemeStore() {
    return {
        subscribe : themeStore.subscribe,
        set: (value) =>  {
            themeStore.set(value)
            setDataTheme(value)
        },
        update : (fn) => {
            themeStore.update(value => {
                const newValue = fn(value);
                setDataTheme(newValue);
                return newValue;
            })
        }
    };
}
