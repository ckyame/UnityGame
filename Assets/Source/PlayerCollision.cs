using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameState GameState;

    public GameObject ObjectToDestroy;
    public bool DestroyOnCollision = false;

    private void Start()
    {
        GameState = FindObjectOfType<GameState>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameState.PlayerScoredEvent();
        if (DestroyOnCollision) 
        {
            Destroy(ObjectToDestroy);
        }
    }
}
