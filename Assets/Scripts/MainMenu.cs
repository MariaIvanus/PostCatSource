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
    Game game;
    private void Start() {
        game = GameObject.Find("GameController").GetComponent<Game>();

        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        fuelText = GameObject.Find("Fuel2Text").GetComponent<Text>();
        coinsText = GameObject.Find("CoinsText").GetComponent<Text>();
        

        fuelBar = GameObject.Find("FuelBar").GetComponent<Slider>();

        if(fuelBar == null) {
            Debug.Log("its null"); 
        }
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
        fuelBar.value = currentFuel;
        fuelText.text = "Fuel: " + Mathf.RoundToInt(currentFuel).ToString();
        Debug.Log(currentFuel.ToString());
    }
    public void DisplayCoins(int currentCoins) {
        //coinsText.text = "Coins: " + currentCoins.ToString();
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
