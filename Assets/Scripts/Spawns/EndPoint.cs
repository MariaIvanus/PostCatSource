using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {

    public GameObject EndPointObject;
    public Transform EndPointPrefab;
    public BoxCollider2D EndPointCollider;


    Vector2 size;
    Vector2 offset;
    
    public void createEndPoint() {
        EndPointObject = Instantiate(EndPointPrefab, new Vector3(-100f,0f,0f), Quaternion.identity).gameObject;
        EndPointObject.transform.parent = transform;

        EndPointCollider = gameObject.GetComponent<BoxCollider2D>();
        EndPointCollider.size = EndPointObject.GetComponent<BoxCollider2D>().size;
        EndPointCollider.offset = new Vector2(EndPointObject.GetComponent<BoxCollider2D>().size.x/2, 0f);

        //EndPointCollider = transform.gameObject.AddComponent<BoxCollider2D>();
        // EndPointCollider.offset = new Vector2(14.5f,0f);
        // EndPointCollider.size = new Vector2(40.0f,40.0f);
        /*EndPointColliders = transform.gameObject.GetComponentsInChildren<BoxCollider2D>();
        for(int i = 0; i<EndPointColliders.Length; i++) {
            this.size += EndPointColliders[i].size;
            this.offset += EndPointColliders[i].size;
        }*/

        //EndPointCollider.size = EndPointObject.GetComponent<BoxCollider2D>().size +this.GetComponentInChildren<BoxCollider2D>();
        //EndPointCollider.offset = new Vector2(EndPointObject.GetComponent<BoxCollider2D>().size.x/2, 0f);

    }


    public void SetPosition() {
        EndPointObject.transform.position = new Vector3(transform.position.x + EndPointCollider.size.x/2, transform.position.y, transform.position.z);
        EndPointCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
       
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Level ends");
            //Debug.Log("Its Player");
            
        }
   
    }
   

    public void CleanUp() {
        Destroy(EndPointObject);
    }

    


   



}
