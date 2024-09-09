import {RocketLaunch, InformationCircle, Document,ComputerDesktop, Key,Home,Photo,Cog,Wallet,User,ArrowUpCircle} from "svelte-hero-icons";

const navigationMenuItems = [ 
    { Title:'Dashboard',Icon:Home,Url:'/app/'  },
    { Title:'API',Icon:Key,Url:'/app/api'  },
    { Title:'Images',Icon:Photo,Url:'/app/images'  },
    { Title:'Documentation',Icon:Document, SubItems: [
            { Title:'Getting started',Icon:RocketLaunch,Url:'/app/settings/billing'},
            { Title:'Api key management\n',Icon:Key,Url:'/app/settings/billing'},
            { Title:'Endpoints',Icon:ComputerDesktop,Url:'/app/settings/billing'},
        ]  },
    { Title:'Settings',Icon:Cog, SubItems: [
        { Title:'Profile & Preferences',Icon:User,Url:'/app/settings/profile'},
        { Title:'Billing',Icon:Wallet,Url:'/app/settings/billing'},
    ]  }

]   

export function getNavigationBarItems()
{
  return navigationMenuItems;    
}