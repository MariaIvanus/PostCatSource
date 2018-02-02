using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Level section controller
public class LSController : MonoBehaviour {

    public delegate void OnCheckPointHandler();
    public static event OnCheckPointHandler playerEnterSection;
    private bool playerEnterThis = false;

    //Store to clean up
    private List<GameObject> polyObstacles;

    private void Awake() {

            polyObstacles = new List<GameObject>();

            InitializeObstacles(); 
        }

    public void InitializeObstacles() {

        foreach (PolyObstacle obstacle in transform.GetComponentsInChildren<PolyObstacle>()) {
            polyObstacles.Add(obstacle.InitializeObstacle());
        }

	}

    private void OnTriggerEnter2D(Collider2D collision) {

        if (playerEnterThis == false && collision.gameObject.CompareTag("Player")) {

            if (playerEnterSection != null) {
                playerEnterSection();
                playerEnterThis = true;
            }
        }

    }

    public void Destroy() {

        foreach (GameObject obstacle in polyObstacles) {
            Destroy(obstacle);
        }
        Destroy(gameObject);

    }

}
