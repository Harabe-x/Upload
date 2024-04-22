<script>   
   import StatsFrame from "../DataPresenters/Stats/StatsFrame.svelte";    
   import Stat from "../DataPresenters/Stats/Stat.svelte";
   import LinearChart from "../DataPresenters/Charts/LinearChart.svelte";
   import Card from "../DataPresenters/Cards/Card.svelte";
</script>
 

<div class="flex flex-col w-full h-full ">
    <div class="flex flex-row w-full h-[50%]"> 
        <div class=" flex flex-row w-full justify-center gap-5">
            <div class="w-[25%]">
                <Card title="Revanue"> 
                    <Stat title='' value="23,300$" description="money earned this week" ></Stat>
                     </Card>
            </div>
            <div class="w-[25%]">
                <Card title="Revanue"> 
                    <Stat title='' value="23,300$" description="money earned this week" ></Stat>
                     </Card>
            </div>   <div class="w-[25%]">
                <Card title="Revanue"> 
                    <Stat title='' value="23,300$" description="money earned this week" ></Stat>
                     </Card>
            </div>
            
        </div> 

     </div>   
    
    <div class="flex flex-row w-full h-[50%] justify-center gap-10"> 
         <div class="w-[45%] h-full">
            <Card title="Votes">
                <LinearChart></LinearChart>
            </Card>
         </div>
         <div class="w-[45%] h-full">
            <Card title="Votes">
                <LinearChart></LinearChart>
            </Card>
         </div>
      </div>   
             
     

</div>



