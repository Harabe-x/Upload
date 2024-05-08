<script>
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import { ArrowDownTray } from "svelte-hero-icons";
    import ApiKeyLogList from "./ApiKeyLogList.svelte";
    export let apiKeysStore; 

    let currentKey; 


</script>

<div class="flex flex-row items-center w-full">
    <select bind:value={currentKey} class="select select-info"> 
        {#each $apiKeysStore as apiKey }
            <option value={apiKey.Name}> {apiKey.Name} </option>
         {/each}
    </select>
    <div class="ml-auto">
        <IconButton iconStyle="w-6" icon={ArrowDownTray}> Download </IconButton>
    </div>
</div>

<div class="divider mt-2"></div>

<div class="">
    {#key currentKey}
        <ApiKeyLogList></ApiKeyLogList> 
    {/key}
    
</div>


