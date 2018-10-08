using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotoControl : MonoBehaviour
{
    public float velocidadRotacion = 50;
    public float velocidadLineal = 10;
    public Transform ruedaTrasera;
    public event eliminadoDelegate eliminado;
    public event triggerDelegate finalNivel;
    public event interruptorDelegate interruptorCaja;
    public delegate void interruptorDelegate();
    public delegate void triggerDelegate();
    public delegate void eliminadoDelegate();
    private List<KeyCode> acciones = new List<KeyCode>();

    private Rigidbody2D rigibody;
    private float radioRueda;

    private void Start()
    {
        rigibody = GetComponent<Rigidbody2D>();
        radioRueda = GetComponent<CircleCollider2D>().radius + 0.1f;

    }
    private void Update()
    {
        ActualizarAccionTeclado(KeyCode.LeftArrow);
        ActualizarAccionTeclado(KeyCode.RightArrow);
        ActualizarAccionTeclado(KeyCode.UpArrow);
        ActualizarAccionTeclado(KeyCode.DownArrow);

        if (acciones.Contains(KeyCode.LeftArrow))
        {
            MueveIzquierda();
        }
        if (acciones.Contains(KeyCode.RightArrow))
        {
            MueveDerecha();
        }
        if (acciones.Contains(KeyCode.UpArrow))
        {
            RotaIzquierda();
        }
        if (acciones.Contains(KeyCode.DownArrow))
        {
            RotaDerecha();
        }
    }

    #region Movimiento
    public void MueveDerecha()
    {
        if (TocaElSuelo()) rigibody.position += new Vector2(transform.right.x * velocidadLineal, transform.right.y * velocidadLineal) * Time.deltaTime;
    }
    public void MueveIzquierda()
    {
        if (TocaElSuelo()) rigibody.position -= new Vector2(transform.right.x * velocidadLineal, transform.right.y * velocidadLineal) * Time.deltaTime;
    }
    public void RotaDerecha()
    {
        rigibody.MoveRotation(rigibody.rotation - velocidadRotacion * Time.deltaTime);
    }
    public void RotaIzquierda()
    {
        rigibody.MoveRotation(rigibody.rotation + velocidadRotacion * Time.deltaTime);
    }
    #endregion




    #region Pulsaciones teclas
    //cunado pulsamos y dejamos de pulsar uno de los botones de flecha
    private void ActualizarAccionTeclado(KeyCode code)
    {
        if (Input.GetKeyDown(code))
        {
            ActualizarAccionDown(code);
        }
        if (Input.GetKeyUp(code))
        {
            ActualizarAccionUp(code);
        }
    }
    private void ActualizarAccionDown(KeyCode code)
    {
        if (!acciones.Contains(code)){
            acciones.Add(code);
        } 
    }
    private void ActualizarAccionUp(KeyCode code)
    {
        //Debug.Log("<color=red>Actualizacion UP teclado:</color>" + code);
        if (acciones.Contains(code)) acciones.Remove(code);
    }
    public void MueveDerechaDown()
    {
        //Debug.Log("<color=red>KEYDOWN:</color> izquierda");
        ActualizarAccionDown(KeyCode.RightArrow);
    }
    public void MueveDerechaUp(){

        //Debug.Log("<color=red>KEYUP:</color> izquierda");
        ActualizarAccionUp(KeyCode.RightArrow);
    }
    public void MueveIzquierdaDown()
    {
        ActualizarAccionDown(KeyCode.LeftArrow);
    }
    public void MueveIzquierdaUp()
    {
        ActualizarAccionUp(KeyCode.LeftArrow);
    }
    public void RotaDerechaDown()
    {
        ActualizarAccionDown(KeyCode.DownArrow);
    }
    public void RotaDerechaUp()
    {
        ActualizarAccionUp(KeyCode.DownArrow);
    }
    public void RotaIzquierdaDown()
    {
        ActualizarAccionDown(KeyCode.UpArrow);
    }
    public void RotaIzquierdaUp()
    {
        ActualizarAccionUp(KeyCode.UpArrow);
    }
    #endregion


    #region Colisiones
    bool TocaElSuelo()
    {
        if (Physics2D.OverlapCircleAll(ruedaTrasera.position, radioRueda).Length > 0)
        {
            //Debug.Log("<color=red>TOCA EL SUELO:</color> TRUE");
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "InterruptorPendulo"){
            if(interruptorCaja!= null){
                interruptorCaja();
            }
        }else if(other.tag == "Meta"){
            if (finalNivel != null)
            {
                finalNivel();
            }
        }else if (other.tag == "Nada"){

        }else{
            if (eliminado != null)
            {
                eliminado();
            }
        }
    }

    #endregion

}
