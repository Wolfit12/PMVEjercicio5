using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TMPro.TMP_Text puntos;
    public TMPro.TMP_Text Life;

    public SpriteRenderer spriteRenderer;
    public Sprite spriteVidas3;
    public Sprite spriteVidas2;
    public Sprite spriteVidas1;
    public Sprite spriteVidas0;

    public TMPro.TMP_Text puntosTotales;
    public TMPro.TMP_Text records;
    public TMPro.TMP_Text tiempoOver;
    public TMPro.TMP_Text puntosOver;

    public TMPro.TMP_Text segundosOver;
    public TMPro.TMP_Text segundosJuego;


    private void Start()
    {
        GameManager.Instance.HUD = this;
    }

    public void MostrarPuntos(int enemys)
    {
        puntos.text = enemys.ToString();
    }

    
    public void MostrarVidas(int lives)
    {
        Life.text = lives.ToString();

        if (lives == 3)
        {
            spriteRenderer.sprite = spriteVidas3;
        }

        if (lives == 2)
        {
            spriteRenderer.sprite = spriteVidas2;
        }

        if (lives == 1)
        {
            spriteRenderer.sprite = spriteVidas1;
        }

        if (lives == 3)
        {
            spriteRenderer.sprite = spriteVidas0;
        }
    }


    public void MostrarPuntosFinales(int sumaPuntos)
    {
        puntosTotales.text = "Puntos Totales: " + sumaPuntos.ToString();
    }

    public void MostrarTiempoOver(int tiempoRestante)
    {
        tiempoOver.gameObject.SetActive(true);
        tiempoOver.text = "+" + tiempoRestante.ToString() + " Tiempo";
    }

    public void MostrarPuntosOver(int enemys)
    {;
        puntosOver.gameObject.SetActive(true);
        puntosOver.text = "+" + enemys.ToString() + " Puntos";
    }

    public void MostrarRecord(int record)
    {
        records.text = "Record: " + record.ToString();
    }

    //Temporizadores
    public void MostrarSegundosFinal(int tiempoFinal)
    {
        segundosOver.text = "Volviendo al menu: " + tiempoFinal.ToString() + "s";
    }

    public void MostrarSegundos(int tiempoRestante)
    {
        segundosJuego.text = tiempoRestante.ToString() + "s";
    }


}
