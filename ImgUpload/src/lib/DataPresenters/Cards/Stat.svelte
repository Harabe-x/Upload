<script>
    import { Icon } from "svelte-hero-icons";

     export let title;
     export let value; 
     export let description;
     export let icon;
     export let titleTextColor = '';
     export let valueTextColor = ''; 
     export let iconColor = '';
</script>

  
<div class="stats shadow">
    <div class="stat">
        <div class="stat-figure dark:text-slate-300"> <Icon src={icon}  class="w-20 h-20 text-{iconColor}" ></Icon> </div>
        <div class="stat-title dark:text-slate-300 text-{titleTextColor}">{title}</div>
        <div class="stat-value dark:text-slate-300 text-{valueTextColor}">{value}</div>
        <div class="stat-desc">{description}</div>
    </div>
</div>