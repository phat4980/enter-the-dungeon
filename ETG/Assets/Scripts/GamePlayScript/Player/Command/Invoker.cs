using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker
{
    private List<ICommand> commands = new List<ICommand>();
    public void SetCommand(ICommand command)
    {
        commands.Add(command);
    }

    public void OnPress(int pos)
    {
        commands[pos].Execute();
    }
}
