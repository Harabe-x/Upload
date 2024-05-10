<script>
    import { XMark,Icon,LockClosed} from "svelte-hero-icons";
    import { getNavigationBarItems } from "../../js/MenuData/MenuItems";
    import NavigationBarSubMenu from "./NavigationBarSubMenu.svelte";
    import NavigationMenuItem from "../Controls/MenuItems/IconMenuItem.svelte";
    import { getNavigationStore } from "../../js/Temp/NavigationStore";
    import { toggleNavBar } from "../../js/Temp/NavbarStateStore";

    const navigationBarItems = getNavigationBarItems();

</script>   

<div class="drawer-side z-30">
    <label  class="drawer-overlay"></label> 
        <ul class="menu  pt-2 w-80 bg-base-100 min-h-full   text-base-content">
            <button on:click={toggleNavBar} class="btn btn-ghost bg-base-100  btn-circle z-50 top-0 right-0 mt-4 mr-2 absolute lg:hidden">
                    <Icon src={XMark} class="h-5 inline-block w-5"/>
                </button>
                <li class="mb-2 font-semibold text-xl"><a href="/app/welcome"> <Icon  class="mask mask-squircle w-10 text-blue-400" src={LockClosed}></Icon>  Imagevault </a> </li>
   
                {#each navigationBarItems as menuItem }
                        {#if menuItem.SubItems !== undefined}
                            <li class="mb-2 font-semibold text-xl">
                                <NavigationBarSubMenu icon={menuItem.Icon} title={menuItem.Title}>
                                    {#each menuItem.SubItems as subItem }
                                        <NavigationMenuItem name={subItem.Title} iconSize={5} component={subItem.Component} icon={subItem.Icon}> {subItem.Title} </NavigationMenuItem>
                                    {/each}
                                </NavigationBarSubMenu>     
                            </li>  
                            {:else}
                                <NavigationMenuItem name={menuItem.Title} icon={menuItem.Icon} component={menuItem.Component} class="mb-2 font-semibold text-xl"> {menuItem.Title} </NavigationMenuItem>                
                        {/if}
                {/each}
            </ul>
</div>