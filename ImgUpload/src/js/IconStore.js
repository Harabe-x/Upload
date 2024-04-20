import House from '../assets/house.svg'
import VerticalDots from '../assets/vertical-dots.svg'
import VerticalDotsEmpty from '../assets/vertical-dots-empty.svg'
import HorizontallDots from '../assets/horizontal dots.svg'
import HorizontallDotsEmpty from '../assets/horizontal-dots-empty.svg'
import Bookmark from '../assets/bookmark.svg'
import CreditCard from '../assets/card.svg'
import LeftArrow from '../assets/chevron-left-svgrepo-com.svg'
import RightArrow from '../assets/chevron-right-svgrepo-com.svg'
import Cog from '../assets/cog.svg'
import Collection from '../assets/collection.svg'
import Download from '../assets/download.svg'
import File from '../assets/file.svg'
import Image from '../assets/image.svg'
import Key from '../assets/key.svg'
import Link from '../assets/link.svg'
import Menu from '../assets/menu.svg'
import Plus from '../assets/plus.svg'
import Trash from '../assets/trash.svg'
import Wallet from '../assets/wallet2.svg'
import User from '../assets/user-svgrepo-com.svg'
import Logout from '../assets/logout-svgrepo-com.svg'

import { readable } from "svelte/store";

const icons = { 
    House:House,
    VerticalDots:VerticalDots,
    VerticalDotsEmpty:VerticalDotsEmpty,
    HorizontallDots:HorizontallDots,
    HorizontallDotsEmpty:HorizontallDotsEmpty,
    Bookmark:Bookmark,
    CreditCard:CreditCard,
    LeftArrow:LeftArrow,
    RightArrow:RightArrow,
    Cog:Cog,
    Collection:Collection,
    Download:Download,
    File:File,
    Image:Image,
    Key:Key,
    Link:Link,
    Menu:Menu,
    Plus:Plus,
    Trash:Trash,
    Wallet:Wallet,
    User:User,
    Logout:Logout
}

const store = readable(icons)

export default function getIconStore()
{
    return store;
}