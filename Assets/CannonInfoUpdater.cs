using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CannonInfoUpdater : MonoBehaviour
{
    public Cannon cannon;

    Text UIText;

    float angle;
    float force;

    void Start()
    {
        if (cannon == null) Destroy(gameObject);
        
        angle = cannon.GetAngle();
        force = cannon.GetForce();

        UIText = GetComponent<Text>();
        if (UIText == null) Destroy(gameObject);

        UpdateUIText();
    }

    void Update()
    {
        if(angle != cannon.GetAngle() || force != cannon.GetForce())
        {
            angle = cannon.GetAngle();
            force = cannon.GetForce();
            UpdateUIText();
        }
    }
    
    void UpdateUIText()
    {
        UIText.text = "Angle: " + angle + "\nForce: " + force;
    }
}
