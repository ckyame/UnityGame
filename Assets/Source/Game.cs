using System.Collections.Generic;

using UnityEngine;

public class Game : MonoBehaviour
{
    public List<BaseState> States;
    public int StartingState;
    public BaseState CurrentState;
    
    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame() 
    {
        foreach (BaseState state in States) 
        {
            state.gameObject.SetActive(false);
        }
        CurrentState = States[StartingState];
        CurrentState.gameObject.SetActive(true);
        CurrentState.StartState();
        Debug.Log(string.Format("Game: {0} started.", CurrentState.name));
    }

    public void NextState() 
    {
        Debug.Log(string.Format("Game: {0} ended.", CurrentState.name));
        CurrentState.EndState();
        CurrentState.gameObject.SetActive(false);
        CurrentState = CurrentState.NextState;
        CurrentState.gameObject.SetActive(true);
        CurrentState.StartState();
        Debug.Log(string.Format("Game: {0} started.", CurrentState.name));
    }
}
