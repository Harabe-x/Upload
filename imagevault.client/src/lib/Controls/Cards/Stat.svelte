<script>
    import { Icon } from "svelte-hero-icons";
    import {getThemeStore  } from "../../../js/State/UserInterface/ThemeStore.js";


     export let title;
     export let value; 
     export let description;
     export let icon;
     export let titleTextColor = 'base-content';
     export let valueTextColor = 'base-content'; 
     export let iconColor = 'base-content';


     const themeStore = getThemeStore();
</script>


<div class="stats shadow">
    <div class="stat bg-base-200">
        <div class="stat-figure">
            <Icon src={icon}  class="w-12 h-12" ></Icon>
         </div>
        <div class="stat-title font-semibold">
            <span class="{$themeStore === 'dark' ? 'text-gray-300' : '' }  "> {title} </span>
        </div>
        <div class="stat-value  text-s">
            <span> {value} </span>
        </div>
        <div class="stat-desc">{description}</div>
    </div>
</div>  