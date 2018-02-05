using UnityEngine;

public class RopeSegment : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        this.transform.parent = FindObjectOfType<Rope>().transform;
	}
	

    void OnDrawGizmos() {
        Gizmos.color = Color.gray;

        Rigidbody2D target = gameObject.GetComponent<HingeJoint2D>().connectedBody;

        if (target != null)
            Gizmos.DrawLine(transform.position, target.transform.position);
    }
}
