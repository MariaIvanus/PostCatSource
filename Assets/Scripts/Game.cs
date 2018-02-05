using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public float offset = 10f;
    public Transform startPoint;
    public Transform endPoint;
    public Transform sections;
    private Sections sectionsClass;
    private StartPoint startPointClass;
    private EndPoint endPointClass;

    public int currentLevel;
    LevelGenerator levelGenerator;
    MainMenu menu;
    GameObject hudUI;
    GameObject gameOverUI;
    GameObject winUI;
    GameObject main_menu;

    Cargo cargoClass;
    Postcat postcatClass;
    GameObject postcatObj;
    public Transform postcatPrefab;
    public Transform postcatCargoPrefab;
    GameObject backgroundObj;
    public Transform backgroundPrefab;

    private int playerSavedLevel;
    private float playerSavedFuel;
    private int playerSavedCoin;

    private void Start() {
        FindObjectOfType<AudioManager>().Play("MainTheme");

        currentLevel = 0;
        playerSavedFuel = 0;

        sectionsClass = sections.GetComponent<Sections>();
        startPointClass = startPoint.GetComponent<StartPoint>();
        endPointClass = endPoint.GetComponent<EndPoint>();


        levelGenerator = new LevelGenerator(currentLevel);
        menu = GameObject.Find("Menu").GetComponent<MainMenu>();
        gameOverUI = menu.transform.Find("GameOverUI").gameObject;
        winUI = menu.transform.Find("WinUI").gameObject;
        hudUI = menu.transform.Find("Panel").gameObject;
        //main_menu = menu.transform.Find("Main_Menu").gameObject;
        //InitBackground();


        InitLevel();
        InitPlayer();
        SetCatValues();
        DisplayHUD();
    }



    void InitPlayer() {
        if (currentLevel <= -1) {
            postcatObj = Instantiate(
                    postcatPrefab,
                    startPoint.transform.position,
                    startPoint.transform.rotation).gameObject;

            postcatClass = postcatObj.GetComponent<Postcat>();

            Camera.main.GetComponent<CameraController>().target = postcatObj.transform;
            postcatObj.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 20.0f, ForceMode2D.Impulse);
        }
        else {
            InitPlayerWithPackage();

        }
    }
    public void InitPlayerWithPackage() {

       // GameObject respawn = GameObject.FindGameObjectWithTag("Respawn");
        Debug.Log("package");
        postcatObj = Instantiate(
            postcatCargoPrefab,
            startPoint.transform.position,
            startPoint.transform.rotation).gameObject;

        // Get fuel from station.
        //Postcat postcat = postcatObj.GetComponentInChildren<Postcat>();
        //postcat.fuel = 100 + gameState.Take();
        postcatClass = postcatObj.GetComponentInChildren<Postcat>();
        cargoClass = postcatObj.GetComponentInChildren<Cargo>();
        cargoClass.currenthealth = cargoClass.maxhealth;
        // Set target for main camera.
        Camera.main.GetComponent<CameraController>().target = postcatObj.transform.GetChild(0);
        // Push Postcat away from the station.
        foreach (Rigidbody2D rb in postcatObj.GetComponentsInChildren<Rigidbody2D>())
            rb.AddForce(Vector3.right * 10.0f, ForceMode2D.Impulse);
    }









    void InitBackground() {
        backgroundObj = Instantiate(backgroundPrefab, Vector3.zero, Quaternion.identity).gameObject;
    }
    void newGame() {
        currentLevel = 1;
        playerSavedFuel = 0;
    }

    void DrawLevel() {
        startPointClass.createStartPoint(Vector3.zero);

        float sectionsPositoinX = offset + startPoint.transform.position.x + startPoint.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        sections.transform.position = new Vector3(sectionsPositoinX, 0f, 0f);

        levelGenerator = new LevelGenerator(currentLevel);
        sectionsClass.CreateSections(levelGenerator.maxSectionNumber);

        endPointClass.createEndPoint();
        float endPointPositionX = offset + sectionsClass.SectionsEndPostionX();
        endPoint.transform.position = new Vector3(endPointPositionX, 0f, 0f);
        endPointClass.SetPosition();
    }

    void InitLevel() {

        DrawLevel();
       
    }
    // TODO: FIX THAT
    public void LoadNextLevel() {
        OnWin();
        sectionsClass.CleanUp();
        startPointClass.CleanUp();
        //move start point and children

        startPoint.transform.position = endPoint.transform.position; ;
        startPointClass.createStartPoint(startPoint.transform.position);

        float sectionsPositoinX = offset + startPoint.transform.position.x + startPoint.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        sections.transform.position = new Vector3(sectionsPositoinX, 0f, 0f);

        levelGenerator = new LevelGenerator(currentLevel);
        sectionsClass.CreateSections(levelGenerator.maxSectionNumber);
        endPointClass.CleanUp();
        endPointClass.createEndPoint();
        float endPointPositionX = offset + sectionsClass.SectionsEndPostionX();
        endPoint.transform.position = new Vector3(endPointPositionX, 0f, 0f);
        endPointClass.SetPosition();

        SetCatValues();
        DisplayHUD();
    }

    

    public void CleanUp() {
        sectionsClass.CleanUp();
        startPointClass.CleanUp();
        endPointClass.CleanUp();
        Destroy(postcatObj);
        //Destroy(backgroundObj);
    }

    public void ResetLevel() {
        Camera.main.GetComponent<CameraController>().transform.position = Vector3.zero;
        FindObjectOfType<Background>().PlaceOnStart();
        //InitBackground();
        //InitBackground();
        InitLevel();
        InitPlayer();
        SetCatValues();
        DisplayHUD();

    }
    /*----------WIN-AND-OVER------------------*/
    public void ShowGameOver() {
        Debug.Log("show go" + postcatClass.currentfuel);
        FindObjectOfType<AudioManager>().Stop("engine");
        FindObjectOfType<AudioManager>().Play("lose");
        PauseOn();
        if (gameOverUI != null) {
            hudUI.SetActive(false);
            gameOverUI.SetActive(true);
        }
        CleanUp();
        ResetLevel();

    }
    public void HideGameOver() {
        if (gameOverUI.activeSelf == true) {
            gameOverUI.SetActive(false);
            hudUI.SetActive(true);
        }
        PauseOff();
    }
    public void hideWinUI() {
        //currentLevel++;

        if (winUI.activeSelf == true) {
            winUI.SetActive(false);
            hudUI.SetActive(true);
        }
        PauseOff();
    }

    private void OnWin() {
        currentLevel++;

        playerSavedLevel = currentLevel;
        playerSavedFuel = postcatClass.currentfuel;
        postcatClass.currentfuel = 10;
        cargoClass.currenthealth = cargoClass.maxhealth;


        playerSavedCoin = 100;
        FindObjectOfType<AudioManager>().Stop("engine");
        FindObjectOfType<AudioManager>().Play("win");
    }

    public void ShowWinUI() {
       
        PauseOn();
        if (winUI != null) {
            hudUI.SetActive(false);
            winUI.SetActive(true);
            menu.ShowScore(playerSavedLevel, playerSavedFuel, playerSavedCoin);
        }
        CleanUp();
        OnWin();
        //
        ResetLevel();

    }

    /*----------CAT-VALUES------------------*/

    private void FixedUpdate() {
        DisplayHUD();
    }
    void SetCatValues() {
        getMaxFuel();
    }

    public float getMaxFuel() {
        float tempFuel = levelGenerator.giveInitialFuel();
        tempFuel += playerSavedFuel;

        menu.SetMaxFuelBar(tempFuel);
        return tempFuel;
    }

    public void DisplayHUD() {
        menu.DisplayLevel(currentLevel);
        menu.DisplayFuel(postcatClass.currentfuel,postcatClass.startfuel);
        //Debug.Log(postcatClass.currentfuel.ToString());
        //m
    }

    /*private void Destroy(GameObject toDestroy) {
        if(toDestroy != null) {
            Debug.Log(toDestroy.tag + toDestroy.name);
            if (toDestroy.tag == "checkpoint") {
                toDestroy.GetComponent<CheckPointController>().Destroy();
            } else if (toDestroy.tag == "LevelSection") {
                toDestroy.GetComponent<LSController>().Destroy();
            } else if (toDestroy.tag == "Player") {
                toDestroy.GetComponent<Postcat>().Destroy();
            }
        } else {
            Debug.Log("its null");
        }
    }*/
    /*public void Restart() {
        PauseOn();
        levelSectionAdded = 0;
        LSController.playerEnterSection -= this.NextSection;
        CheckPointController.playerRichCheckPoint -= this.NextLevel;

        prevLevelSection = GameObject.Find("GameController").GetComponent<GameController>().prevLevelSection;
        Destroy(prevLevelSection);
        curLevelSection = GameObject.Find("GameController").GetComponent<GameController>().curLevelSection;
        Destroy(curLevelSection);
        nextLevelSection = GameObject.Find("GameController").GetComponent<GameController>().nextLevelSection;
        Destroy(nextLevelSection);

        checkpoint = GameObject.Find("GameController").GetComponent<GameController>().checkpoint;
        if (checkpoint == null) {
            InstantiateFisrtsCheckPoint();
        } else {
            checkpoint.transform.position = new Vector3(0f, 0f, 0f);
        }

        curLevelSection = checkpoint;
        postcatObj = GameObject.Find("GameController").GetComponent<GameController>().postcatObj;
        Destroy(postcatObj);
        InstantiatePostcatNoRope();

        nextLevelSection = GameObject.Find("GameController").GetComponent<GameController>().nextLevelSection;
        if (nextLevelSection == null) {
            InstantiateSection(); 
        } else {
            nextLevelSection.transform.position = new Vector3(curLevelSection.GetComponent<Collider2D>().bounds.size.x + curLevelSection.transform.position.x, 0f, 0f);
            levelSectionAdded++;
        }*/


    /*Destroy(prevLevelSection);
    Destroy(curLevelSection);
    Destroy(nextLevelSection);

    checkpoint = GameObject.Find("GameController").GetComponent<GameController>().checkpoint;
    if (checkpoint != null)
        Destroy(checkpoint);
    postcatObj = GameObject.Find("GameController").GetComponent<GameController>().postcatObj;
    Destroy(postcatObj);

 
    //StartGame();
    PauseOff();
}*/
    /*public void Restart() {
        PauseOn();
        levelSectionAdded = 0;

        LSController.playerEnterSection -= this.NextSection;
        CheckPointController.playerRichCheckPoint -= this.NextLevel;

        prevLevelSection = GameObject.Find("GameController").GetComponent<GameController>().prevLevelSection;
        curLevelSection = GameObject.Find("GameController").GetComponent<GameController>().curLevelSection;
        nextLevelSection = GameObject.Find("GameController").GetComponent<GameController>().nextLevelSection;
        Destroy(prevLevelSection);
        Destroy(curLevelSection);
        Destroy(nextLevelSection);

        checkpoint = GameObject.Find("GameController").GetComponent<GameController>().checkpoint;
        if (checkpoint != null)
            Destroy(checkpoint);
        postcatObj = GameObject.Find("GameController").GetComponent<GameController>().postcatObj;
        Destroy(postcatObj); 

        
        StartGame();
        PauseOff();
    }*/





    private bool isPaused;
    public void PauseToggle() {
        if (isPaused) {
            PauseOff();
        } else {
            PauseOn();
        }
    }

    public void PauseOn() {
        Time.timeScale = 0.0f;
        isPaused = true;
        //menu.PauseOn();
    }
    public void PauseOff() {
        Time.timeScale = 1.0f;
        isPaused = false;
        //menu.PauseOff();
    }
   
    
    /*
    public void StageCleared(float fuel, float cargoHealth, float offset) {
        gameState.StoreFuel(fuel);
        gameState.fuel += cargoHealth;
        //StartCoroutine(ContinueGame(offset));
    }

    public void Win() {

        PauseOn();
        if (winUI != null)
            winUI.SetActive(true);
    }

    public void GotToMainMenu() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }*/

 /*public IEnumerator InstantiatePostCatWithPackage() {

     GameObject respawn = GameObject.FindGameObjectWithTag("Respawn");

     postcatObj = Instantiate(
         postcatPrefabWithCargo, 
         respawn.transform.position, 
         respawn.transform.rotation).gameObject;

     // Get fuel from station.
     Postcat postcat = postcatObj.GetComponentInChildren<Postcat>();
     postcat.fuel = 100 + gameState.Take();

     // Set target for main camera.
     Camera.main.GetComponent<CameraController>().target = postcatObj.transform.GetChild(0);

     yield return new WaitForSeconds(0.1f);

     // Push Postcat away from the station.
     foreach(Rigidbody2D rb in postcatObj.GetComponentsInChildren<Rigidbody2D>())
         rb.AddForce(Vector3.right * 10.0f, ForceMode2D.Impulse);
 }*/


 


 


}
