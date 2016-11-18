using UnityEngine;
using System.Collections;

public class DualCamera : MonoBehaviour {

    [SerializeField]
    Transform target1, target2;

    [SerializeField]
    float minimunDistance;

    float distance;
    float averageDist;
	
	// Update is called once per frame
	void Update () {
        distance = (target2.position.z - target1.position.z) * 2;
        averageDist = (target1.position.z + target2.position.z) / 2;

        transform.position = new Vector3(distance < minimunDistance ? 
            distance : minimunDistance,
            transform.position.y, 
            averageDist);
	}
}
