using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public Text score;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    // Start is called before the first frame update

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }
    public void UpdateScore()
    {
        score.text = "Score: " + PlayerManager.instance.score.ToString();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
