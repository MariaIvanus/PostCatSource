using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour {




    //Animation pulling cat inside - after that show WIN or continue
    /*public float pullForce = 30.0f;

    void OnTriggerEnter2D(Collider2D col) {
        GameObject obj = col.gameObject;

         if (obj.CompareTag("Player") || obj.CompareTag("Cargo")) {
            Debug.Log("cat pull");
            Postcat postcat = obj.GetComponent<Postcat>();

            postcat.GetComponent<Postcat>().MoveAllowed = true;
            obj.GetComponentInChildren<Animator>().SetBool("Station", false);

            // Suck Player into Station
            Vector3 dir = transform.position - obj.transform.position;
             obj.GetComponent<Rigidbody2D>().AddForce(dir * pullForce);

         }

    }*/

}
