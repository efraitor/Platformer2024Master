using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Librería para poder usar las escenas

public class LevelManager : MonoBehaviour
{
    //Variable de tiempo para la corrutina
    public float waitToRespawn;

    //Variable para el contador de gemas
    public int gemCollected;

    //Variable para guardar el nombre del nivel al queremos ir
    public string levelToLoad;

    //Referencia al PlayerController
    private PlayerController _pCReference;
    //Referencia al CheckpointController
    private CheckpointController _cReference;
    //Referencia al UIController
    private UIController _uIReference;
    //Referencia al PlayerHealthController
    private PlayerHealthController _pHReference;

    private void Start()
    {
        //Inicializamos la referencia al PlayerController
        _pCReference = GameObject.Find("Player").GetComponent<PlayerController>();
        //Inicializamos la referencia al CheckpointController
        _cReference = GameObject.Find("CheckpointController").GetComponent<CheckpointController>();
        //Inicializamos la referencia al UIController
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
        //Inicializamos la referencia al PlayerHealthController
        _pHReference = GameObject.Find("Player").GetComponent<PlayerHealthController>();
        //Vemos si estamos en la escena del autobús
        if (SceneManager.GetActiveScene().name == "LoadScene")
        {
            //Llamamos a la corrutina de salir del nivel de carga
            LoadLevelExit();
            //Llamamos a la música del autobús
            AudioManager.audioMReference.bus.Play();
        }
           
    }

    //Método para respawnear al jugador cuando muere
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayerCo());
    }

    //Corrutina para respawnear al jugador
    private IEnumerator RespawnPlayerCo()
    {
        //Desactivamos al jugador
        _pCReference.gameObject.SetActive(false);
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(waitToRespawn);
        //Reactivamos al jugador
        _pCReference.gameObject.SetActive(true);
        //Lo ponemos en la posición de Respawn
        _pCReference.transform.position = _cReference.spawnPoint;
        //Ponemos la vida del jugador al máximo
        _pHReference.currentHealth = _pHReference.maxHealth;
        //Actualizamos la UI
        _uIReference.UpdateHealthDisplay();
    }

    //Método para terminar un nivel
    public void ExitLevel()
    {
        //Llamamos a la corrutina de salir del nivel
        StartCoroutine(ExitLevelCo());
    }
    
    //Corrutina de terminar el nivel
    public IEnumerator ExitLevelCo()
    {
        //Paramos los inputs del jugador
        _pCReference.stopInput = true;
        //Paramos el movimiento del jugador
        _pCReference.StopPlayer();
        //Hacemos el método que pone la música del final del nivel
        AudioManager.audioMReference.LevelEndMusic();
        //Mostramos el cartel de haber finalizado el nivel
        _uIReference.levelCompleteText.gameObject.SetActive(true);
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(1.5f);
        //Hacemos fundido a negro
        _uIReference.FadeToBlack();
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(1.5f);
        //Ir a la pantalla de carga o al selector de niveles
        SceneManager.LoadScene(levelToLoad);
    }

    //Método para salir del nivel de carga
    public void LoadLevelExit()
    {
        //Llamamos a la corrutina que nos saca del nivel de carga
        StartCoroutine(LoadLevelExitCo());
    }

    //Corrutina para salir del nivel de carga
    IEnumerator LoadLevelExitCo()
    {
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(3.5f);
        //Hacemos fundido a negro
        _uIReference.FadeToBlack();
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(1.5f);
        //Ir a la pantalla de carga o al selector de niveles
        SceneManager.LoadScene(levelToLoad);
    }
}
