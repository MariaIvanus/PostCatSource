using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Postcat : MonoBehaviour {

    //public float health = 100f; //from 0 to 1,
    //public Slider heathBar;

    public float startfuel; //= 100.0f;
    public float currentfuel;
    //public Slider fuelBar;

    public float speedScale = 10.0f;
	public float maxSpeed = 10.0f;
	public float consumption = 0.1f;
	public float jumpForce = 10.0f;

	public float yBound = 8.0f;
	public float reboundForce = 1.0f;
	public float clampVelScale = 1.0f;

	Rigidbody2D rb;
	Animator animator;
	Game gameController;


	void Awake() {
        rb = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
		gameController = GameObject
			.Find("GameController")
			.gameObject
			.GetComponent<Game>();

        startfuel = gameController.getMaxFuel();
        currentfuel = startfuel;

	}
	

	void FixedUpdate () {

		float h = Mathf.Max(0.0f, Input.GetAxis("Horizontal"));
		float v = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(h, v, 0.0f);

		animator.SetFloat("horizontal", h);
		animator.SetFloat("vertical", v);

        EngineSoundHandler();

        if (currentfuel > 0) {

            if (transform.position.y >= yBound) {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, clampVelScale);
                rb.AddForce(Vector3.up * -reboundForce);
            } else if (transform.position.y <= -yBound) {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, clampVelScale);
                rb.AddForce(Vector3.up * reboundForce);
            } else {
                rb.AddForce(movement * speedScale);
            }

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

            if (movement.magnitude > 0) {
                currentfuel -= consumption;
            }

        } else {
            gameController.ShowGameOver();
            //currentfuel = startfuel;
        }
        /*If no btn pressed - restore fuel
        if(h==0 && v ==0) {
            fuel += 0.01f * Time.deltaTime;
        }*/

        /* Надо логикой топлива еще нужно поработать отдельно, его нельзя подбирать, не восполняется со временем!(а должно) Геймовер не наступает.
         * И игрок должен все время двигаться, просто с минимальной скоростью, топливо для рывка и ускорения.

        */

    }

   

    public void ApplyDamage(float damage) {
		currentfuel -= damage;
    }

	public void Refuel(float fuelAmount) {
		currentfuel += fuelAmount;        
	}

	public void Jump() {
		rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
		animator.SetTrigger("Jump");
        FindObjectOfType<AudioManager>().Play("jump");
    }

    public void Crash() {
		rb.AddForce(Vector3.left * 5.0f, ForceMode2D.Impulse);
		animator.SetTrigger("Crash");
        FindObjectOfType<AudioManager>().Play("hitAsteroid");
    }

    public void Destroy() {
        Destroy(gameObject);
    }

    private void EngineSoundHandler() {
       // Звук двигателя
       if (Input.GetKeyDown(KeyCode.W)) {
           FindObjectOfType<AudioManager>().Play("engine");
       }
       if (Input.GetKeyDown(KeyCode.S)) {
           FindObjectOfType<AudioManager>().Play("engine");
       }
       if (Input.GetKeyDown(KeyCode.D)) {
           FindObjectOfType<AudioManager>().Play("engine");
       }
       if (Input.GetKeyDown(KeyCode.UpArrow)) {
           FindObjectOfType<AudioManager>().Play("engine");
       }
       if (Input.GetKeyDown(KeyCode.DownArrow)) {
           FindObjectOfType<AudioManager>().Play("engine");
       }
       if (Input.GetKeyDown(KeyCode.RightArrow)) {
           FindObjectOfType<AudioManager>().Play("engine");

       }
       if (Input.GetKeyUp(KeyCode.W)) {
           FindObjectOfType<AudioManager>().Stop("engine");
       }
       if (Input.GetKeyUp(KeyCode.S)) {
           FindObjectOfType<AudioManager>().Stop("engine");
       }
       if (Input.GetKeyUp(KeyCode.D)) {
           FindObjectOfType<AudioManager>().Stop("engine");
       }
       if (Input.GetKeyUp(KeyCode.UpArrow)) {
           FindObjectOfType<AudioManager>().Stop("engine");
       }
       if (Input.GetKeyUp(KeyCode.DownArrow)) {
           FindObjectOfType<AudioManager>().Stop("engine");
       }
       if (Input.GetKeyUp(KeyCode.RightArrow)) {
           FindObjectOfType<AudioManager>().Stop("engine");
       }
   }
}
