using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaySystem : MonoBehaviour
{

   
    private void OnTriggerEnter(Collider other)
    {
        WayPooling();
    }
    private void WayPooling()
    {
        Vector3 position = transform.parent.parent.position;
        position.z += 10 * transform.parent.lossyScale.z;
        transform.parent.parent.position = position;
        {
        }
    }

}


