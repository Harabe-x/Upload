<script>
    import Card from "../../Controls/Cards/Card.svelte";
    import Chart from "../../Controls/Charts/Chart.svelte";
    import Stat from "../../Controls/Cards/Stat.svelte";
    import { Icon,ServerStack,Photo, Banknotes,CloudArrowUp,ArrowPath,Envelope,EllipsisVertical,ArrowDownTray} from "svelte-hero-icons";
    import { getChartData } from "../../../js/ApplicationData/ChartData"
    import IconButton from "../../Controls/Buttons/IconButton.svelte";
    import PageTopMenu from "../../Controls/Shared/PageTopMenu.svelte";
    import IconDropdown from "../../Controls/Dropdowns/IconDropdown.svelte";
    import SelectInput from "../../Controls/Inputs/SelectInput.svelte";
</script>   


<!-- Card section -->
<PageTopMenu>
    <div slot="leftSide">
        <SelectInput title="Selected key:"></SelectInput>
    </div>
    <div slot="rightSide">
        <IconButton iconStyle="w-4 mr-1" icon={ArrowPath}>
            Refresh
        </IconButton>
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
    