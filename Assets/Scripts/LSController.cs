using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Level section controller
public class LSController : MonoBehaviour {

    public delegate void OnCheckPointHandler();
    public static event OnCheckPointHandler playerEnterSection;
    private bool playerEnterThis = false;
    //Хранить астероиды в массиве! Как добавить элемент в массив? Может список?
    private Transform[] polyObstacles;

    public void InitializeObstacles() {
		foreach(PolyObstacle obstacle in transform.GetComponentsInChildren<PolyObstacle>()) {
			obstacle.InitializeObstacle();
            //polyObstacles.shi

        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("chek");
        if (playerEnterThis == false && collision.gameObject.CompareTag("Player")) {
            Debug.Log("enter");
            if (playerEnterSection != null) {
                playerEnterSection();
                playerEnterThis = true;
            }
        }
    }
    private void OnDisable() {
        foreach (PolyObstacle obstacle in transform.GetComponentsInChildren<PolyObstacle>()) {
            Destroy(obstacle);
        }
        Destroy(gameObject);
    }
    private void onDestroy() {
       
    }

}
