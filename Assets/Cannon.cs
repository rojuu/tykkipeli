using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cannon : MonoBehaviour
{
    public Transform barrelEnd;
    public Transform barrel;
    public GameObject projectilePrefab;

    float angle = 60;
    float force = 60;

    float turretTurnSpeed = 30;
    float forceDialSpeed = 30;

    bool _isPlayer1;

	void Start ()
    {
        barrel.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        _isPlayer1 = (transform.localScale.x > 0); //if x scale is positive, we must be p1
        if(!_isPlayer1) turretTurnSpeed = -turretTurnSpeed;
    }
	
	void Update ()
    {
        if (GameManager.Instance.Player1Turn != _isPlayer1) return;

        //rotate barrel
        angle -= turretTurnSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        angle = Mathf.Clamp(angle, 5, 90);
        barrel.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //adjust force
        force += forceDialSpeed * Input.GetAxis("Vertical") * Time.deltaTime;

        //shoot pellet
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject instance;
            instance = Instantiate(projectilePrefab);
            instance.transform.position = barrelEnd.transform.position;

            Vector3 forceDir = (barrelEnd.transform.position - transform.position).normalized;
            instance.GetComponent<Projectile>().AddForce(forceDir * force);
        }
    }

    public float GetAngle() { return angle; }
    public float GetForce() { return force; }
    public bool isPlayer1() { return _isPlayer1; }
}
