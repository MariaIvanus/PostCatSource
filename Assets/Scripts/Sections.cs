using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sections : MonoBehaviour {

    public GameObject[] levelSectionsObjects;
    public Transform[] levelSectionsPrefabs;



    public void CreateSections(int totalSection) {

        levelSectionsObjects = new GameObject [totalSection];

        for (int i = 0; i < totalSection; i++) {
            int prefabIndex = ChooseSection();
            levelSectionsObjects[i] = Instantiate(levelSectionsPrefabs[prefabIndex], this.transform.position, Quaternion.identity).gameObject;
            levelSectionsObjects[i].transform.parent = gameObject.transform;
        }

        PlaceSections();
    }

    public void PlaceSections() {

        float firstElemPosition = transform.position.x + levelSectionsObjects[0].GetComponent<BoxCollider2D>().bounds.size.x/2;

        levelSectionsObjects[0].transform.position = new Vector3(firstElemPosition, 0f, 0f);

        for (int i = 1; i < levelSectionsObjects.Length; i++) {
            float sectionPositionX = levelSectionsObjects[i-1].transform.position.x 
                + levelSectionsObjects[i - 1].GetComponent<BoxCollider2D>().bounds.size.x/2
                + levelSectionsObjects[i].GetComponent<BoxCollider2D>().bounds.size.x/2;
            levelSectionsObjects[i].transform.position = new Vector3(sectionPositionX, 0f, 0f);
        }
    }

    private int ChooseSection() {
        int prefabIndex = Random.Range(0, levelSectionsPrefabs.Length-1);
        return prefabIndex;
    }

    public float SectionsEndPostionX() {

        float totalWidth = levelSectionsObjects[0].transform.position.x - levelSectionsObjects[0].GetComponent<BoxCollider2D>().bounds.size.x/2;

        for (int i = 0; i < levelSectionsObjects.Length; i++) {
            totalWidth += levelSectionsObjects[i].GetComponent<BoxCollider2D>().bounds.size.x;
        }

        return totalWidth;
    }

    public void CleanUp() {
        for(int i = 0; i<levelSectionsObjects.Length;i++) {
            Destroy(levelSectionsObjects[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log("section entered");
    }

    void OnDrawGizmos() {

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
