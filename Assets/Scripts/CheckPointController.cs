using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour {

    //стартовый для уровня чекпоинт
    //public Transform prevCheckpoint;


    public delegate void OnCheckPointHandler();
    public static event OnCheckPointHandler playerRichCheckPoint;

   // GameObject gameController;

    private void Start() {
        //gameController.
    }
    public void OnTriggerEnter2D(Collider2D collision) {

        //Debug.Log("chek");
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("rich somehow");
            //GameObject.Find("GameController").GetComponent<GameController>().NextLevel();
            if (playerRichCheckPoint != null) {
                playerRichCheckPoint();
            }
        }
    }
    public void Destroy() {
        Destroy(gameObject);
    }
}