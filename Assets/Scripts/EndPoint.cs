using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {

    public GameObject EndPointObject;
    public Transform EndPointPrefab;
    public BoxCollider2D EndPointCollider;


    void Start() {
        //createEndPoint();
    }
    
    public void createEndPoint() {
        EndPointObject = Instantiate(EndPointPrefab, new Vector3(-100f,0f,0f), Quaternion.identity).gameObject;
        EndPointObject.transform.parent = transform;

        //EndPointCollider = EndPointObject.GetComponent<BoxCollider2D>();
        EndPointCollider.size = EndPointObject.GetComponent<BoxCollider2D>().size;
        EndPointCollider.offset = new Vector2(EndPointObject.GetComponent<BoxCollider2D>().size.x/2, 0f);
        
    }


    public void SetPosition() {
        EndPointObject.transform.position = new Vector3(transform.position.x + EndPointCollider.size.x/2, transform.position.y, transform.position.z);
        EndPointCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
       
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Level ends");
            //Debug.Log("Its Player");
            FindObjectOfType<Game>().ShowWinUI();
        }
        
    }
    public void CleanUp() {
        Destroy(EndPointObject);
    }
}
