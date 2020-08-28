using System.Collections;

using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject Node1, Node2, Target;
    public float Speed;
    Renderer Renderer;

    void Start()
    {
        Renderer = GetComponent<Renderer>();
        StartCoroutine(crPatrol());
    }

    private IEnumerator crPatrol() 
    {
        while (true)
        {
            if (Renderer.isVisible)
            {
                if (Vector3.Distance(transform.position, Node1.transform.position) < 1f)
                {
                    Target = Node2;
                    transform.Rotate(new Vector3(0, 180f, 0));
                }
                if (Vector3.Distance(transform.position, Node2.transform.position) < 1f)
                {
                    Target = Node1;
                    transform.Rotate(new Vector3(0, -180f, 0));
                }
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
            }
            yield return null;
        }
    }
}
