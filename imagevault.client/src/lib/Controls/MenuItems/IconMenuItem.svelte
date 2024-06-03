<script>
 import { Icon } from "svelte-hero-icons";
 import { getNavigationStore } from "../../../js/Temp/NavigationStore";
 import { toggleNavBar } from "../../../js/Temp/NavbarStateStore";
 import PageHeaderNameStore from "../../../js/Temp/PageHeaderNameStore";
 import { navigate } from "svelte-routing";


 const navigationStore = getNavigationStore();
 const pageHeaderStore = PageHeaderNameStore();
    
  export let name = null;
  export let icon = null; 
  export let url;
  export let iconSize = 6;
  
  
  function navigateToUrl()
  {
    navigate(url);
    $pageHeaderStore = name; 
    toggleNavBar()
  }

</script>


<li {... $$restProps} >
    <!-- svelte-ignore a11y-missing-attribute -->
    <a class="font-normal" on:click={navigateToUrl} >
        {#if icon !== null}
        <Icon src={icon} class="h-{iconSize} w-{iconSize}" />
        {/if}
        <slot />
    </a>
</li>



