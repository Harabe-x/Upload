<script>
     import { Icon, ChevronDown , ChevronUp } from "svelte-hero-icons";


    let visable = false; 
    export let icon;
    export let title;
    export let items = []

    function changeVisability()
    {
        visable = !visable  
    }        
</script>



<div class="flex flex-col w-full"> 
    <!-- svelte-ignore a11y-click-events-have-key-events -->
    <!-- svelte-ignore a11y-no-static-element-interactions -->
    <div class='w-full block' on:click={changeVisability} >
        <Icon src={icon} class="h-6 w-6 inline"></Icon> 
        <span class="font-normal mb-2"> Settings </span>
        <Icon src={ChevronUp} class="w-5 h-5 mt-1 inline float-right  delay-400 duration-500 transition-all"/>
    </div>
    
    <div class="w-full { visable ? '' : 'hidden' }">
        <ul class="menu menu-compact">
            {#each items as item}
            <li class="mb-2 font-semibold text-m"> 
                <span>
                    <Icon src={icon} class="w-5 h-5"></Icon>  {item}
                </span>
             </li>
            {/each}     
        </ul>
    </div>  

</div>