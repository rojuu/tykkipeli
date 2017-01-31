using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public const float gravity = -9.8f;
    public static float wind = 0;
    const float maxWindSpeed = 10;

    bool player1Turn;
    public bool Player1Turn { get { return player1Turn; } }

    bool gameEnded;
    public bool GameEnded {  get { return gameEnded; } }

    List<Projectile> projectileList;
    List<Cannon> cannonList;

    void Awake()
    {
        if (instance == null) instance = this;
        if (instance != this) Destroy(gameObject);
        projectileList = new List<Projectile>();
        cannonList = new List<Cannon>();
    }
    
    void Start()
    {
        SwitchTurn();
    }

    void Update()
    {
        if(gameEnded && Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        CheckProjectileCollisions();
    }

    public void GiveProjectileReference(Projectile projectile)
    {
        projectileList.Add(projectile);
    }

    public void GiveCannonReference(Cannon cannon)
    {
        cannonList.Add(cannon);
    }

    public void CheckProjectileCollisions()
    {
        for(int i = projectileList.Count - 1; i >= 0; i--)
        {
            for (int j = cannonList.Count - 1; j >= 0; j--)
            {
                if (Vector3.Distance(cannonList[j].transform.position, projectileList[i].transform.position) <= (0 + cannonList[j].GetRadius() + projectileList[i].GetRadius()))
                {
                    Destroy(projectileList[i].gameObject);
                    Destroy(cannonList[j].gameObject);
                    projectileList.Remove(projectileList[i]);
                    cannonList.Remove(cannonList[j]);
                    EndGame();
                    return;
                }
            }

            if(projectileList[i].transform.position.y + projectileList[i].GetRadius() <= 0)
            {
                Destroy(projectileList[i].gameObject);
                projectileList.Remove(projectileList[i]);
                SwitchTurn();
            }
        }
    }

    void SwitchTurn()
    {
        player1Turn = !player1Turn;
        wind = Random.Range(-maxWindSpeed, maxWindSpeed);
    }

    void EndGame()
    {
        gameEnded = true;
    }
}
