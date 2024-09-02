<script>
import SelectInput from "@/lib/Controls/Inputs/SelectInput.svelte";
import {getApiKeyStore} from "@/js/State/ApiKey/ApiKeyStore.js";
import {onMount} from "svelte";
import {get} from "svelte/store";

const apiKeyStore = getApiKeyStore(); 


onMount( async () => { 
    await fetchKeys();
})


async function fetchKeys()
{
    var store = get(apiKeyStore)
    if (store.apiKeys.length === 0)
    { 
        await apiKeyStore.fetchKeys(); 
    }
}

function selectKey(event)
{
    alert(event.detail.target.value)
    apiKeyStore.selectKey(event.detail.target.value);
    alert(JSON.stringify(get(apiKeyStore).selectedKey))
}

</script>

<SelectInput bind:value={$apiKeyStore.selectedKey.key} on:change={selectKey} title="Selected key:">
    {#each $apiKeyStore.apiKeys as key (key.key)}
        <option value={key.key}> {key.keyName} </option>
    {/each}
</SelectInput>