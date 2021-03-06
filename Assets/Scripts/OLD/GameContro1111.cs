﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

	
	//public GameState gameState;

	//public int currentLevel;
	//public int startLevel;

	//private Postcat postcat;
	//private Transform[] levelSections;
	//private Vector3 lastSectionRoot;

    int level = 0;
    Text levelText;
    int levelSectionAdded;
    int levelSectionMax;
    public GameObject prevLevelSection;
    public GameObject curLevelSection;
    public GameObject nextLevelSection;
    public GameObject postcatObj;
    public GameObject checkpoint;
    public Transform postcatPrefab;
    public Transform postcatPrefabWithCargo;
    public Transform levelSectionPrefab;
    public Transform checkPointPrefab;

    public GameObject gameOverUI;
    public GameObject winUI;

    void Start() {
        
        //StartGame();

    }

    /*void StartGame() {
        levelText = GameObject.Find("levelText").GetComponent<Text>();
        InstantiateFisrtsCheckPoint();
        InstantiatePostcatNoRope();

        prevLevelSection = null;
        curLevelSection = checkpoint;
        if (curLevelSection == null) {
            Debug.Log("dgfdg");
        } else {
            Debug.Log("NotificationServices null");
        }
        //curLevelSection = GameObject.Find("che")
        SetupLevel();
    }

    void InstantiateFisrtsCheckPoint() {
        checkpoint = Instantiate(
            checkPointPrefab,
            new Vector3(0f, 0f, 0f),
            Quaternion.identity).gameObject;
    }

    void InstantiatePostcatNoRope() {
        postcatObj = Instantiate(
            postcatPrefab,
            checkpoint.transform.position,
            checkpoint.transform.rotation).gameObject;
        Camera.main.GetComponent<CameraController>().target = postcatObj.transform;
        postcatObj.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 20.0f, ForceMode2D.Impulse);
    }

    void SetupLevel() {


        LevelSpec temp_lv = new LevelSpec(level);
        levelSectionMax = temp_lv.maxSectionNumber;
        Debug.Log(levelSectionMax.ToString() + "max");
        levelText.text = level.ToString();
        Debug.Log(level.ToString() + "min");

        if (prevLevelSection != null) {
            NextSection();
            Debug.Log("next");
        } else {
            InstantiateSection();
            Debug.Log("инст");
        }

        
        LSController.playerEnterSection += this.NextSection;

    }

    void InstantiateSection() {
        float LevelPositionX = curLevelSection.GetComponent<Collider2D>().bounds.size.x + curLevelSection.transform.position.x;
        nextLevelSection = Instantiate(levelSectionPrefab, new Vector3(LevelPositionX, 0f, 0f), Quaternion.identity).gameObject;
        levelSectionAdded++;
        Debug.Log("levelSectionAdded" + levelSectionAdded);
    }

    void NextSection() {
        if (levelSectionAdded == levelSectionMax) {
            Debug.Log("WHATlevelSectionAdded" + levelSectionAdded);
            levelSectionAdded = 0;

            LSController.playerEnterSection -= this.NextSection;
            DrawCheckPoint();
        } else { 
            if (prevLevelSection != null) {
                //prevLevelSection.SetActive(false);
                //prevLevelSection.GetComponent<LSController>().Destroy();
                Destroy(prevLevelSection);
            }
            prevLevelSection = curLevelSection;
            curLevelSection = nextLevelSection;
            

            float LevelPositionX = curLevelSection.GetComponent<Collider2D>().bounds.size.x + curLevelSection.transform.position.x;
            nextLevelSection = Instantiate(levelSectionPrefab, new Vector3(LevelPositionX, 0f, 0f), Quaternion.identity).gameObject;
            levelSectionAdded++;
            Debug.Log("levelSectionAdded" + levelSectionAdded);
        }
    }
    public void NextLevel() {
       // if (prevLevelSection != null) {
            level++;
            Debug.Log("You cool" + level.ToString());
            CheckPointController.playerRichCheckPoint -= this.NextLevel;
        Destroy(prevLevelSection);
        //prevLevelSection.GetComponent<LSController>().Destroy();
        prevLevelSection = curLevelSection;
            curLevelSection = nextLevelSection;
            nextLevelSection = checkpoint;
            SetupLevel();
        //}
    }

    void DrawCheckPoint() {
        //Debug.Log(levelSectionMax.ToString() + " " + levelSectionAdded.ToString());
        float LevelPositionX = nextLevelSection.GetComponent<Collider2D>().bounds.size.x + nextLevelSection.transform.position.x;
        checkpoint = Instantiate(checkPointPrefab, new Vector3(LevelPositionX, 0f, 0), Quaternion.identity).gameObject;
      
        CheckPointController.playerRichCheckPoint += this.NextLevel;
       // Debug.Log("start listen playerRichCheckPoint ");
    }



    public void GameOver() {
        Debug.Log("Game Over");
        // TODO: Play sound
       // PauseOn();
        if (gameOverUI != null) { 
            gameOverUI.SetActive(true);
        }
        //PauseOff();
    }

    private void Destroy(GameObject toDestroy) {
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
    }
    public void Restart() {
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
        }

        
        /*Destroy(prevLevelSection);
        Destroy(curLevelSection);
        Destroy(nextLevelSection);

        checkpoint = GameObject.Find("GameController").GetComponent<GameController>().checkpoint;
        if (checkpoint != null)
            Destroy(checkpoint);
        postcatObj = GameObject.Find("GameController").GetComponent<GameController>().postcatObj;
        Destroy(postcatObj);
        
        if (gameOverUI != null) {
            gameOverUI.SetActive(false);
        }

        //StartGame();
        PauseOff();
    }
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

        if (gameOverUI != null) {
            gameOverUI.SetActive(false);
        }
        
        StartGame();
        PauseOff();
    }*/





    private bool isPaused;
    public void PauseOn() {
        Time.timeScale = 0.0f;
        isPaused = true;
    }
    public void PauseOff() {
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    // TODO: Пофиксить меню - займусь следующим
    /*public void GameOver() {
        // TODO: Play sound
        PauseOn();
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }
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
}
//TODELETE
/* void Awake() {
     levelSections = Resources.LoadAll<Transform>("Prefab/LevelSections");
 }
 void Start() {
     StartCoroutine( StartGame() );
 }
 public IEnumerator StartGame() {

     currentLevel = 3 + startLevel; // 3 is an build index offset.

     var loadFuture = SceneManager
         .LoadSceneAsync(currentLevel, LoadSceneMode.Additive);
     yield return new WaitUntil(() => loadFuture.isDone);

     foreach(GameObject levelSection in GameObject.FindGameObjectsWithTag("LevelSection"))
         levelSection.GetComponent<LSController>().InitializeObstacles();

     var loadCheckpointFuture = SceneManager
         .LoadSceneAsync("Checkpoint", LoadSceneMode.Additive);
     yield return new WaitUntil(() => loadCheckpointFuture.isDone);

     GameObject respawn = GameObject.FindGameObjectWithTag("Respawn");

     yield return StartCoroutine(InstantiatePostcatNoRope());

     yield return null;
 }
 IEnumerator InstantiatePostcatNoRope() {

     GameObject respawn = GameObject.FindGameObjectWithTag("Respawn");

     postcatObj = Instantiate(
         postcatPrefab, 
         respawn.transform.position, 
         respawn.transform.rotation).gameObject;

     Camera.main.GetComponent<CameraController>().target = postcatObj.transform;

     // Push Postcat away from the station.
     postcatObj.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 20.0f, ForceMode2D.Impulse);
     yield return null;
 }


 public IEnumerator InstantiatePostCatWithPackage() {

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
 }


 


 public IEnumerator ContinueGame(float offset) {

     var unloadFuture = SceneManager.UnloadSceneAsync(currentLevel);
     yield return new WaitUntil(() => unloadFuture.isDone);

     currentLevel++;

     if (currentLevel == 6)
         Win();
     else {

         var loadFuture = SceneManager
             .LoadSceneAsync(currentLevel, LoadSceneMode.Additive);
         yield return new WaitUntil(() => loadFuture.isDone);

         Transform levelRoot = GameObject.FindWithTag("LevelRoot").transform;
         levelRoot.transform.position = Vector3.right * (offset + 5.0f);

         foreach(GameObject levelSection in GameObject.FindGameObjectsWithTag("LevelSection"))
             levelSection.GetComponent<LSController>().InitializeObstacles();

         yield return new WaitForSeconds(1.0f);

         // For the sake of safe spawning
         GameObject station = GameObject.FindGameObjectWithTag("Station");
         station.transform.Find("Finish").gameObject.SetActive(false);

         yield return StartCoroutine(InstantiatePostCatWithPackage());
     }

 }

}*/
