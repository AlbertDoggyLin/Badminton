﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestGameFrame;

public class TaskFanctory 
{
    public static List<TaskBase> GetCurrentScopeTasks()
    {
        var temptasks = new List<TaskBase>
        {
            new mainGameTask()
        };
        return temptasks;
    }
}