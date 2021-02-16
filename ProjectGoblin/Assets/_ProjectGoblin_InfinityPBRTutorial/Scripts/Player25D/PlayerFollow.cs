using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public GameObject followTargetObject;
    private Vector3 followPosition;
    // Update is called once per frame
    void LateUpdate()
    {
        followPosition = Vector3.Lerp
            (transform.position, followTargetObject.transform.position, Time.deltaTime * 10);
        followPosition = new Vector3(followPosition.x, transform.position.y, transform.position.z);
        transform.position = followPosition;
    }
}
