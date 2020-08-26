using System;

using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    [Serializable]
    public class PlayerHitEvent : UnityEvent { }

    [SerializeField]
    public PlayerHitEvent PlayerHitCallback = new PlayerHitEvent();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy") 
        {
            PlayerHitCallback.Invoke();
        }
    }
}
