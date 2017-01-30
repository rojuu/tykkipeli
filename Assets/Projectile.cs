using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    Vector3 velocity;

    void Awake()
    {
        velocity = Vector3.zero;
    }

    void Update()
    {
        velocity.y += GameManager.gravity * Time.deltaTime;
        velocity.x += GameManager.wind * Time.deltaTime;

        transform.Translate(velocity * Time.deltaTime);
    }

    public void AddForce(Vector3 force)
    {
        velocity += force;
    }
}
