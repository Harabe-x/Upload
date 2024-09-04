<script>
    import {DocumentText, Icon} from "svelte-hero-icons";

    export let title; 
    export let value; 
    export let description;
    export let icon;
</script>

<div class="stat">
    <div class="stat-figure text-secondary">
        <Icon src={icon}  class="inline-block h-8 w-8 stroke-current" >  </Icon>
    </div>
    <div class="stat-title">{title}</div>
    <div class="stat-value text-sm">{value}</div>
    <div class="stat-desc">{description}</div>
</div>

                       