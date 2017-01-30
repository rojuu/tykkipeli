using UnityEngine;
using System.Collections;

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

    void Awake()
    {
        if (instance == null) instance = this;
        if (instance != this) Destroy(gameObject);
    }
    
    void Start()
    {
        SwitchTurn();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Invoke("SwitchTurn", 0.2f);
    }

    void SwitchTurn()
    {
        player1Turn = !player1Turn;
        wind = Random.Range(-maxWindSpeed, maxWindSpeed);
    }
}
