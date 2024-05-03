<script>
    import { Icon } from "svelte-hero-icons";

    export let icon; 
    export let buttonStyle = null; 
    export let iconStyle = null; 
</script>

<button class="btn btn-ghost btn-sm normal-case {buttonStyle}">
    <Icon src={icon} class="{iconStyle}"></Icon>
    <slot/>
</button> 