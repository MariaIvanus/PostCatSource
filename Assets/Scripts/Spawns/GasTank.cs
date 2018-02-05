﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTank : MonoBehaviour {

	public float fuel = 10.0f;

	private Animator animator;


	void Awake() {
		animator = GetComponent<Animator>();
	}


	void OnTriggerEnter2D(Collider2D collision) {
		
		GameObject obj = collision.gameObject;
		
		if (obj.CompareTag("Player")) {
			
			Postcat postcat = obj.GetComponent<Postcat>();
			
			postcat.Refuel(fuel);

			animator.SetTrigger("Take");

			
		}

	}

  /*  private void OnTriggerExit2D(Collider2D collision) {

        GameObject obj = collision.gameObject;

        if (obj.CompareTag("Player")) {

            Debug.Log("cool");
           // Destroy(animator.gameObject);
        }
    }*/

    

}
