using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float GetRadius() { return transform.localScale.x / 2; }

    Vector3 velocity;

    void Awake()
    {
        velocity = Vector3.zero;
    }

    void Start()
    {
        GameManager.Instance.GiveProjectileReference(this);
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
