<script>
    import { Cog,XMark,Icon,Home,Key,Photo} from "svelte-hero-icons";
    import Logo from '../../assets/orginal waifu removed bg.png'
    import NavigationBarSubMenu from "./NavigationBarSubMenu.svelte";
    import NavigationMenuItem from "./NavigationMenuItem.svelte";
    const items = ['Test','Data Retrival','Twoja Stara'];
</script>   

<div class="drawer-side z-30">
    <label class="drawer-overlay"></label> 
        <ul class="menu  pt-2 w-80 bg-base-300 min-h-full   text-base-content">
            <button class="btn btn-ghost bg-base-300  btn-circle z-50 top-0 right-0 mt-4 mr-2 absolute lg:hidden">
                    <Icon src={XMark} class="h-5 inline-block w-5"/>
                </button>
                <li class="mb-2 font-semibold text-xl"><a href="/app/welcome"> <img class="mask mask-squircle w-10" src="{Logo}" alt="DashWind Logo">ImageValut</a> </li>
                <NavigationMenuItem url="#" icon={Home}> Dashboard </NavigationMenuItem>
                <NavigationMenuItem url="#" icon={Key}> API </NavigationMenuItem>
                <NavigationMenuItem url="#" icon={Photo}> Images </NavigationMenuItem>
                <li class="mb-2 font-semibold text-xl"> <NavigationBarSubMenu icon={Cog} title="Settings" {items} > </NavigationBarSubMenu> </li>

            </ul>

</div>