using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cargo : MonoBehaviour {

	public float maxhealth = 100.0f;
    public float currenthealth = 100.0f;
    public float damageScale = 100.0f;

    MainMenu menu;
    
    private void Awake() {
        currenthealth = maxhealth;
        menu = FindObjectOfType<MainMenu>();
        if(menu == null) {
            Debug.Log("sad");
        }
        menu.DisplayHealth(currenthealth, maxhealth);
    }


    void Update () {

        // if (health <= 0)
        // 	Die();
        menu.DisplayHealth(currenthealth, maxhealth);
    }
    public void Crash(float damage) {
        Debug.Log("happend");
        currenthealth -= damage;
        menu.DisplayHealth(currenthealth, maxhealth);
        FindObjectOfType<AudioManager>().Play("cargocrash");
    }


	/*void OnCollisionEnter2D(Collision2D other) {

        if( other.gameObject.tag == "Asteroid") {
            FindObjectOfType<AudioManager>().Play("cargocrash");
            float m = other.otherRigidbody.velocity.magnitude;
            currenthealth -= m * damageScale;


            Debug.Log("happend");
            menu.DisplayHealth(currenthealth, maxhealth);
        }
    }*/


	void Die() {
		Destroy(this.gameObject);
	}
}
