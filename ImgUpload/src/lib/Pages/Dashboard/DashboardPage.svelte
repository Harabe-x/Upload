<script>
    import Card from "../../DataPresenters/Cards/Card.svelte";
    import Chart from "../../DataPresenters/Charts/Chart.svelte";
    import Stat from "../../DataPresenters/Cards/Stat.svelte";
    import StatIcon from "../../DataPresenters/Cards/StatIcon.svelte";
    import { Server,ServerStack,Photo ,CurrencyDollar } from "svelte-hero-icons";
    import { getChartData } from "../../../js/MenuData/ApplicationData/ChartData";    
    import TextInput from "../../Controls/Inputs/TextInput.svelte";
    import DashboardTopMenu from "./DashboardTopMenu.svelte";
    let value;
</script>


<!-- Card section -->
<DashboardTopMenu> </DashboardTopMenu>
<div class="grid lg:grid-cols-3 mt-2 md:grid-cols-2 grid-cols-1 gap-6" >
    <Stat icon={ServerStack} title="Request Ammount" value="221,222" description="Your value increased by  220%" > </Stat>
    <Stat icon={Photo} title="Uploaded Images" value="823,174" description="Your value increased by  220%" > </Stat>
    <Stat icon={CurrencyDollar} title="Money Spent" value="23,300$" description="Your value increased by  220%"> </Stat>

</div>

<!-- Charts -->
<div class="grid lg:grid-cols-2 mt-1 md:grid-cols-2 grid-cols-1 gap-6">
        <Card title="Uploaded Images">
            <Chart chartType='line' data={getChartData()}>

            </Chart>
            
        </Card>
        <Card title="Number of requests">
            <Chart data={getChartData()} chartType="bar"></Chart>
        </Card>
</div>
    