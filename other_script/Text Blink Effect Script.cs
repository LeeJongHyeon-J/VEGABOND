using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    private Text text;
    [SerializeField] private float blinkSpeed = 50f;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        float alpha = (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);


    }
}