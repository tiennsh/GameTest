using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public Image HpPlayer;
    public Text HpPlayerText;
    public HpController HpEnemy;
    public GameObject gameOver;
    public bool isGameOver;
    public Transform TfEnemys;
    public Text Score;

    private void Update()
    {
        if(isGameOver)
        {
            GameOver();
        }
        if(Score)
        {
            Score.text = "Score : " + GameManeger.Ins.score.ToString();
        }
    }


    public void UpdateHpEnemy(Gun gun)
    {
        if(HpEnemy)
        {
            HpController HpEnemyClone = Instantiate(HpEnemy);
            HpEnemyClone.transform.SetParent(TfEnemys);
            HpEnemyClone.gun = gun;
        }

    }

    void ClearEnemy()
    {
        foreach (Transform child in TfEnemys)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdateHpPlayer(int rate)
    {
        if (HpPlayerText)
        {
            if (rate < 0)
                rate = 0;
            HpPlayerText.text = rate.ToString();
        }
        if (HpPlayer)
            HpPlayer.fillAmount = (float)rate/100;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        isGameOver = false;
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOver.SetActive(false);
        GameManeger.Ins.score = 0;
        ClearEnemy();
        Time.timeScale = 1f;

    }
}
