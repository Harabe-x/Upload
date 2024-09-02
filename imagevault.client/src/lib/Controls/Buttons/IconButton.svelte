<script>
    import { createEventDispatcher } from "svelte";
    import { Icon } from "svelte-hero-icons";
    const dispatcher = createEventDispatcher();

    export let icon; 
    export let buttonStyle = null; 
    export let iconStyle = null; 
    export let flipIcons = false;
    export let disabled = false; 
    function invokeEvent(event)
    {
       dispatcher(event.type)
    }

</script>

<button class="btn btn-ghost btn-sm normal-case {buttonStyle}" {disabled} on:click={invokeEvent}>
    {#if flipIcons === false}
    <Icon src={icon} class="{iconStyle}"></Icon>
    <slot/>
    {:else}
    <slot/>
    <Icon src={icon} class="{iconStyle}"></Icon>
    {/if}
   
    
    
</button> 