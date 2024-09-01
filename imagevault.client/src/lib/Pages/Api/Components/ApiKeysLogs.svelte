<script>
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import { ArrowDownTray } from "svelte-hero-icons";
    import ApiKeyLogList from "./ApiKeyLogList.svelte";
    import {onMount} from "svelte";
    import {getApiKeys} from "../../../../js/Temp/ApiKeysData.js";
    import SelectInput from "../../../Controls/Inputs/SelectInput.svelte";
    import ApiKeySelector from "@/lib/Controls/Shared/ApiKeySelector.svelte";

    const apiKeysStore = getApiKeys();

    let currentKey;

    onMount(() => {
         apiKeysStore.fetchKeys();

         return () =>  {
             apiKeysStore
         }
    })

    function updateSelectedKey(event)
    {
        apiKeysStore.selectKey(event.detail.target.value)
    }


</script>

<div class="flex flex-row items-center w-full">
    <ApiKeySelector>
        
    </ApiKeySelector>

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


