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

    Image fuelIndicator1;
    Image fuelIndicator2;

    Animator boxAnimator;
    Animator pauseAnimator;

    /*        if (pauseAnimator == null) {
            Debug.Log("sab");
        } else {
            Debug.Log("ok");
        }
        */
    Game game;
    private void Start() {
        game = GameObject.Find("GameController").GetComponent<Game>();

        //levelText = GameObject.Find("LevelText").GetComponent<Text>();
        //fuelText = GameObject.Find("Fuel2Text").GetComponent<Text>();
       // coinsText = GameObject.Find("CoinsText").GetComponent<Text>();
       // fuelBar = GameObject.Find("FuelBar").GetComponent<Slider>();

        fuelIndicator1 = GameObject.Find("Oxygen").GetComponent<Image>();
        fuelIndicator2 = GameObject.Find("OxegenFill").GetComponent<Image>();
        //GameObject.Find("Oxygen").GetComponent<Animator>();

        boxAnimator = GameObject.Find("BoxHealth").GetComponent<Animator>();

        pauseAnimator = GameObject.Find("PauseStopBtn").GetComponent<Animator>();



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
        //fuelBar.maxValue = maxValue;
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
       /* scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "Score: "
            + "\nComplete level " + (level-1).ToString()
            + "\nFuel left: " + fuelLeft.ToString()
            + "\nCoins earned:" + coinsGet;*/

    }

    public void DisplayHealth(float current, float max) {

        /*float displayedHealth = Mathf.Round((current / max)*100) % 10;
        if (displayedHealth<5) {
            displayedHealth = 5;
        } else
        {
            displayedHealth = 10;
        }
        displayedHealth += Mathf.Round((current / max) * 10);*/
        //boxAnimator = GameObject.Find("BoxHealth").GetComponent<Animator>();

        if (boxAnimator != null) { 
            Debug.Log((current / max).ToString());
            boxAnimator.SetFloat("health", current/max);
            
        }
    }
    public void DisplayLevel(int currentLevel) {
       // levelText.text = "Level: " + currentLevel.ToString();
        
        //coinsText = GameObject.Find("CoinsText").GetComponent<Text>();
    }
    public void DisplayFuel(float current, float max) {
        float displayedFuel = (current / max);
        fuelIndicator1.fillAmount = displayedFuel;

        displayedFuel = (current / max) * 100f * 0.007f;

        fuelIndicator2.fillAmount = displayedFuel;
        //Debug.Log((current / max).ToString());
        //fuelBar.value = currentFuel;
        // fuelText.text = "Fuel: " + Mathf.RoundToInt(currentFuel).ToString();
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
        GameObject.Find("MenuBG").SetActive(false);
        GameObject.Find("Main_Menu").SetActive(false);

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
