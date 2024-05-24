import { Title } from "chart.js";
import {RocketLaunch, InformationCircle, Document,ComputerDesktop, Key,Home,Photo,Cog,Wallet,User,ArrowUpCircle} from "svelte-hero-icons";
import DashboardPage from '../../lib/Pages/Dashboard/DashboardPage.svelte'
import ApiPage from '../../lib/Pages/Api/ApiPage.svelte'
import ImagePage from '../../lib/Pages/Images/ImagesPage.svelte'
import ProfilePage from '../../lib/Pages/Settings/Profile/ProfilePage.svelte'
import BillingPage from '../../lib/Pages/Settings/Billing/BillingPage.svelte'

const navigationMenuItems = [ 
    { Title:'Dashboard',Icon:Home,Component:DashboardPage  },
    { Title:'API',Icon:Key,Component:ApiPage  }, 
    { Title:'Images',Icon:Photo,Component:ImagePage  },
    { Title:'Documentation',Icon:Document, SubItems: [
            { Title:'Getting started',Icon:RocketLaunch,Component:BillingPage},
            { Title:'Api key management\n',Icon:Key,Component:ProfilePage},
            { Title:'Endpoints',Icon:ComputerDesktop,Component:ProfilePage},
        ]  },
    { Title:'Settings',Icon:Cog, SubItems: [
        { Title:'Profile & Preferences',Icon:User,Component:ProfilePage},
        { Title:'Billing',Icon:Wallet,Component:BillingPage},
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