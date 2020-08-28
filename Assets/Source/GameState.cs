using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameState : BaseState
{
    // Player Gameobject Data
    public Transform Player;
    public Rigidbody PlayerRigidBody;
    public float PlayerSpeedUp;
    public float PlayerSpeedSides;
    public GameObject BunnyIdleAnim;
    public GameObject BunnyLeftRunAnim;
    public GameObject BunnyRightRunAnim;

    public GameObject UIPanel;
    public GameObject ScoredSprite;
    public Text ScoreText;
    public GameObject TweenDestination;
    public GameObject PlatformPrefab;
    public List<GameObject> LoadedPlatforms;

    public int PlayerScore = 0;
    public int ScoreThreshold = 0;

    public override void StartState()
    {
        StartCoroutine(crWatchInput());
        StartCoroutine(crSpawnPlatforms());
        StartCoroutine(crScoreTimer());
        BunnyLeftRunAnim.SetActive(false);
        BunnyRightRunAnim.SetActive(false);
        BunnyIdleAnim.SetActive(true);
    }

    public override void EndState()
    {
        // Stop scoring timer
        StopCoroutine(crScoreTimer());
        // Stop handling input
        StopCoroutine(crWatchInput());
        // Unload spawned platforms
        for (int i = 0; i < LoadedPlatforms.Count; i++) 
        {
            Destroy(LoadedPlatforms[i]);
        }
        LoadedPlatforms = new List<GameObject>();
        // Reset player position/physics
        Player.position = new Vector3(0f, -3.17f, 0f);
        Player.rotation = Quaternion.Euler(Vector3.zero);
        PlayerRigidBody.velocity = Vector3.zero;
        PlayerRigidBody.angularVelocity = Vector3.zero;
    }

    public IEnumerator PlayerScored(float points = 0) 
    {
        GameObject scoredSprite = Instantiate(ScoredSprite, UIPanel.transform) as GameObject;
        yield return null;
        scoredSprite.GetComponent<Text>().text = ((int)points).ToString();
        Debug.Log(scoredSprite.transform);
        yield return null;
        iTween.MoveTo(scoredSprite, iTween.Hash(
            "position", ScoreText.gameObject.transform.position,
            "easeType", iTween.EaseType.spring,
            "loopType", iTween.LoopType.none,
            "delay", .1,
            "time", 2f));
        StartCoroutine(crDestroyScoreSprite(scoredSprite));
        yield return null;
        PlayerScore +=(int)points;
        ScoreText.text = PlayerScore.ToString();
        Debug.Log("Player Scored " + points + ", total points: " + PlayerScore);
    }

    private IEnumerator crDestroyScoreSprite(GameObject sprite) 
    {
        yield return new WaitForSeconds(2f);
        Destroy(sprite);
    }

    public void onComplete() 
    {
        Debug.Log("Completed0");
    }

    public void PlayerHit()
    {
        PlayerLost();
    }

    private IEnumerator crWatchInput() 
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                Player.position = Vector3.MoveTowards(Player.position, Player.position + (Vector3.left * 5), PlayerSpeedSides * Time.deltaTime);
                if (BunnyLeftRunAnim.activeSelf == false)
                {
                    BunnyLeftRunAnim.SetActive(true);
                    BunnyIdleAnim.SetActive(false);
                }
            }
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                Player.position = Vector3.MoveTowards(Player.position, Player.position + (Vector3.right * 5), PlayerSpeedSides * Time.deltaTime);
                if (BunnyRightRunAnim.activeSelf == false) 
                {
                    BunnyRightRunAnim.SetActive(true);
                    BunnyIdleAnim.SetActive(false);
                }
            }
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
            {
                BunnyLeftRunAnim.SetActive(false);
                BunnyRightRunAnim.SetActive(false);
                BunnyIdleAnim.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.W) && PlayerRigidBody.velocity.y == 0)
            {
                PlayerRigidBody.AddForce(Vector3.up * PlayerSpeedUp, ForceMode.Force);
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                PlayerRigidBody.velocity = new Vector3(0, PlayerRigidBody.velocity.y, 0);
                BunnyLeftRunAnim.SetActive(false);
                BunnyRightRunAnim.SetActive(false);
                BunnyIdleAnim.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) 
            {
                if (Input.GetKeyDown(KeyCode.A)) 
                {
                    BunnyLeftRunAnim.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    BunnyRightRunAnim.SetActive(true);
                }

                BunnyIdleAnim.SetActive(false);        
            }
            
            yield return null;
        }
    }

    private IEnumerator crScoreTimer() 
    {
        //while (true) 
        //{
        //    if (Player.position.y > ScoreThreshold) 
        //    {
        //        //ScoreThreshold = (int)Player.position.y;
        //        //StartCoroutine(PlayerScored(System.Math.Abs(PlayerRigidBody.velocity.y)));
        //    }
        //    yield return new WaitForSeconds(1f);
        //}
        yield return null;
    }

    private IEnumerator crSpawnPlatforms() 
    {
        float i = 5f;
        //while (i < 10) 
        //{
            for (i = -2f; i < 100; i += 3)
            {
                for (int j = 0; j < 25; j++)
                {
                    GameObject platform = Instantiate(PlatformPrefab) as GameObject;
                    platform.transform.position =
                        (Vector3.up * i)
                      + (i % 2 == 0
                        ? Vector3.left * Random.Range(-90f, 90f)
                        : Vector3.right * Random.Range(-90f, 90f));
                    LoadedPlatforms.Add(platform);
                    yield return null;
                }
                yield return null;
            }
            yield return null;
        //}
    }

    private void PlayerLost() 
    {
        GameController.NextState();
    }

    public void PlayerScoredEvent() 
    {
        StartCoroutine(PlayerScored(5f));
        PlayerRigidBody.AddForce(Vector3.up * PlayerSpeedUp/2, ForceMode.Force);
    }
}
