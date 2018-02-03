using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour {

    public GameObject StartPointObject;
    public Transform StartPointPrefab;
    public BoxCollider2D StartPointCollider;


    public void createStartPoint() {
        StartPointObject = Instantiate(StartPointPrefab, Vector3.zero, Quaternion.identity).gameObject;
        StartPointObject.transform.parent = transform;

        //StartPointCollider = gameObject.Get<BoxCollider2D>();
        StartPointCollider.size = StartPointObject.GetComponent<BoxCollider2D>().size;
        StartPointCollider.offset = new Vector2(StartPointObject.GetComponent<BoxCollider2D>().size.x/2, 0f);
        StartPointCollider.isTrigger = true;
    }


    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Level starts");
            //Debug.Log("Its Player");
        }
    }

    public void CleanUp() {
        Destroy(StartPointObject);
    }
}
