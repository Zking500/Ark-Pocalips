using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ControlMenu : MonoBehaviour
{
    public static ControlMenu instancia;
    [SerializeField] private TMP_Text dado1;
    [SerializeField] private TMP_Text dado2;
    [SerializeField] private TMP_Text valorTotal;
    [SerializeField] private TMP_Text turnoJ;
    private bool turno = false;
    private int valorD1 = 0;
    private int valorD2 = 0;
    private int TotalValor = 0;
    private int index = 0;

    public PlayerController players;

    private void OnEnable(){
        if(instancia == null)
        {
            instancia = this;
        }
    }

    public void ActualizarNumero(){
        if(valorD1 != 0 && valorD2 != 0)
        {
            TotalValor = valorD1 + valorD2;
            dado1.text = "Dado Rojo : " + valorD1.ToString();
            dado2.text = "Dado Azul : " + valorD2.ToString();
            valorTotal.text = "Valor Total : " + TotalValor.ToString();
            if(turno){
                Movimientos(TotalValor);
            }
        }
    }
    public void SetRojo(int Rojo)
    {
        valorD1 = Rojo;
    }

    public void SetAzul(int Azul)
    {
        valorD2 = Azul;
    }

    public void LimpiarValores()
    {
        valorD1 = 0;
        valorD2 = 0;
        dado1.text = "Dado Rojo : " + valorD1.ToString();
        dado2.text = "Dado Azul : " + valorD2.ToString();
        valorTotal.text = "Valor Total : " + (valorD1 + valorD2).ToString();
        turno = true;
    }

    public void Movimientos(int TotalValor)
    {
        players.MovingbyNum(TotalValor);
        turno = false;
    }

    private void cambiarTurno(){
        //index = (index + 1) % players.Length;
    }
}
