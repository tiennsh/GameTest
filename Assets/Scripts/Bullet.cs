using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed;

    public bool isEnemy;

    Vector3 moveVt3;
    void Update()
    {
        MoveLerp();
    }

    public void LookAtMouse(Transform barrel)
    {
        if(isEnemy)
            moveVt3 = new Vector3(-transform.position.x, 0f, -transform.position.z) * speed;
        else
            moveVt3 = (barrel.position - transform.position) * speed;
        Invoke("Delete", 2f);
    }

    void MoveLerp()
    {
        transform.position += (moveVt3 * Time.deltaTime);
    }

    void Delete()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Walk"))
        {
            Destroy(gameObject);
        }
    }

    

}
