using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titilador : MonoBehaviour
{
    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        myLight.intensity =  0.8f;
    }
}
