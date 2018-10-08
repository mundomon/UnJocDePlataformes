using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubeControl : MonoBehaviour {
    public Transform nube;
    private float bordeIzquierdo = -1;
    private float bordeDerecho = 120;
    private float velocidadNube = 0.3f;
	
	// Update is called once per frame
    private void Update () {
        if (FinDePantalla())
        { 
            nube.transform.position = new Vector3(bordeDerecho, nube.position.y, 2.0f);
        }
        nube.position -= Vector3.right * velocidadNube * Time.deltaTime;
    }
    public void mueveHorizontal(){
        //nube.position += new Vector2(transform.right.x + 10, transform.right.y * 10) * Time.deltaTime;
    }

    bool FinDePantalla()
    {
        return (nube.position.x <= bordeIzquierdo);
    }
}
