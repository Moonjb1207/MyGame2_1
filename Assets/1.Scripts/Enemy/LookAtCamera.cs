using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDir = (Camera.main.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(lookDir.z, lookDir.y) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 90, Vector3.right);
        transform.rotation = q;
    }
}
