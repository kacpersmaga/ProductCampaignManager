using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCampaignManager.Application.Commands;
using ProductCampaignManager.Application.DTOs;
using ProductCampaignManager.Application.Queries;

namespace ProductCampaignManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampaignsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CampaignsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetByUser([FromQuery] Guid userId)
    {
        if (userId == Guid.Empty)
            return BadRequest("Valid UserId is required");

        var result = await _mediator.Send(new GetCampaignListQuery(userId));
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetCampaignByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CampaignDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _mediator.Send(new CreateCampaignCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CampaignDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _mediator.Send(new UpdateCampaignCommand(id, dto));
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteCampaignCommand(id));
        return NoContent();
    }
}