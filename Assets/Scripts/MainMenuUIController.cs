using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour {
    
	public void LoadGame()
    {
        print("ASD");
        SceneManager.LoadScene(1);
    }
}
