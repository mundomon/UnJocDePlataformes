using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    public MotoControl motorista;
    public GameObject marcadorTiempo;
    public GameObject pendulo;
    public Text timingText;
    public Text finalTimeText;
    public Text recordText;
    public Button startButton;
    public Button restartButton;
    private int segundosParaEmpezar = 3;
    private Text mainText;
    private float tiempoInicial;
    private float tiempoRecord;

    // Use this for initialization
    void Start()
    {
        motorista.eliminado += Reinciar;
        motorista.finalNivel += FinalNivel;
        motorista.interruptorCaja += Pendulo;
        motorista.enabled = false;
        restartButton.enabled = false;
        marcadorTiempo.SetActive(false);
        restartButton.gameObject.SetActive(false);
        finalTimeText.text = "";

        tiempoRecord = PlayerPrefs.GetFloat("tiempo record", 0);
        if (tiempoRecord > 0) recordText.text = "Record: " 
                + tiempoRecord.ToString("##.##");

        mainText = startButton.GetComponentInChildren<Text>();
    }
    private void Pendulo(){
        pendulo.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    private void Reinciar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void Restart(){
        Reinciar();
    }

    public void StartGame(){
        startButton.enabled = false;
        mainText.text = "" + segundosParaEmpezar;
        PlayerPrefs.SetFloat("tiempo record", 0);
        InvokeRepeating("CuentaAtras", 1, 1);
    }

    private void CuentaAtras(){
        segundosParaEmpezar--;
        if (segundosParaEmpezar <= 0)
        {
            CancelInvoke();
            JuegoEmpezado();
        }
        else mainText.text = "" + segundosParaEmpezar;
    }

    private void JuegoEmpezado(){
        mainText.text = "";
        marcadorTiempo.SetActive(true);
        motorista.enabled = true;
        tiempoInicial = Time.time;
        //if (tiempoRecord > 0) recordText.enabled = true;
    }

    private void FinalNivel()
    {
        motorista.enabled = false;
        marcadorTiempo.SetActive(false);
        restartButton.gameObject.SetActive(true);
        restartButton.enabled = true;
        mainText.text = "Final!";
        finalTimeText.text = (Time.time - tiempoInicial).ToString("##.##");
        //comprobamos si bate el record 
        if(tiempoRecord>0){
            if ((Time.time - tiempoInicial) < tiempoRecord)
            {
                mainText.text = "RECORD!!!";
                PlayerPrefs.SetFloat("tiempo record", (Time.time - tiempoInicial));
            }
        }
        else{
            mainText.text = "RECORD!!!";
            PlayerPrefs.SetFloat("tiempo record", (Time.time - tiempoInicial));
        }

        //PlayerPrefs.SetFloat("tiempo record", (Time.time - tiempoInicial));
    }
    private void Update()
    {
        if(motorista.enabled){
            timingText.text = (Time.time - tiempoInicial).ToString("##.##");
        }
    }
}
