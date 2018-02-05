using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

// COLORS C1148C - pink & 280A55 - violet

public class MainMenu : MonoBehaviour {

    Slider fuelBar;
    Text levelText;
    Text fuelText;
    Text coinsText;
    Text scoreText;

    Animator fuelAnimator;
    Animator boxAnimator;
    Animator pauseAnimator;


    Game game;
    private void Start() {
        game = GameObject.Find("GameController").GetComponent<Game>();

        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        fuelText = GameObject.Find("Fuel2Text").GetComponent<Text>();
        coinsText = GameObject.Find("CoinsText").GetComponent<Text>();
        fuelBar = GameObject.Find("FuelBar").GetComponent<Slider>();

        fuelAnimator = GameObject.Find("Oxygen").GetComponent<Animator>();
        if (fuelAnimator == null) {
            Debug.Log("sab1");
        } else {
            Debug.Log("ok");
        }
        boxAnimator = GameObject.Find("BoxHealth").GetComponent<Animator>();
        if (boxAnimator == null) {
            Debug.Log("sab22");
        } else {
            Debug.Log("ok");
        }
        pauseAnimator = GameObject.Find("PauseStopBtn").GetComponent<Animator>();
        if (pauseAnimator == null) {
            Debug.Log("sab");
        } else {
            Debug.Log("ok");
        }


    }
    private bool isPaused;
    public void PauseToggle() {
        if (isPaused) {
            PauseOff();
        } else {
            PauseOn();
        }
    }
    public void PauseOn() {
       // pauseAnimator.SetBool("Paused", true);
        pauseAnimator.SetFloat("Blend", 1.0f);
        game.PauseOn();
        isPaused = true;
    }

    public void PauseOff() {
        pauseAnimator.SetBool("Paused", false);
        pauseAnimator.SetFloat("Blend", 0.0f);
        game.PauseOff();
        isPaused = false;
    }

    public void SetMaxFuelBar(float maxValue) {
        fuelBar.maxValue = maxValue;
    }

    public void CleanUp() {
        game.CleanUp();
    }
    public void ResetGame() {
        game.ResetLevel();
    }
    public void Restart() {
        game.HideGameOver();
    }
    public void NextLevel() {
        game.hideWinUI();
    }

    public void ShowScore(int level, float fuelLeft, int coinsGet) {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "Score: "
            + "\nComplete level " + (level-1).ToString()
            + "\nFuel left: " + fuelLeft.ToString()
            + "\nCoins earned:" + coinsGet;

    }

    public void DisplayLevel(int currentLevel) {
        levelText.text = "Level: " + currentLevel.ToString();
        
        //coinsText = GameObject.Find("CoinsText").GetComponent<Text>();
    }
    public void DisplayFuel(float currentFuel) {

       /// fuelAnimator.SetFloat

        //fuelBar.value = currentFuel;
        // fuelText.text = "Fuel: " + Mathf.RoundToInt(currentFuel).ToString();
        //animator.SetFloat("horizontal", h);
        ///Debug.Log("fuel"+ currentFuel.ToString());
    }
    public void DisplayCoins(int currentCoins) {
        //coinsText.text = "Coins: " + currentCoins.ToString();
        //Debug.Log("coins" + currentCoins.ToString());
    }

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Stop("MenuTheme");
        FindObjectOfType<AudioManager>().Play("MainTheme");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlaySound()
    {
        //TODO: добавить задрежку для проигрывания звука слайдера - зайдает
        FindObjectOfType<AudioManager>().Play("btn");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    
}
