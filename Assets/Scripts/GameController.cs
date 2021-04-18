using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOver;
    public float score;
    public float scoreCoin;
    public Text scoreCoinText;
    public Text scoreText;
    private Player player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isDead)
        {
            score += Time.deltaTime * 5f;
            scoreText.text = Mathf.Round(score).ToString() + "m"; //Mathf.Round arredonda o float
        }

    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);


    }

    public void AddCoin()
    {
        scoreCoin++;
        scoreCoinText.text = scoreCoin.ToString();
    }

    public void RestartGame(string lvlname)
    {
        SceneManager.LoadScene(lvlname);
    }
}
