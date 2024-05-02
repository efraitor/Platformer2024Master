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

    //Referencia al FadeScreen
    public Image fadeScreen;
    //Variable para la velocidad de transición al FadeScreen
    public float fadeSpeed;
    //Variables para conocer cuando hacemos fundido a negro o vuelta a transparente
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    //Referencia al texto de completar el nivel
    public TextMeshProUGUI levelCompleteText;

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
        //Cuando empieza el juego hacemos fundido a transparente
        FadeFromBlack();
    }

    private void Update()
    {
        //Llamamos al método de fundido
        FadeUnfade();
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

    //Método para hacer fundido a negro
    public void FadeToBlack() 
    {
        //Activamos la booleana de fundido a negro
        shouldFadeToBlack = true;
        //Desactivamos la booleana de fundido a transparente
        shouldFadeFromBlack = false;
    }

    //Método para hacer fundido a transparente
    public void FadeFromBlack()
    {
        //Activamos la booleana de fundido a transparente
        shouldFadeFromBlack = true;
        //Desactivamos la booleana de fundido a negro
        shouldFadeToBlack = false;
    }

    //Método para fundir a negro o transparente
    void FadeUnfade()
    {
        //Si hay que hacer fundido a negro
        if (shouldFadeToBlack)
        {
            //Cambiar la transparencia del color a opaco
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            //Mathf.MoveTowards (Moverse hacia) -> el valor que queremos cambiar, valor al que lo queremos cambiar, velocidad a la que lo queremos cambiar
            //Si el color ya es totalmente opaco
            if (fadeScreen.color.a == 1f)
                //Paramos de hacer fundido a negro
                shouldFadeToBlack = false;
        }
        //Si hay que hacer fundido a transparente
        if (shouldFadeFromBlack)
        {
            //Cambiar la transparencia del color a transparente
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            //Mathf.MoveTowards (Moverse hacia) -> el valor que queremos cambiar, valor al que lo queremos cambiar, velocidad a la que lo queremos cambiar
            //Si el color ya es totalmente transparente
            if (fadeScreen.color.a == 0f)
                //Paramos de hacer fundido a transparente
                shouldFadeFromBlack = false;
        }
    }

}
