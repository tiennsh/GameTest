using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Singleton<Player>
{
    public int HpPlayer;
    Vector3 target;
    public int maxBullet;
    public Transform barrel;
    public Bullet bullet;
    public Image skillEimage;
    public Image skillQimage;

    bool skillQ;
    bool skillQcooldown;
    bool skillE;
    bool skillEcooldown;
    int m_Bullet;
    


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !skillEcooldown)
        {
            StartCoroutine(SkillECooldown());

            StartCoroutine(SkillE());
            
            StartCoroutine(SkillERun());
        }
        else if(Input.GetKeyDown(KeyCode.Q) && !skillQcooldown)
        {
            StartCoroutine(SkillQCooldown());

            StartCoroutine(SkillQ());
        }
        else if (Input.GetMouseButtonDown(0) && m_Bullet < maxBullet)
        {
            StartCoroutine(Shoot());
        }
        if (HpPlayer > 100)
            HpPlayer = 100;
        if (HpPlayer<=0)
        {
            //Debug.Log("GameOver");
            GameGUIManager.Ins.isGameOver = true;
            HpPlayer = 100;
            Time.timeScale = 0f;
        }
        LookAtMouse();
        
        GameGUIManager.Ins.UpdateHpPlayer(HpPlayer);
    }

    IEnumerator SkillE()
    {
        skillE = true;
        yield return new WaitForSeconds(5f);
        skillE = false;
    }

    IEnumerator SkillERun()
    {
        Bullet bulletClone = Instantiate(bullet);
        bulletClone.LookAtMouse(barrel);
        yield return new WaitForSeconds(0.1f);
        if (skillE)
            StartCoroutine(SkillERun());
    }

    IEnumerator SkillECooldown()
    {
        skillEcooldown = true;
        skillEimage.fillAmount = 1f;
        yield return new WaitForSeconds(15f);
        skillEcooldown = false;
        skillEimage.fillAmount = 0f;
    }

    IEnumerator SkillQ()
    {
        skillQ = true;
        yield return new WaitForSeconds(2f);
        skillQ = false;
    }


    public void SkillQRun(int damage)
    {
        if (skillQ)
        {
            HpPlayer += damage;
        }

            
    }

    IEnumerator SkillQCooldown()
    {
        skillQcooldown = true;
        skillQimage.fillAmount = 1f;
        yield return new WaitForSeconds(20f);
        skillQcooldown = false;
        skillQimage.fillAmount = 0f;
    }
    
    

    IEnumerator Shoot()
    {
        if(!skillE)
        {
            Bullet bulletClone = Instantiate(bullet);
            bulletClone.LookAtMouse(barrel);
            m_Bullet += 1;
            yield return new WaitForSeconds(1f);
            m_Bullet -= 1;
        }
    }

    void LookAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            target = new Vector3(hit.point.x , 1f, hit.point.z) ;
        }
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BulletEnemy"))
        {
            HpPlayer -= Random.Range(1, 6);
            
            Destroy(other.gameObject);
        }
    }
}
