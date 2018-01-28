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

    private GameObject died1;
    private GameObject died2;
    private GameObject died3;
    private GameObject died4;


    public GameObject timeSliderPrefab;

    private GameObject slider1;
    private GameObject slider2;
    private GameObject slider3;
    private GameObject slider4;


    private Text player1MinionCountText, player2MinionCountText, player3MinionCountText, player4MinionCountText;
    private Vector3 playerDiedMenuOriginalPosition;
    public float howLongWillPlayerDiedMenuShow = 2f;

    public float howLongPlayerHasTimeToGatherMinions = 15f;

    private float p1Counter;
    private float p2Counter;
    private float p3Counter;
    private float p4Counter;

    private bool canShowP1DiedScreen = true, canShowP2DiedScreen = true, canShowP3DiedScreen = true, canShowP4DiedScreen = true;

    private void Start()
    {

        p1Counter = howLongPlayerHasTimeToGatherMinions;
        p2Counter = howLongPlayerHasTimeToGatherMinions;
        p3Counter = howLongPlayerHasTimeToGatherMinions;
        p4Counter = howLongPlayerHasTimeToGatherMinions;

        playerDiedMenu = transform.GetChild(4).gameObject;
        playerDiedMenuOriginalPosition = playerDiedMenu.transform.position;
        player1MinionCountText = transform.GetChild(0).GetComponent<Text>();
        player2MinionCountText = transform.GetChild(1).GetComponent<Text>();
        player3MinionCountText = transform.GetChild(2).GetComponent<Text>();
        player4MinionCountText = transform.GetChild(3).GetComponent<Text>();

        slider1 = Instantiate(timeSliderPrefab, player1MinionCountText.transform.position, Quaternion.identity, player1MinionCountText.transform);
        slider2 = Instantiate(timeSliderPrefab, player2MinionCountText.transform.position, Quaternion.identity, player2MinionCountText.transform);
        slider3 = Instantiate(timeSliderPrefab, player3MinionCountText.transform.position, Quaternion.identity, player3MinionCountText.transform);
        slider4 = Instantiate(timeSliderPrefab, player4MinionCountText.transform.position, Quaternion.identity, player4MinionCountText.transform);

        playerDiedText = playerDiedMenu.transform.GetComponent<Text>();
        originalPlayer1Text = player1MinionCountText.text;
        originalPlayer2Text = player2MinionCountText.text;
        originalPlayer3Text = player3MinionCountText.text;
        originalPlayer4Text = player4MinionCountText.text;
    }
    private void Update()
    {
        slider1.GetComponent<Slider>().value = p1Counter;
        slider2.GetComponent<Slider>().value = p2Counter;
        slider3.GetComponent<Slider>().value = p3Counter;
        slider4.GetComponent<Slider>().value = p4Counter;


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

            if (player1MinionCount <= 1 && canShowP1DiedScreen)
            {
                p1Counter -= Time.deltaTime;

            if (p1Counter <= 0)
            {
                ShowPlayerDiedMenu(0);

                if (GameObject.Find("Player1") != null)
                {
                    Camera.main.GetComponent<CameraFollow>().targets.Remove(GameObject.Find("Player1").transform);
                    GameObject.Find("Player1").SetActive(false);
                    
                }
            }
                
            }
        else if (player1MinionCount > 1)
        {
            p1Counter = howLongPlayerHasTimeToGatherMinions;
        }
                
            if (player2MinionCount <= 1 && canShowP2DiedScreen)
        {
            p2Counter -= Time.deltaTime;

            if (p2Counter <= 0)
            {
                ShowPlayerDiedMenu(1);

                if (GameObject.Find("Player2") != null)
                {
                    Camera.main.GetComponent<CameraFollow>().targets.Remove(GameObject.Find("Player2").transform);
                    GameObject.Find("Player2").SetActive(false);
                    
                }
                
            }
        }
        else if (player2MinionCount > 1)
        {
            p2Counter = howLongPlayerHasTimeToGatherMinions;
        }

        if (player3MinionCount <= 1 && canShowP3DiedScreen)
        {
            p3Counter -= Time.deltaTime;

            if (p3Counter <= 0)
            {
                ShowPlayerDiedMenu(2);

                if (GameObject.Find("Player3") != null)
                {
                    Camera.main.GetComponent<CameraFollow>().targets.Remove(GameObject.Find("Player3").transform);
                    GameObject.Find("Player3").SetActive(false);
                    
                }
            }
        }
        else if (player3MinionCount > 1)
        {
            p3Counter = howLongPlayerHasTimeToGatherMinions;
        }

        if (player4MinionCount <= 1 && canShowP4DiedScreen)
        {
            p4Counter -= Time.deltaTime;

            if (p4Counter <= 0)
            {
                ShowPlayerDiedMenu(3);

                if (GameObject.Find("Player4") != null)
                {
                    Camera.main.GetComponent<CameraFollow>().targets.Remove(GameObject.Find("Player4").transform);
                    GameObject.Find("Player4").SetActive(false);
                    
                }
            }
        }
        else if (player4MinionCount > 1)
        {
            p4Counter = howLongPlayerHasTimeToGatherMinions;
        }
        
        
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

            playerDiedMenu.SetActive(true);

            if (index == 0 && died1 == null)
            {
                died1 = Instantiate(playerDiedMenu, player1MinionCountText.transform.position, Quaternion.identity, this.transform);
                died1.transform.position = player1MinionCountText.transform.position;
            }
            else if (index == 1 && died2 == null)
            {
                died2 = Instantiate(playerDiedMenu, player2MinionCountText.transform.position, Quaternion.identity, this.transform);
                died2.transform.position = player2MinionCountText.transform.position;
            }
            else if (index == 2 && died3 == null)
            {
                died3 = Instantiate(playerDiedMenu, player3MinionCountText.transform.position, Quaternion.identity, this.transform);
                died3.transform.position = player3MinionCountText.transform.position;
            }
            else if (index == 3 && died4 == null)
            {
                died4 = Instantiate(playerDiedMenu, player4MinionCountText.transform.position, Quaternion.identity, this.transform);
                died4.transform.position = player4MinionCountText.transform.position;
            }
            


    }
    private void HidePlayerDiedMenu()
    {
        playerDiedMenu.SetActive(false);
    }
}
