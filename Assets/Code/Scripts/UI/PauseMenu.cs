using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para cambiar de escenas

public class PauseMenu : MonoBehaviour
{
    //Variables para saber las escenas a las que queremos ir desde este men�
    public string levelSelect, mainMenu;
    //Variable para conocer cuando el juego est� pausado o no
    public bool isPaused;
    //Referencia al GO del men� de pausa
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        //Si pulsamos el bot�n de Intro
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("ButtonStart"))
            //Pausamos el juego
            PauseUnpause();
        if (isPaused)
            Input.GetButtonDown("ButtonA");
    }

    //M�todo para pausar o continuar el juego
    public void PauseUnpause()
    {
        //Si la variable pausa es verdadera (el juego est� pausado)
        if (isPaused)
        {
            //Entonces el juego continuar�
            isPaused = false;
            //Desactivamos el panel de pausa
            pauseMenu.SetActive(false);
            //Reanudamos el tiempo de juego
            Time.timeScale = 1f;
        }
        //Si el juego no estaba pausado
        else
        {
            //Entonces el juego se pondr� en pausa
            isPaused = true;
            //Activamos el panel de pausa
            pauseMenu.SetActive(true);
            //Pausamos el tiempo de juego
            Time.timeScale = 0f;
        }
    }

    //M�todo para el bot�n Resume
    public void Resume()
    {
        //Pausamos el juego
        PauseUnpause();
    }

    //M�todo para el bot�n LevelSelect
    public void LevelSelect()
    {
        //Para ir a la escena LevelSelect
        SceneManager.LoadScene(levelSelect);
        //Reanudamos el tiempo de juego
        Time.timeScale = 1f;
    }

    //M�todo para el bot�n MainMenu
    public void MainMenu()
    {
        //Para ir a la escena MainMenu
        SceneManager.LoadScene(mainMenu);
        //Reanudamos el tiempo de juego
        Time.timeScale = 1f;
    }
}
