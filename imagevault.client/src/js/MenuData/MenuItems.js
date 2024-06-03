import { Title } from "chart.js";
import {RocketLaunch, InformationCircle, Document,ComputerDesktop, Key,Home,Photo,Cog,Wallet,User,ArrowUpCircle} from "svelte-hero-icons";
import DashboardPage from '../../lib/Pages/Dashboard/DashboardPage.svelte'
import ApiPage from '../../lib/Pages/Api/ApiPage.svelte'
import ImagePage from '../../lib/Pages/Images/ImagesPage.svelte'
import ProfilePage from '../../lib/Pages/Settings/Profile/ProfilePage.svelte'
import BillingPage from '../../lib/Pages/Settings/Billing/BillingPage.svelte'

const navigationMenuItems = [ 
    { Title:'Dashboard',Icon:Home,Url:'/'  },
    { Title:'API',Icon:Key,Url:'/api'  }, 
    { Title:'Images',Icon:Photo,Url:'/images'  },
    { Title:'Documentation',Icon:Document, SubItems: [
            { Title:'Getting started',Icon:RocketLaunch,Url:'/settings/billing'},
            { Title:'Api key management\n',Icon:Key,Url:'/settings/profile'},
            { Title:'Endpoints',Icon:ComputerDesktop,Url:'/settings/profile'},
        ]  },
    { Title:'Settings',Icon:Cog, SubItems: [
        { Title:'Profile & Preferences',Icon:User,Url:'/settings/profile'},
        { Title:'Billing',Icon:Wallet,Url:'/settings/billing'},
    ]  }

]   

const profileMenuItems = [ 
    { Type:'Normal' , Title:'Profile item'},
    { Type:'Icon' , Title:'Icon',Icon:ArrowUpCircle}
]


export function getNavigationBarItems()
{
  return navigationMenuItems;    
}
export function getProfileNenuItems()
{
    return profileMenuItems;
}