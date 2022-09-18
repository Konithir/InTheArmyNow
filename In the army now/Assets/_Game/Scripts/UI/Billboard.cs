using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    private void LateUpdate()
    {
            transform.LookAt(transform.position + GameManager.Singleton.MainCamera.transform.rotation * Vector3.forward,
                GameManager.Singleton.MainCamera.transform.rotation * Vector3.up);
    }
}
