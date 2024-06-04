import {RocketLaunch, InformationCircle, Document,ComputerDesktop, Key,Home,Photo,Cog,Wallet,User,ArrowUpCircle} from "svelte-hero-icons";

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

export function getNavigationBarItems()
{
  return navigationMenuItems;    
}