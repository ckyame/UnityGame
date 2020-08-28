using UnityEngine;

public class BaseState : MonoBehaviour
{
    public Game GameController;
    public BaseState NextState;

    public virtual void StartState() { }
    public virtual void EndState() { }
}
