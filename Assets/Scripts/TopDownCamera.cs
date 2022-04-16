using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour {

    [SerializeField] Transform observable;
    [SerializeField] float aheadSpeed;
    [SerializeField] float followDamping;
    [SerializeField] float cameraHeight;

    Rigidbody _observableRigidbody;

    void Start () {
        _observableRigidbody = observable.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(observable == null)
        {
            return;
        }

        Vector3 targetPosition = observable.position + Vector3.up * cameraHeight + _observableRigidbody.velocity * aheadSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDamping * Time.deltaTime);


    }
}
