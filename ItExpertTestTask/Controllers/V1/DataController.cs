using System.Runtime.InteropServices;
using AutoMapper;
using ItExpertTestTask.Bll;
using ItExpertTestTask.Bll.Models;
using ItExpertTestTask.Controllers.V1.Models.GetData;
using ItExpertTestTask.Controllers.V1.Models.SaveData;
using Microsoft.AspNetCore.Mvc;

namespace ItExpertTestTask.Controllers.V1;

[Route("api/[controller]")]
public class DataController : Controller
{
    private readonly IMapper _mapper;

    private readonly DataValuesService _dataValuesService;

    public DataController(DataValuesService dataValuesService,
        IMapper mapper)
    {
        _dataValuesService = dataValuesService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("save")]
    public async Task<ActionResult> SaveData([FromBody] SaveDataRequest request, CancellationToken token)
    {
        await _dataValuesService.SaveData(_mapper.Map<DataValues>(request), token);

        return Ok();
    }

    [HttpGet]
    [Route("get")]
    public async Task<ActionResult<GetDataResponse>> GetData([Optional]int[] codes, CancellationToken token)
    {
        var data = await _dataValuesService.GetData(codes, token);

        var response = new GetDataResponse(data.Select(d => _mapper.Map<Models.GetData.Data>(d)).ToArray());
        
        return Ok(response);
    }
}