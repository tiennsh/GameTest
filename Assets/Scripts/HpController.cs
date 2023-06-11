using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    public Image Hp;
    public Text HpText;
    public RectTransform rectTransform;
    public Gun gun;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    // Update is called once per frame
    void Update()
    {
        if(gun)
        {
            Vector3 pos = new Vector3(gun.transform.position.x, gun.transform.position.y + 1f, gun.transform.position.z);
            rectTransform.position = Camera.main.WorldToScreenPoint(pos);
            UpdateHpEnemy(gun.hp);
            if(gun.hp <=0)
            {
                Destroy(gameObject);
            }
        }    
    }

    void UpdateHpEnemy(int rate)
    {
        if (HpText)
            HpText.text = rate.ToString();
        if (Hp)
            Hp.fillAmount = (float)rate / 100;
    }
}
