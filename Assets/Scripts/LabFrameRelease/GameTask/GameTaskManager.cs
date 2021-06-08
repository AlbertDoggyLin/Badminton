using GameData;
using LabData;
using System.Collections;
using System.Collections.Generic;
public class GameTaskManager : MonoSingleton<GameTaskManager>, IGameManager

{
    int IGameManager.Weight => GobalData.GameTaskManagerWeight;

    private List<TaskBase> _mainqueuetasks;
    private List<TaskBase> _servingqueuetasks;

    void IGameManager.ManagerInit()
    {
        _mainqueuetasks = TaskFanctory.GetCurrentScopeTasks();
        _servingqueuetasks = TaskFanctory.GetServingModeTasks();
    }

    IEnumerator IGameManager.ManagerDispose()
    {
        _mainqueuetasks.Clear();
        _servingqueuetasks.Clear();
        yield return null;
    }


    public void StartMainGameTask()
    {
        _mainqueuetasks.ForEach(p => { StartCoroutine(StartGameTaskEnumerator(p)); });
    }

    public void StartServingGameTask()
    {
        _servingqueuetasks.ForEach(p => { StartCoroutine(StartGameTaskEnumerator(p)); });
    }

    private IEnumerator StartGameTaskEnumerator(TaskBase taskBase)
    {
        yield return StartCoroutine(taskBase.TaskInit());
        yield return StartCoroutine(taskBase.TaskStart());
        yield return StartCoroutine(taskBase.TaskStop());
    }
}
