<script>
 import { Icon } from "svelte-hero-icons";
 import { getNavigationStore } from "../../../js/Temp/NavigationStore";
 import { toggleNavBar } from "../../../js/Temp/NavbarStateStore";
 import PageHeaderNameStore from "../../../js/Temp/PageHeaderNameStore";
 const navigationStore = getNavigationStore();
 const pageHeaderStore = PageHeaderNameStore();
    
  export let name = null;
  export let icon = null; 
  export let component;
  export let iconSize = 6;
  
  
  function navigate()
  {
    $navigationStore = component;
    $pageHeaderStore = name; 
    toggleNavBar()
  }

</script>


<li {... $$restProps} >
    <!-- svelte-ignore a11y-missing-attribute -->
    <a class="font-normal" on:click={navigate} >
        {#if icon !== null}
        <Icon src={icon} class="h-{iconSize} w-{iconSize}" />
        {/if}
        <slot />
    </a>
</li>



