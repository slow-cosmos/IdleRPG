using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageUI : MonoBehaviour
{   
    private TMP_Text text;
    private Color color;

    private float destroyTime = 1;
    private float moveSpeed = 1;
    private float fadeSpeed = 1;

    public float damage;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        color = text.color;

        text.text = damage.ToString();
        Invoke("DestoryText", destroyTime);
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;

        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        color.a = Mathf.Lerp(color.a, 0, Time.deltaTime * fadeSpeed);
        text.color = color;
    }

    private void DestoryText()
    {
        Destroy(gameObject);
    }
}
