using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : Singleton<GameManeger>
{
    public Enemy enemyPrefab;
    public Vector3[] pos;
    public float TimeSwap;
    public int score;

    public bool skillE;


    // Start is called before the first frame update
    public override void Start()
    {
        StartCoroutine(SwapEnemy(TimeSwap));
    }

    IEnumerator SwapEnemy(float timeSwap)
    {
        if (timeSwap < 1f) timeSwap = 1f;
        Enemy enemy = Instantiate(enemyPrefab, SwapPos(), Quaternion.identity);
        yield return new WaitForSeconds(timeSwap);
        StartCoroutine(SwapEnemy(timeSwap - 0.1f));
    }

    Vector3 SwapPos()
    {
        Vector3 pos = Vector3.zero;
        if (Random.Range(0, 2) == 0)
        {
            pos.x = Random.Range(-8.5f, 8.5f);
            int randomSign = (Random.value < 0.5f) ? -1 : 1;
            pos.z = 8.5f * randomSign;
        }
        else
        {
            pos.z = Random.Range(-8.5f, 8.5f);
            int randomSign = (Random.value < 0.5f) ? -1 : 1;
            pos.x = 8.5f * randomSign;
        }
        pos.y = 0.5f;
        return pos;
    }
}
