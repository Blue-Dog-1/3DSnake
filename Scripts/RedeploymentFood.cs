using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace snake3D
{
    public class RedeploymentFood : MonoBehaviour
    {
        void Start()
        {
            gameObject.transform.position = new Vector3(Random.Range(-4, 5), Random.Range(-4, 5), Random.Range(-5, 4));
        }
        public void OnTriggerEnter(Collider collider)
        {
            gameObject.transform.position = new Vector3(Random.Range(-4, 5), Random.Range(-4, 5), Random.Range(-5, 4));
        }
        

    }
}

    
