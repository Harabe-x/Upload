<script>
    import { Icon } from "svelte-hero-icons";

     export let title;
     export let value; 
     export let description;
     export let icon;
     export let titleTextColor = 'base-content';
     export let valueTextColor = 'base-content'; 
     export let iconColor = 'base-content';

</script>

  
<div class="stats shadow">
    <div class="stat bg-base-300">
        <div class="stat-figure text-primary"> 
            <Icon src={icon}  class="w-20 h-20 text-{iconColor}" ></Icon>
         </div>
        <div class="stat-title font-semibold">
            <span class="text-{titleTextColor}"> {title} </span>
        </div>
        <div class="stat-value text-s">
            <span class="text-{valueTextColor}"> {value} </span>
        </div>
        <div class="stat-desc">{description}</div>
    </div>
</div>  