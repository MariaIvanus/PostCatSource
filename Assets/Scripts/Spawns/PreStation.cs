using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreStation : MonoBehaviour {

    //start pulling Player into station
    void OnTriggerEnter2D(Collider2D col) {
        GameObject obj = col.gameObject;
       // || obj.CompareTag("Cargo")
        if (obj.CompareTag("Player") ) {

            FindObjectOfType<AudioManager>().Stop("engine");

            //TODO: fix precat scene
            //obj.GetComponent<Postcat>().MoveAllowed = false;
            //obj.GetComponentInChildren<Animator>().SetBool("Station", true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision) {
      // || obj.CompareTag("Cargo"))
        GameObject obj = collision.gameObject;
        if (this.transform.parent.gameObject.name != "StartPoint" && (obj.CompareTag("Player"))) {
            Debug.Log("cat stop2");
            //TODO: fix delay loading;
            //FindObjectOfType<Game>().LoadNextLevel();
            FindObjectOfType<Game>().ShowWinUI();
        }
    }

}
