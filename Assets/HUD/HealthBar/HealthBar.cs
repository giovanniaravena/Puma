using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform rectTransform;
    public static float Health { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        Health = 100f;
        rectTransform = GetComponent<RectTransform>();
        foreach (Transform item in this.transform)
        {
            rectTransform = item.GetComponent<RectTransform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float UpdateLife = Mathf.MoveTowards(rectTransform.rect.height, Health, 5.0f);
        rectTransform.sizeDelta = new Vector2(100f, Mathf.Clamp(UpdateLife, 0.0f, 100f));
        //GameObject child = HealthBar.G
    }
    public void SetHealth(float amount)
    {
        Debug.Log("SET HEALTH WIITH " + amount +".");
        Health += amount;
    }
}
