using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public int player1MinionCount = 0;
    public int player2MinionCount = 0;
    public int player3MinionCount = 0;
    public int player4MinionCount = 0;
    private string originalPlayer1Text, originalPlayer2Text, originalPlayer3Text, originalPlayer4Text;

    private Text player1MinionCountText, player2MinionCountText, player3MinionCountText, player4MinionCountText;

    private void Start()
    {
        player1MinionCountText = GameObject.Find("Player1MinionCount").GetComponent<Text>();
        player2MinionCountText = GameObject.Find("Player2MinionCount").GetComponent<Text>();
        player3MinionCountText = GameObject.Find("Player3MinionCount").GetComponent<Text>();
        player4MinionCountText = GameObject.Find("Player4MinionCount").GetComponent<Text>();
        originalPlayer1Text = player1MinionCountText.text;
        originalPlayer2Text = player2MinionCountText.text;
        originalPlayer3Text = player3MinionCountText.text;
        originalPlayer4Text = player4MinionCountText.text;
    }
    private void Update()
    {
        getMinionCounts();
        updateMinionCountTexts();
    }
    private void getMinionCounts()
    {
        player1MinionCount = 0;
        player2MinionCount = 0;
        player3MinionCount = 0;
        player4MinionCount = 0;
        GameObject[] infected = GameObject.FindGameObjectsWithTag("Infected");
        foreach (GameObject temp in infected)
        {
            if(temp.GetComponent<Infected>().index == 0)
            {
                player1MinionCount++;
            }
            else if (temp.GetComponent<Infected>().index == 1)
            {
                player2MinionCount++;
            }
            else if (temp.GetComponent<Infected>().index == 2)
            {
                player3MinionCount++;
            }
            else if (temp.GetComponent<Infected>().index == 3)
            {
                player4MinionCount++;
            }
        }
    }
    private void updateMinionCountTexts()
    {
        player1MinionCountText.text = originalPlayer1Text + player1MinionCount.ToString();
        player2MinionCountText.text = originalPlayer2Text + player2MinionCount.ToString();
        player3MinionCountText.text = originalPlayer3Text + player3MinionCount.ToString();
        player4MinionCountText.text = originalPlayer4Text + player4MinionCount.ToString();
    }
}
