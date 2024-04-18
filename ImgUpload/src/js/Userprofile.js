import { readable } from 'svelte/store'
import svelteLogo  from '../assets/svelte.svg'

const store = readable({ 
    profilePic: svelteLogo, 
    profileName: 'Harabe Kowalski' 
})

export function getUserProfileInfo() {
    return store;
}