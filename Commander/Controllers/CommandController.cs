using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CommandController : ControllerBase
{
    private readonly ICommanderRepo _repository;
    private readonly IMapper _mapper;

    public CommandController(ICommanderRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // Get / api / command
    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()             
    {
        var commandItems = _repository.GetAllCommands();
        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
    }

    // Get / api / command / {id} 
    [HttpGet("{id}", Name = "GetCommandById")]
    public ActionResult<CommandReadDto> GetCommandById(int id)
    {
        var commandItem = _repository.GetCommandById(id);
        if (commandItem != null)
        {
            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }
        return NotFound();
    }
    
    // Post / api / command
    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
    {
        var commandModel = _mapper.Map<Command>(commandCreateDto);
        _repository.CreateCommand(commandModel);
        _repository.SaveChanges();

        var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

        return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);
    }
    
    // Put / api / command / {id}
    [HttpPut("{id}")]
    public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
    {
        var commandModelFromRepo = _repository.GetCommandById(id);
        if (commandModelFromRepo == null)
        {
            return NotFound();
        }

        _mapper.Map(commandUpdateDto, commandModelFromRepo);
        
        _repository.UpdateCommand(commandModelFromRepo);

        _repository.SaveChanges();

        return NoContent();
    }
    
    // Put / api / command / {id}
    [HttpPatch("{id}")]
    public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
    {
        var commandModelFromRepo = _repository.GetCommandById(id);
        if (commandModelFromRepo == null)
        {
            return NotFound();
        }

        var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
        
        patchDoc.ApplyTo(commandToPatch, ModelState);

        if (!TryValidateModel(commandToPatch))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(commandToPatch, commandModelFromRepo);
        
        _repository.UpdateCommand(commandModelFromRepo);

        _repository.SaveChanges();

        return NoContent();
    }
    
    // Delete / api / command / {id}
    public ActionResult DeleteCommand(int id)
    {
        var commandModelFromRepo = _repository.GetCommandById(id);
        if (commandModelFromRepo == null)
        {
            return NotFound();
        }
        
        _repository.DeleteCommand(commandModelFromRepo);

        _repository.SaveChanges();

        return NoContent();
    }
}