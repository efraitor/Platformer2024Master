using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Nos permite trabajar con las escenas

public class LSManager : MonoBehaviour
{
    //Referencia al LSPlayer para poder acceder a su información
    private LSPlayer _lS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método que carga el nivel
    public void LoadLevel()
    {

    }

    //La corrutina para cargar un nivel
    public IEnumerator LoadLevelCo()
    {
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(1f);
        //Cargamos el nivel al que queremos ir
        SceneManager.LoadScene()
    }
}
