<script>
    import IconMenuItem from "../Controls/MenuItems/IconMenuItem.svelte";

    import {Icon, Bars3, Sun, Moon, Bell, ArrowRightOnRectangle, ArrowDownTray} from "svelte-hero-icons";
    import BadgeMenuItem from "../Controls/MenuItems/BadgeMenuItem.svelte";
    import UserProfileDropdown from '../Controls/Dropdowns/UserProfileDropdown.svelte'
    import {toggleNavBar } from '../../js/Temp/NavbarStateStore'
    import {getAuthStore} from "@/js/State/Auth/AuthStore.js";

    export let pageTitle;

    const authStore = getAuthStore();

    function logout()
    {
        authStore.logout()
    }
</script>


<div class="navbar sticky top-0 bg-base-200 z-10">

    <div class="flex-1">
        <label for="navbar"  on:click={toggleNavBar} class="btn btn-primary drawer-button lg:hidden">
            <Icon src={Bars3}   class="h-5 inline-block w-5"></Icon>
        </label>
        <h1 class="text-2xl font-semibold ml-2"> {pageTitle} </h1>
    </div>

    <div class="flex-none">

             <!-- TODO : Implement open notification  -->
        <button class="btn btn-ghost ml-4  btn-circle">
            <div class="indicator">
                <Icon  src={Bell} class="h-6 w-6"/>
            </div>
        </button>
        
            <UserProfileDropdown>
                <li on:click={logout}><a><Icon src={ArrowRightOnRectangle} class="w-4"/>Logout</a></li>
            </UserProfileDropdown>
    </div>
</div>

