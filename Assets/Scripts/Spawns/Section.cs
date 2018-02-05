using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour {




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

    /*public void Destroy() {

        foreach (GameObject obstacle in polyObstacles) {
            Destroy(obstacle);
        }
        Destroy(gameObject);

    }*/


    void OnDrawGizmos() {

        Gizmos.color = Color.gray;

        BoxCollider2D collider = this.GetComponent<BoxCollider2D>();
        Vector3 size = new Vector3(collider.bounds.size.x, collider.bounds.size.y, 1f);

        Gizmos.DrawWireCube(transform.position, size);
    }

    /*
     public GameObject[] PolyObstacleObjects;
     public Transform[] PolyObstaclePrefabs;
     public void CreateSections() {

        levelSectionsObjects = new GameObject[totalSection];

        for (int i = 0; i < totalSection; i++) {
            int prefabIndex = ChooseSection();
            levelSectionsObjects[i] = Instantiate(levelSectionsPrefabs[prefabIndex], this.transform.position, Quaternion.identity).gameObject;
            levelSectionsObjects[i].transform.parent = gameObject.transform;
        }

        PlaceSections();
    }
    public void PlaceSections() {
        for (int i = 1; i < levelSectionsObjects.Length; i++) {
            float sectionPositionX = levelSectionsObjects[i - 1].transform.position.x + levelSectionsObjects[i - 1].GetComponent<BoxCollider2D>().bounds.size.x;
            levelSectionsObjects[i].transform.position = new Vector3(sectionPositionX, 0f, 0f);
        }
    }

    private int ChooseSection() {
        //от 0 до длины - рандомное число.... - индекс префаба в массиве..
        int prefabIndex = Random.Range(0, levelSectionsPrefabs.Length - 1);
        return prefabIndex;
    }

    public float SectionsEndPostionX() {

        float totalWidth = levelSectionsObjects[0].transform.position.x;

        for (int i = 0; i < levelSectionsObjects.Length; i++) {
            totalWidth += levelSectionsObjects[i].GetComponent<BoxCollider2D>().bounds.size.x;
        }

        return totalWidth;
    }

    public void CleanUp() {
        for (int i = 0; i < levelSectionsObjects.Length; i++) {
            Destroy(levelSectionsObjects[i]);
        }
    }*/



    /*
     * private List<GameObject> polyObstacles;

    private void Awake() {

            polyObstacles = new List<GameObject>();

            InitializeObstacles(); 
        }

    public void InitializeObstacles() {

        foreach (PolyObstacle obstacle in transform.GetComponentsInChildren<PolyObstacle>()) {
            //polyObstacles.Add(obstacle.InitializeObstacle());
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

    }*/
}
