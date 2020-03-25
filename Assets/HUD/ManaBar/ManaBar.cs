using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using CodeMonkey;

public class ManaBar : MonoBehaviour
{

    //private Image barImage;
    public Mana mana;
    private float barMaskWidth;
    private RectTransform barMaskRectTransform;
    private RectTransform edgeRectTransform;
    private RawImage barRawImage;

    private void Awake(){
        barMaskRectTransform = transform.Find("barMask").GetComponent<RectTransform>();
        barRawImage = transform.Find("barMask").Find("bar").GetComponent<RawImage>();
        edgeRectTransform = transform.Find("edge").GetComponent<RectTransform>();

        barMaskWidth = barMaskRectTransform.sizeDelta.x;
        mana = new Mana();
/*
        CMDebug.Botton UI(new Vector2(0,-50) , "Spend Mana" , () => {
            mana.TrySpendMana(30);
        });
*/
    }

    private void Update(){
        mana.Update();
        //barImage.fillAmount = mana.GetManaNormalized();
        Rect uvRect = barRawImage.uvRect;
        uvRect.x += 0.1f * Time.deltaTime;
        barRawImage.uvRect = uvRect;

        Vector2 barMaskSizeDelta = barMaskRectTransform.sizeDelta;
        barMaskSizeDelta.x = mana.GetManaNormalized() * barMaskWidth;
        barMaskRectTransform.sizeDelta = barMaskSizeDelta;

        edgeRectTransform.anchoredPosition = new Vector2(mana.GetManaNormalized() * barMaskWidth , 0);
        edgeRectTransform.gameObject.SetActive(mana.GetManaNormalized() < 1f);
    }

}

public class Mana {

    public const int MANA_MAX = 100;

    private float manaAmount;
    private float manaRegenAmount;    

    public Mana(){
        
        manaAmount = 80;
        manaRegenAmount = 10f;
    }

    public void Update(){
        manaAmount += manaRegenAmount * Time.deltaTime;
        manaAmount = Mathf.Clamp(manaAmount, 0f, MANA_MAX);
    }

    public bool TrySpendMana(int amount){
        if (manaAmount >= amount){
            manaAmount -= amount;
            return true;
        }
        return false;
    }

    public float GetManaNormalized(){
        return manaAmount/MANA_MAX;
    }

}