using Commander.Models;

namespace Commander.Data;

public class MockCommandRepo : ICommanderRepo
{
    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Command> GetAllCommands()
    {
        var Commands = new List<Command>()
        {
            new Command {Id = 0, Line = "Hello", HowTo = "WhatToDo", PlatForm = "Windows"},
            new Command {Id = 1, Line = "Bello", HowTo = "WhatIsThat", PlatForm = "MacOs"},
            new Command {Id = 2, Line = "Yello", HowTo = "WhatIsIt", PlatForm = "Linux"}
        };
        return Commands;
    }

    public Command GetCommandById(int id)
    {
        return new Command {Id = 0, Line = "Hello", HowTo = "WhatToDo", PlatForm = "Windows"};
    }

    public void CreateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public void UpdateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public void DeleteCommand(Command cmd)
    {
        throw new NotImplementedException();
    }
}