import { writable } from "svelte/store";

let userData = localStorage.getItem('userData');

let theme;

if(userData)
{
    const parsedData = JSON.parse(userData);

    theme = parsedData.userData.preferedColorSchema;

}

theme = theme === null ? "dark" : theme;

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
