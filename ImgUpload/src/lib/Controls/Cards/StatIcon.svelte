<script>
    import { Icon } from 'svelte-hero-icons' 
    
    export let icon; 
    export let value;
    export let description; 
    export let valueColor = ''; 
    
</script>

<div class="stat place-items-start">  
    <div class="stat-figure text-priamry"> 
        <Icon class="w-20 h-20" src={icon}></Icon>
    </div>     
    <div class="stat-value text-{valueColor}"> 
        {value}
    </div>
    <div class="stat-description">
        {description}
    </div>

</div>