<script>
    import Card from "../../DataPresenters/Cards/Card.svelte";
    import Chart from "../../DataPresenters/Charts/Chart.svelte";
    import Stat from "../../DataPresenters/Cards/Stat.svelte";
    import { Icon,ServerStack,Photo, Banknotes,CloudArrowUp,ArrowPath,Envelope,EllipsisVertical,ArrowDownTray} from "svelte-hero-icons";
    import { getChartData } from "../../../js/ApplicationData/ChartData"
    import IconButton from "../../Controls/Buttons/IconButton.svelte";
    import PageTopMenu from "../../Controls/Shared/PageTopMenu.svelte";
</script>   


<!-- Card section -->
<PageTopMenu>
    <IconButton icon={ArrowPath} iconStyle="w-4 mr-2"> Refresh data</IconButton>
    <div class="dropdown dropdown-bottom dropdown-end  ml-2">
        <label tabIndex={0} class="btn btn-ghost btn-sm normal-case btn-square "><Icon src={EllipsisVertical} class="w-5"/></label>
        <ul tabIndex={0} class="dropdown-content menu menu-compact  p-2 shadow bg-base-100 rounded-box w-52">
            <li><a><Icon src={Envelope} class="w-4"/>Email Digests</a></li>
            <li><a><Icon src={ArrowDownTray} class="w-4"/>Download</a></li>
        </ul>
    </div>
</PageTopMenu>

<div class="grid lg:grid-cols-4 mt-2 md:grid-cols-2 grid-cols-1 gap-6" >
    <Stat titleTextColor="base-content" 
     icon={ServerStack} 
       title="Storage used"
        value="21.4 GB"
         description="Your value increased by  220%" >
         </Stat>
    <Stat icon={Photo} 
    title="Uploaded Images"
     value="7 742"
      description="Your value increased by  220%" > 
    </Stat>
    <Stat icon={CloudArrowUp} 
      title="API requests"
      value="13,436" 
      description="Api calls during selected time period">
     </Stat>
     <Stat icon={Banknotes} 
      title="Costs"
      value="2 632 $" 
      description="Costs for using the API during this time period">
     </Stat>

</div>

<!-- Charts -->
<div class="grid lg:grid-cols-2 mt-1 md:grid-cols-2 grid-cols-1 gap-6">
        <Card title="Uploaded Images">
            <Chart chartType='bar' data={getChartData()}>

            </Chart>
            
        </Card>
        <Card title="Number of requests">
            <Chart data={getChartData()} chartType="line"></Chart>
        </Card>
</div>
    