using Microsoft.AspNetCore.Mvc;

namespace ImageVault.Server.Controllers;


[ApiController]
[Route("[Controller]")]
public class ChartDataController
{   
    [HttpGet(Name = "GetDummyChartData")]
    public ChartDataModel Get(int count)
    {
        return new ChartDataModel()
        {
            ChartData =   Enumerable.Range(1, count).Select(_ =>
            {
                return Random.Shared.Next(10, 200);
            }) 
        };
    }       
}