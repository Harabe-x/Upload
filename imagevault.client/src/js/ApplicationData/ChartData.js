import { color } from "chart.js/helpers";
import { getRandomNumber } from "../Temp/DummyDataGenerator";
function generateRandomNumberArray(length)
{
    const array = []
    for (let index = 0; index < length; index++) {
        array.push(getRandomNumber(10000,100000))
    }
    return array;
}
async function getChartDataFromApi(count) {
    const promise = fetch(`https://localhost:7110/ChartData/GetDummyChartData?count=7`, {
        method: 'GET',
        headers: {
            'Accept': 'text/json'
        }
    });
    
    return (await promise).json()
    
}
export async  function getChartData()
{
    
    const chartData  = await getChartDataFromApi();
    
    
    return { 
        labels : ['29/04/2024','30/04/2024','01/05/2024','02/05/2024','03/05/2024','04/05/2024','05/05/2024',],
        datasets : [{
            label : 'Number of requests',
            data: chartData.chartData

        }  ]
    }
}
