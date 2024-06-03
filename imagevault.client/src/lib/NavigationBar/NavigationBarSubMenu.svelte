<script>
     import { Icon, ChevronDown , ChevronUp } from "svelte-hero-icons";


    let isSubListExtended = false; 
    export let icon;
    export let title;

    function changeVisability()
    {
        isSubListExtended = !isSubListExtended  
    }        
</script>



<div class="flex flex-col w-full"> 
    <!-- svelte-ignore a11y-click-events-have-key-events -->
    <!-- svelte-ignore a11y-no-static-element-interactions -->
    <div class='w-full block' on:click={changeVisability} >
        <Icon src={icon} class="h-6 w-6 inline"></Icon> 
        <span class="font-normal mb-2"> {title} </span>
        <Icon src={ChevronUp} class="w-5 h-5 mt-1 inline float-right  delay-400 duration-500 transition-all {isSubListExtended ? '' : 'rotate-180'} "/>
    </div>
    
    <div class="w-full { isSubListExtended ? '' : 'hidden' }">
        <ul class="menu menu-compact">
            <slot> </slot>   
        </ul>
    </div>  

</div>