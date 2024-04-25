<script>
    import { Icon } from "svelte-hero-icons";

  export let icon = undefined; 
  export let url;
  export let iconSize = 6;
</script>


<li class="mb-2 font-semibold text-xl">
    <a class="font-normal" href={url}>  
        {#if icon !== undefined   }
        <Icon src={icon} class="h-{iconSize} w-{iconSize}" />     
        {/if}
        <slot /> 
    </a>
</li>




