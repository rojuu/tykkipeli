using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 velocity;
    Vector3 acceleration;
    Vector3 force;
    Vector3 deltaPos;
    Vector3 newAcceleration;
    Vector3 averageAcceleration;
    Vector3 position;
    Vector3 externalForce;

    public float mass = 1;
    float radius;
    float restitution; //bounciness
    float density;
    float drag;
    float area; //frontal area
    float gravity;

    void Awake()
    {
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        force = Vector3.zero;
        deltaPos = Vector3.zero;
        newAcceleration = Vector3.zero;
        averageAcceleration = Vector3.zero;
        externalForce = Vector3.zero;

        radius = transform.localScale.x / 2;
        restitution = -0.5f;
        density = 1.2f;
        drag = 0.47f;
        area = Mathf.PI * radius * radius;
        gravity = GameManager.gravity;
    }

    void Start()
    {
        GameManager.Instance.GiveProjectileReference(this);
        position = transform.position;
    }

    void Update()
    {
        //reset force every frame
        force = Vector3.zero;
        
        //add any external forces from AddForce()
        force.x += mass * externalForce.x;
        force.y += mass * externalForce.y;
        externalForce = Vector3.zero;

        //add wind force
        //force.x += mass * GameManager.wind;

        //weight force
        force.y += mass * gravity;

        //air resistance force
        force.y += -1 * 0.5f * density * drag * area * velocity.y * velocity.y;
        force.x += -1 * 0.5f * density * drag * area * (velocity.x - GameManager.wind) * (velocity.x - GameManager.wind);

        //verlet integration
        deltaPos.y = velocity.y * Time.deltaTime + (0.5f * acceleration.y * Time.deltaTime * Time.deltaTime);
        deltaPos.x = velocity.x * Time.deltaTime + (0.5f * acceleration.x * Time.deltaTime * Time.deltaTime);

        transform.Translate(deltaPos * 100);
        
        newAcceleration.y = force.y / mass;
        newAcceleration.x = force.x / mass;

        averageAcceleration.y = 0.5f * (newAcceleration.y + acceleration.y);
        averageAcceleration.x = 0.5f * (newAcceleration.x + acceleration.x);

        velocity.y += averageAcceleration.y * Time.deltaTime;
        velocity.x += averageAcceleration.x * Time.deltaTime;

        //velocity.y += GameManager.gravity * Time.deltaTime;
        //velocity.x += GameManager.wind * Time.deltaTime;

        //transform.Translate(velocity * Time.deltaTime);
    }

    public void AddForce(Vector3 force)
    {
        externalForce = force;
    }

    public float GetRadius()
    {
        return radius;
    }
}
