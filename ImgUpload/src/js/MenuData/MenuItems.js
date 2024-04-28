import { Title } from "chart.js";
import { Key,Home,Photo,Cog,Wallet,User,ArrowUpCircle} from "svelte-hero-icons";

const navigationMenuItems = [ 
    { Title:'Dashboard',Icon:Home  },
    { Title:'API',Icon:Key  }, 
    { Title:'Images',Icon:Photo  },
    { Title:'Settings',Icon:Cog, SubItems: [
        { Title:'Profile',Icon:User},
        { Title:'Billing',Icon:Wallet},
    ]  },   
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