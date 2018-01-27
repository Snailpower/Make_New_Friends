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
    private Text playerDiedText;
    private GameObject playerDiedMenu;
    private Text player1MinionCountText, player2MinionCountText, player3MinionCountText, player4MinionCountText;
    private Vector3 playerDiedMenuOriginalPosition;
    public float howLongWillPlayerDiedMenuShow = 2f;
    public float howLongPlayerHasTimeToGatherMinions = 15f;

    private bool canShowP1DiedScreen = true, canShowP2DiedScreen = true, canShowP3DiedScreen = true, canShowP4DiedScreen = true;

    private void Start()
    {
        playerDiedMenu = GameObject.Find("PlayerDiedMenu");
        playerDiedMenuOriginalPosition = playerDiedMenu.transform.position;
        player1MinionCountText = GameObject.Find("Player1MinionCount").GetComponent<Text>();
        player2MinionCountText = GameObject.Find("Player2MinionCount").GetComponent<Text>();
        player3MinionCountText = GameObject.Find("Player3MinionCount").GetComponent<Text>();
        player4MinionCountText = GameObject.Find("Player4MinionCount").GetComponent<Text>();
        playerDiedText = GameObject.Find("PlayerDiedText").GetComponent<Text>();
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
        if (howLongPlayerHasTimeToGatherMinions >= 0)
            howLongPlayerHasTimeToGatherMinions -= Time.deltaTime;
        if(howLongPlayerHasTimeToGatherMinions <= 0)
        {
            print("Show Shit");
            if (player1MinionCount <= 1 && canShowP1DiedScreen)
                ShowPlayerDiedMenu(0);
            else if (player2MinionCount <= 1 && canShowP2DiedScreen)
                ShowPlayerDiedMenu(1);
            else if (player3MinionCount <= 1 && canShowP3DiedScreen)
                ShowPlayerDiedMenu(2);
            else if (player4MinionCount <= 1 && canShowP4DiedScreen)
                ShowPlayerDiedMenu(3);
        }
        print("ASD: " + howLongPlayerHasTimeToGatherMinions);
        
    }
    private void updateMinionCountTexts()
    {
        player1MinionCountText.text = originalPlayer1Text + player1MinionCount.ToString();
        player2MinionCountText.text = originalPlayer2Text + player2MinionCount.ToString();
        player3MinionCountText.text = originalPlayer3Text + player3MinionCount.ToString();
        player4MinionCountText.text = originalPlayer4Text + player4MinionCount.ToString();
    }

    private void ShowPlayerDiedMenu(int index)
    {
        switch (index)
        {
            case 0:
                canShowP1DiedScreen = false;
                break;
            case 1:
                canShowP2DiedScreen = false;
                break;
            case 2:
                canShowP3DiedScreen = false;
                break;
            case 3:
                canShowP4DiedScreen = false;
                break;
        }

        if(howLongWillPlayerDiedMenuShow > 0)
        {
            playerDiedMenu.transform.localPosition = Vector3.zero;
            playerDiedText.text = "Player " + index + " died";
            howLongWillPlayerDiedMenuShow -= Time.deltaTime;
            print("APSODKPOK" + index + " : " + howLongWillPlayerDiedMenuShow);
        }
        else if (howLongWillPlayerDiedMenuShow <= 0)
        {
            howLongWillPlayerDiedMenuShow = 2;
            HidePlayerDiedMenu();
            return;
        }
            


    }
    private void HidePlayerDiedMenu()
    {
        playerDiedMenu.transform.position = playerDiedMenuOriginalPosition;
    }
}
