using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public const float gravity = -20f;
    public static float wind = 0;
    const float maxWindSpeed = 50;

    bool player1Turn;
    public bool Player1Turn { get { return player1Turn; } }

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
        if (Input.GetButtonDown("Fire1")) Invoke("SwitchTurn", 0.2f);
    }

    public void GiveProjectileReference(Projectile projectile)
    {
        projectileList.Add(projectile);
    }

    public void GiveCannonReference(Cannon cannon)
    {
        cannonList.Add(cannon);
    }

    void SwitchTurn()
    {
        player1Turn = !player1Turn;
        wind = Random.Range(-maxWindSpeed, maxWindSpeed);
    }
}
