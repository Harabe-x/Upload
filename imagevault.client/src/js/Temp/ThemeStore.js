import { writable } from "svelte/store";

const theme = localStorage.getItem('theme');


const themeStore = writable(theme === null ? 'dark' : theme) ;


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
            })
        }
    };
}
