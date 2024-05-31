<script>
    import Card from '../../Controls/Cards/Card.svelte'
    import { ArrowPath,Icon,Key,Plus } from 'svelte-hero-icons';
    import ApiKeysList from './Components/ApiKeysList.svelte';
    import IconButton from '../../Controls/Buttons/IconButton.svelte';
    import DoughnutChart from "../../Controls/Charts/DoughnutChart.svelte";
    import { getApiKeys } from '../../../js/Temp/ApiKeysData';
    import ApiKeysLogs from './Components/ApiKeysLogs.svelte';
    import AddApiKeyModal from './Components/AddApiKeyModal.svelte';
    import ModalWindow from "../../Controls/Shared/ModalWindow.svelte";
    import PageTopMenu from '../../Controls/Shared/PageTopMenu.svelte';
    import SelectInput from "../../Controls/Inputs/SelectInput.svelte";

    const apiKeys = getApiKeys();

    let addImageModalToggleFunction;

</script>

<PageTopMenu>
    <div slot="rightSide">
        <IconButton iconStyle="w-4 mr-1" icon={ArrowPath}>
            Refresh
        </IconButton>
    </div>
</PageTopMenu>

<div class="grid mt-1 grid-cols-1">
     
    <Card title="Api Keys">
        <!-- Title Button  -->
            <div class="flex flex-row ml-auto mr-2" slot="titleControl"> 
                <IconButton icon={Plus} on:click={() => {addImageModalToggleFunction(); }}  iconStyle="w-4 h-4"> Add </IconButton>
            </div>
        <ApiKeysList></ApiKeysList> 
    </Card>
</div> 
 
<div class="grid lg:grid-cols-2 mt-2 grid-cols-1 gap-10 mb-10">
    {#key $apiKeys}
    <Card title="Api Keys Usage" > 
        <DoughnutChart labels={$apiKeys.apiKeys.map((item) => item.Name )} values={$apiKeys.apiKeys.map((item) => item.Storage)}></DoughnutChart>
    </Card>
    {/key}
<div class="grid gird-cols-1 grid-flow-row">
    <Card title="Api Key Logs">
        <ApiKeysLogs apiKeysStore={apiKeys} > </ApiKeysLogs>
    </Card>
</div>

</div>    

<ModalWindow type="AddApiKeyModal" bind:toggleModal={addImageModalToggleFunction}></ModalWindow>