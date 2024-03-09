using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadosControler : MonoBehaviour
{
    public enum TipoDado
    {
        Azul,
        Rojo
    }

    public TipoDado DadoType;
    private float ejeX;
    private float ejeY;
    private float ejeZ;
    private Vector3 posicionInicial;
    private Rigidbody rbDado;
    private bool dadoMovimendo = true;
    public controlCara[] lados = new controlCara[6];
    private int valorDado;
    private int ladoOculto;

    // Start is called before the first frame update
    void Start()
    {
        
        rbDado = GetComponent<Rigidbody>();
        posicionInicial = transform.position;
        PrepararDado();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){//lanzar dado
            PrepararDado();
        }

        if(rbDado.IsSleeping() && dadoMovimendo)//comprobar que el objecto se mueve y 
        {
            dadoMovimendo = false;
            ladoOculto = comprobarLados();
            valorDado = 7 - ladoOculto;
            if(valorDado == 7)
            {
                rbDado.AddForce(3f, 0f,0f, ForceMode.Impulse);
                dadoMovimendo = true;
            }
        }

        if(!dadoMovimendo)
        {
            switch(DadoType){
                case TipoDado.Azul:
                    ControlMenu.instancia.SetAzul(valorDado);
                    break;
                case TipoDado.Rojo:
                    ControlMenu.instancia.SetRojo(valorDado);
                    break;
                default:
                    break;
            }
            ControlMenu.instancia.ActualizarNumero();
        }
    }

    void PrepararDado()
    {
        transform.position = posicionInicial;
        rbDado.velocity = new Vector3(0f,0f,0f);
        ControlMenu.instancia.LimpiarValores();
        Debug.Log("Limpiar pantalla");
        dadoMovimendo = true;
        ejeX = Random.Range(0f, 271f);
        ejeY = Random.Range(0f, 271f);
        ejeZ = Random.Range(0f, 271f);
        transform.Rotate(ejeX,ejeY,ejeZ);
        ejeX = Random.Range(-3f, 3f);
        ejeY = Random.Range(-2f, 0f);
        ejeZ = Random.Range(-3f, 3f);
        rbDado.AddForce(ejeX,ejeY,ejeZ,ForceMode.Impulse);
    }

    int comprobarLados()
    {
        int valor = 0;
        for(int i = 0; i<6; i++)
        {
            if(lados[i].compruebaSuelo())
            {
                valor = i+1;
            }
        }
        return valor;
    }
}