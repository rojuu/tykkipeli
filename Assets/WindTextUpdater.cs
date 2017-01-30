using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WindTextUpdater : MonoBehaviour
{
    float wind;
    Text windText;

    void Start()
    {
        wind = GameManager.wind;
        windText = GetComponent<Text>();
        if (windText == null) Destroy(gameObject);

        UpdateUIText();
    }

    void Update()
    {
        if(wind != GameManager.wind) UpdateUIText();
    }

    void UpdateUIText()
    {
        wind = GameManager.wind;
        windText.text = "Wind: " + wind;
    }
}
