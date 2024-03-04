using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Para poder trabajar con elementos de la UI
using TMPro;

public class UIController : MonoBehaviour
{
    //Referencia a las imágenes de los corazones de la UI
    public Image heart1, heart2, heart3;
    //Referencias a los sprites que cambiarán al perder o ganar un corazón
    public Sprite heartFull, heartEmpty;

    //Referencia al texto de las gemas de la UI
    public TextMeshProUGUI gemText;

    //Referencia al Script que controla la vida del jugador
    private PlayerHealthController _pHReference;
    //Referencia al LevelManager
    private LevelManager _lMReference;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia al PlayerHealthController
        //Con GameObject.Find buscamos el objeto jugador en la escena
        //Con GetComponent obtenemos el código que necesitamos (el componente) del objeto jugador
        _pHReference = GameObject.Find("Player").GetComponent<PlayerHealthController>();
        //Inicializamos la referencia al LevelManager
        _lMReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        //Inicializamos el contador de gemas
        UpdateGemCount();
    }

    //Método para actualizar la vida en la UI
    public void UpdateHealthDisplay()
    {
        //En este caso será mejor implementar un Switch ya que depende del valor de una misma variable
        //Si la vida del jugador fuera 3
        //if(_pHReference.currentHealth == 3)
        //{
        //    heart1.sprite = heartFull;
        //    heart2.sprite = heartFull;
        //    heart3.sprite = heartFull;
        //}

        //Dependiendo del valor de la vida actual del jugador
        switch (_pHReference.currentHealth)
        {
            //En el caso en el que la vida actual valga 3
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                //Cerramos el caso y salimos del Switch
                break;
            //En el caso en el que la vida actual valga 2
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                //Cerramos el caso y salimos del Switch
                break;
            //En el caso en el que la vida actual valga 1
            case 1:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                //Cerramos el caso y salimos del Switch
                break;
            //En el caso en el que la vida actual valga 0
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                //Cerramos el caso y salimos del Switch
                break;
            //En el caso por defecto, el jugador estará muerto
            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                //Cerramos el caso y salimos del Switch
                break;
        }
    }

    //Método para actualizar el contador de gemas
    public void UpdateGemCount()
    {
        //Actualizar el número de gemas recogidas
        //Cast -> convertimos el número entero en texto para que pueda ser representado en la UI
        gemText.text = _lMReference.gemCollected.ToString();
    }
}
