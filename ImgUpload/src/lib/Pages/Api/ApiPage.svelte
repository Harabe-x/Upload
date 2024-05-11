<script>
    import Card from '../../DataPresenters/Cards/Card.svelte'
    import Chart from '../../DataPresenters/Charts/Chart.svelte';
    import { getChartData } from '../../../js/ApplicationData/ChartData';
    import { ArrowPath,Icon,Key,Plus } from 'svelte-hero-icons';
    import ApiKeysList from './Components/ApiKeysList.svelte';
    import IconButton from '../../Controls/Buttons/IconButton.svelte';
    import DoughnutChart from "../../DataPresenters/Charts/DoughnutChart.svelte";
    import { getApiKeys } from '../../../js/Temp/ApiKeysData';
    import ApiKeysLogs from './Components/ApiKeysLogs.svelte';
    import AddApiKeyModal from './Components/AddApiKeyModal.svelte';
  import PageTopMenu from '../../Controls/Shared/PageTopMenu.svelte';

    const apiKeys = getApiKeys();

    let isModalVisable = false;   


    function toggleModal()
    {
        isModalVisable = !isModalVisable
    }

</script>

<PageTopMenu>
    <IconButton iconStyle="w-4 mr-1" icon={ArrowPath}>
        Refresh
    </IconButton>
</PageTopMenu>

<div class="grid mt-1 grid-cols-1">
     
    <Card title="Api Keys">
        <!-- Title Button  -->
            <div class="flex flex-row ml-auto mr-2" slot="titleControl"> 
                <IconButton icon={Plus} on:click={() => { toggleModal() }} iconStyle="w-4 h-4"> Add </IconButton> 
            </div>
        <ApiKeysList></ApiKeysList> 
    </Card>
</div> 
 
<div class="grid lg:grid-cols-2 mt-2 grid-cols-1 gap-10 mb-10">
    {#key $apiKeys}
    <Card title="Api Keys Usage" > 
        <DoughnutChart labels={$apiKeys.map((item) => item.Name )} values={$apiKeys.map((item) => item.Storage)}></DoughnutChart>
    </Card>
    {/key}
<div class="grid gird-cols-1 grid-flow-row">
    <Card title="Api Key Logs">
        <ApiKeysLogs apiKeysStore={apiKeys} > </ApiKeysLogs>
    </Card>
</div>

</div>    


<AddApiKeyModal isVisable={isModalVisable} on:modalClosed={() => {toggleModal() }}> </AddApiKeyModal>
