using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jegymester.DataContext.Entities;
using Jegymester.Services;
//using Jegymester.Dtos;
using Microsoft.CodeAnalysis.CSharp;
using System.Threading.Tasks;

namespace Jegymester.Controllers
{
    [Route("api/TestData")]
    [ApiController]
    public class TestDataController : ControllerBase
    {
        private readonly ITestData _testData;

        public TestDataController(ITestData testData)
        {
            _testData = testData;
        }

        [HttpPost("InsertTestData")]
        public async Task<IActionResult> InsertDatabaseTestData()
        {
            await _testData.InsertDatabaseTestDataAsync();
            return Ok();
        }


        [HttpDelete("ClearDatabase")]
        public async Task<IActionResult> ClearDatabaseData()
        {
            await _testData.ClearDatabaseDataAsync();
            return Ok();
        }
    }
}
