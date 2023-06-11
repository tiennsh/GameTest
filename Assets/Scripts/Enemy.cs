using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public bool isRotation;
    public bool isRun;
    public bool isShoot;
    public float Speed;
    public Gun gun;

    [SerializeField] Quaternion targetRotation;
    Vector3 targetPosition;
    float xPos;
    float zPos;
    float distance;
    int countRotation;

    private void Start()
    {
        targetPosition = Vector3.zero;
        targetRotation = Quaternion.identity;
        RandomPos();

    }
    private void Update()
    {
        if (gun.isShowHp)
        {
            UpdateHpEnemy();
            gun.isShowHp = false;
        }
        EnemyDeath();
    }

    private void FixedUpdate()
    {

        if (isShoot)
        {
            isRun = false;
            if (!gun.isShoot)
                gun.Shoot();
            else
            {
                isShoot = false;
                gun.isShoot = false;
                targetPosition = Vector3.zero;
                targetRotation = Quaternion.identity;
                RandomPos();
            }
        }
        else if (gun.RotationReset(transform.rotation))
        {
            if (isRotation)
            {
                RotationEnemy();
            }
            else
            {
                if (targetRotation.y == Math.Sign(xPos - transform.position.x))
                {
                    Run();
                }
                else if (!isRun)
                {
                    isRun = true;
                    isRotation = true;
                }
                if (distance == Math.Sign(zPos - transform.position.z) && isRun)
                {
                    Run();
                }
                else if (isRun && !isRotation)
                {
                    isShoot = true;
                }


            }
        }


    }

    void RotationEnemy()
    {
        if (transform.position.x != xPos && !isRun)
        {
            targetRotation.y = Math.Sign(xPos - transform.position.x);
            if (countRotation == 30)
            {
                isRotation = false;
                countRotation = 0;
            }
        }

        else if (transform.position.y != zPos)
        {
            distance = Math.Sign(zPos - transform.position.z);
            if (distance > 0)
            {
                targetRotation.y = 0f;
            }
            else
            {
                targetRotation.w = 0.000001f;
                targetRotation.y = 2f;
            }

            if (countRotation == 30)
            {
                isRotation = false;
                countRotation = 0;
            }
        }


        Quaternion rotationOld = transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Speed * Time.deltaTime);
        if (rotationOld == transform.rotation)
        {
            countRotation += 1;
        }
        else
        {
            countRotation = 0;
        }

    }

    void EnemyDeath()
    {
        if (gun.hp <= 0)
        {
            GameManeger.Ins.score += 1;
            Destroy(gameObject);
        }
    }

    void RandomPos()
    {
        xPos = UnityEngine.Random.Range(-5f, 5f);
        while (xPos < 1 && xPos > -1)
        {
            xPos = UnityEngine.Random.Range(-5f, 5f);
        }
        zPos = UnityEngine.Random.Range(-5f, 5f);
        while (zPos < 1 && zPos > -1)
        {
            zPos = UnityEngine.Random.Range(-5f, 5f);
        }
        isRotation = true;
    }

    void Run()
    {
        {
            Vector3 movePos = gun.transform.position - transform.position;
            movePos.y = 0f;
            transform.position += movePos * Speed * 5 * Time.deltaTime;
        }

    }

    void UpdateHpEnemy()
    {
        GameGUIManager.Ins.UpdateHpEnemy(gun);
    }

}
