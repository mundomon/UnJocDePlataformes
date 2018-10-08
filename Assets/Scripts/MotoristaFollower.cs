using UnityEngine;
using System.Collections;

public class MotoristaFollower : MonoBehaviour
{
    public Transform motorista;
    // Update is called once per frame
    void Update()
    {
        transform.position = motorista.position - Vector3.forward * 10;
    }

}
