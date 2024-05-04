<script>
    import { createEventDispatcher } from "svelte";
    import { Icon } from "svelte-hero-icons";
    const dispatcher = createEventDispatcher();

    export let icon; 
    export let buttonStyle = null; 
    export let iconStyle = null; 

    function invokeEvent(event)
    {
       dispatcher(event.type)
    }

</script>

<button class="btn btn-ghost btn-sm normal-case {buttonStyle}" on:click={invokeEvent}>
    <Icon src={icon} class="{iconStyle}"></Icon>
    <slot/>
</button> 