using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : BaseState
{
    public override void StartState()
    {}

    public override void EndState()
    {}

    public void StartGame() 
    {
        GameController.NextState();
    }
}
