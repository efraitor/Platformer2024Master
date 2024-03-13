using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Nos permite trabajar con las escenas

public class LSManager : MonoBehaviour
{
    //Referencia al LSPlayer para poder acceder a su información
    private LSPlayer _lS;
    //Referencia al LSUIController
    private LSUIController _lSUIReference;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia al script del jugador
        _lS = GameObject.Find("LSPlayer").GetComponent<LSPlayer>();
        //Inicializamos la referencia al script del LSUIController
        _lSUIReference = GameObject.Find("LSCanvas").GetComponent<LSUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método que carga el nivel
    public void LoadLevel()
    {
        //Llamamos a la corrutina que carga el nivel
        StartCoroutine(LoadLevelCo());
    }

    //La corrutina para cargar un nivel
    public IEnumerator LoadLevelCo()
    {
        //Hacemos fundido a negro
        _lSUIReference.FadeToBlack();
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(1f);
        //Cargamos el nivel al que queremos ir
        SceneManager.LoadScene(_lS.currentPoint.levelToLoad);
    }
}
