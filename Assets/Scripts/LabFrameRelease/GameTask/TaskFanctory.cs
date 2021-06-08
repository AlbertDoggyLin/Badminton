using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestGameFrame;

public class TaskFanctory 
{
    public static List<TaskBase> GetCurrentScopeTasks()
    {
        var temptasks = new List<TaskBase>
        {
            new mainGameTask(),
            new gameEndTask()
        };
        return temptasks;
    }
    public static List<TaskBase> GetServingModeTasks()
    {
        var temptasks = new List<TaskBase>
        {

        };
        return temptasks;
    }
}
