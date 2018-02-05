using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullInStationBehaviour : StateMachineBehaviour {



    private float smoothTime = 1F;
    private Vector3 velocity = Vector3.zero;
    private float pullForce = 24.0f;
    private float pullDistance = 20f;
    Vector3 targetPosition;
    GameObject postcat;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        postcat = animator.gameObject.transform.parent.gameObject;
        targetPosition = new Vector3(postcat.transform.position.x+2, 1f, 0f);

    }

   
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        postcat.GetComponent<Postcat>().MoveAllowed = true;
        postcat.GetComponentInChildren<Animator>().SetBool("Station", false);

        /* if (postcat != null) {
             postcat.transform.position = Vector3.SmoothDamp(postcat.transform.position, targetPosition, ref velocity, smoothTime);

             float distance = Vector3.Distance(postcat.transform.position, targetPosition);
             if (distance <= 1) {
                 Debug.Log("cat stop");
                 postcat.GetComponentInChildren<Animator>().SetBool("Station", false);
                 postcat.GetComponent<Postcat>().MoveAllowed = true;
                 targetPosition = new Vector3(postcat.transform.position.x+pullDistance, postcat.transform.position.y, 0.0f);
                 Vector3 dir = targetPosition - postcat.transform.position;
                 postcat.GetComponent<Rigidbody2D>().AddForce(dir * pullForce);


             }
         }*/

    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
