using UnityEngine;

public class KillEnemyCollision : MonoBehaviour
{
    public GameState GameState;

    private void Start()
    {
        GameState = FindObjectOfType<GameState>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            GameState.PlayerHit();
        }
    }
}
