using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int hp;
    public float rotationSpeed;
    public Bullet bullet;
    public Transform barrel;
    public bool isShoot;
    public HpController hpController;
    public bool isShowHp;

    int countRotation;
    Vector3 direction;

    public void Shoot()
    {
        if (countRotation == 50)
        {
            Bullet bulletClone = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletClone.LookAtMouse(barrel);
            isShoot = true;
            countRotation = 0;
        }
        Quaternion rotationOld = transform.rotation;
        RotationGun();
        if (rotationOld == transform.rotation)
        {
            countRotation += 1;
        }
        else
        {
            countRotation = 0;
        }


    }

    void RotationGun()
    {
        direction = new Vector3(-transform.position.x, 0, -transform.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    /*public void Shoot()
    {
        Bullet bulletClone = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletClone.LookAtMouse(barrel);
        isShoot = true;
    }

    public bool RotationGun()
    {
        direction = Player.Ins.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        if (transform.rotation == targetRotation) return true;
        
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        return false;
    }*/

    public bool RotationReset(Quaternion quaternion)
    {
        if (transform.rotation == quaternion) return true;
        
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, rotationSpeed * Time.deltaTime);
        
        return false;
        
    }

    public int Hp()
    {
        return hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if(hp==100)
                isShowHp = true;
            int damage = Random.Range(1, 6);
            hp -= damage;
            Player.Ins.SkillQRun(damage);
            Destroy(other.gameObject);
        }
    }
}
