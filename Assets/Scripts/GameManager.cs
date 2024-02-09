using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    public int enemys = 0;
    public int lives = 3;

    public int totalEnemigos = 27;
    public int sumaPuntos;
    public int record = 0;
    float tiempoRestante = 60;
    float tiempoFinal = 10;

    public HUDManager HUD;
    public static GameManager Instance { get; private set; }

    public bool juego = false;
    public bool menu = true;
    public bool over = false;

    public bool ganar = false;
    public bool informacionfinalmostrada = false;

    //sonidos
    public AudioSource explosionJugador;
    public AudioSource explosionEnemigo;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
   

    public void AddEnemy()
    {
        explosionEnemigo.Play();
        enemys++;
        totalEnemigos--;
        HUD.MostrarPuntos(enemys);
        print(totalEnemigos + " + " + enemys);

    }

    //Pierde vidas
    public void LoseLife()
    {
        explosionJugador.Play();
        lives--;
        HUD.MostrarVidas(lives);
        print(lives);

        AddEnemy();
    }



    public void LoadJuego()
    {
        SceneManager.LoadScene("Level01");
        juego = true;
        menu = false;
        over = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
        menu = true;
        juego = false;
        over = false;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");
        over = true;
        menu = false;
        juego = false;
    }

    public void CerrarJuego()
    {
        Application.Quit();
        Debug.Log("Se salio del juego");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CerrarJuego();
        }

        if (menu)
        {
            HUD.MostrarRecord(record);

            if (Input.anyKeyDown)
            {
                LoadJuego();
                Debug.Log("Inicio del juego");
            }
        }

        if (over)
        {
            if (tiempoFinal > 0)
            {
                tiempoFinal -= Time.deltaTime;
                HUD.MostrarSegundosFinal((int)tiempoFinal);
                //print(tiempoFinal);
            }

            if (Input.anyKeyDown || tiempoFinal <= 0)
            {
                Menu();
                Debug.Log("Vuelta al menu");

                //reset de todo menos record
                informacionfinalmostrada = false;
                ganar = false;
                enemys = 0;
                lives = 3;
                sumaPuntos = 0;
                totalEnemigos = 27;
                tiempoRestante = 60;
            }

        }

        if (juego)
        {
            //Tiempo
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
                HUD.MostrarSegundos((int) tiempoRestante);
                //print(tiempoRestante);
            }
        }



        //Game Over + Calculo de puntos + reset de los datos

        if (lives <= 0 || totalEnemigos <= 0 || tiempoRestante <= 0)
        {
            if (informacionfinalmostrada == false)
            {
                GameOver();
                //CalculoFinal();
                informacionfinalmostrada = true;
            }
            //temporal
            CalculoFinal();
        }

    }

    public void CalculoFinal()
    {
        if (totalEnemigos <= 0)
        {
            sumaPuntos = enemys + (int)tiempoRestante;
            ganar = true;
        }
        else
        {
            sumaPuntos = enemys;
        }

        if (sumaPuntos <= record)
        {
            record = sumaPuntos;
            print("record: "+ record);
        }

        //Solo muestra la suma de los puntos y el tiempo si has ganado
        if (ganar)
        {
            HUD.MostrarPuntosOver(enemys);
            HUD.MostrarTiempoOver((int)tiempoRestante);
            //ganar = false;
        }

        HUD.MostrarPuntosFinales(sumaPuntos);
        print("total: " + sumaPuntos);
    }




}
