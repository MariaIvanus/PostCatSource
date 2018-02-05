using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour {

    public bool initializeOnStart = true;
    //public Transform[] choices;
    public Transform prefab;

    void Start() {
        if (initializeOnStart)
            InitializeObstacle();
    }


    public GameObject InitializeObstacle() {
        int chance = 0;

        chance = Random.Range(0, 7);
        if (chance < 8) {
            GameObject obstacle = Instantiate(prefab, transform.position, transform.rotation).gameObject;
            obstacle.transform.parent = transform;
            return obstacle;
        } else {
            return null;
        }

    }

    void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
