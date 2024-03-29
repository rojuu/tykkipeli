﻿using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform barrelEnd;
    public Transform barrel;
    public GameObject projectilePrefab;

    public bool isPlayer1() { return _isPlayer1; }
    public float GetAngle() { return angle; }
    public float GetForce() { return force; }
    public float GetRadius() { return transform.localScale.x / 2; }

    float angle = 60;
    float force = 100;

    float turretTurnSpeed = 30;
    float forceDialSpeed = 100;

    bool _isPlayer1;

    void Start()
    {
        barrel.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        _isPlayer1 = (transform.localScale.x > 0); //if x scale is positive, we must be p1
        if (!_isPlayer1) turretTurnSpeed = -turretTurnSpeed;

        GameManager.Instance.GiveCannonReference(this);
    }

    void Update()
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
}
