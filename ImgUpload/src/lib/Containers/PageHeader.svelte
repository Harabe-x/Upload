<script>
    export let pageTitle;     
    import { Icon,Bars3,Sun,Moon,Bell} from "svelte-hero-icons";
    import BadgeMenuItem from "../Controls/MenuItems/BadgeMenuItem.svelte";
    import UserProfileDropdown from '../Controls/Dropdowns/UserProfileDropdown.svelte'
    import MrFrost from '../../assets/117171438_10157804750272880_5668855721113743083_n.jpg'
</script>

    
<div class="navbar sticky top-0 bg-base-100 z-10">
    
    <div class="flex-1">
        <label for="" class="btn btn-primary drawer-button lg:hidden">
            <Icon src={Bars3} class="h-5 inline-block w-5"></Icon>
        </label>
        <h1 class="text-2xl font-semibold ml-2"> {pageTitle} </h1>
    </div>  

    <div class="flex-none">
        <label class="swap">
            <input type="checkbox" />
            <Icon src={Moon} class="fill-current w-6 h-6 swap-on"></Icon>
            <Icon src={Sun} class="fill-current w-6 h-6 swap-off"></Icon>
        </label>
        
             <!-- TODO : Implement open notification  -->
        <button class="btn btn-ghost ml-4  btn-circle">
            <div class="indicator">
                <Icon  src={Bell} class="h-6 w-6"/>
            </div>
        </button>
        
            <UserProfileDropdown image={MrFrost}>
                <BadgeMenuItem badgeText="New" badgeColor="primary" > Item 1 </BadgeMenuItem>
                <BadgeMenuItem badgeText="New" badgeColor="secondary "  > Item 1 </BadgeMenuItem>

            </UserProfileDropdown>
        
        
   
   
    </div>
</div>

