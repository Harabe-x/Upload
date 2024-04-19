import { readable } from 'svelte/store'
import svelteLogo  from '../assets/svelte.svg'


function getProfileData() { 
     profileData.FirstName = 'Harabe'
     profileData.LastName = 'Kowalski'
     profileData.ProfilePic = svelteLogo
     profileData.ProfileName =  'Harabe Kowalski' 
}
let profileData = { 
    ProfilePic: '', 
    ProfileName: '',
    FirstName: '',
    LastName: '' 
}

   

const store = readable(profileData,getProfileData)


export function getUserProfileInfo() {
    return store;
}