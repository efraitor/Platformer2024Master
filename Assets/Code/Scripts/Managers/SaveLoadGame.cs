using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadGame : MonoBehaviour
{
    public int intEjemplo = 0;
    public float floatEjemplo = 0;
    public string stringEjemplo = "Vacio";
    public bool boolEjemplo = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SaveGame();
        if (Input.GetKeyDown(KeyCode.L))
            LoadGame();
    }

    public void SaveGame()
    {
        //Para guardar vamos a usar PlayerPrefs, una librería que nos permite guardar valores en el Editor del Registro
        //Dentro del Editor del Registro hay que buscar en: UsuarioActual/Software/Unity/UnityEditor/NombreCompañia/NombreProyecto
        PlayerPrefs.SetInt("intEjemploGuardado", intEjemplo);
        PlayerPrefs.SetFloat("floatEjemploGuardado", floatEjemplo);
        PlayerPrefs.SetString("stringEjemploGuardado", stringEjemplo);
        if(boolEjemplo)
            PlayerPrefs.SetInt("boolEjemploGuardado", 1);
        else
            PlayerPrefs.SetInt("boolEjemploGuardado", 0);
    }

    public void LoadGame()
    {
        //Para cargar vamos a usar PlayerPrefs, una librería que nos permite guardar valores en el Editor del Registro
        //Dentro del Editor del Registro hay que buscar en: UsuarioActual/Software/Unity/UnityEditor/NombreCompañia/NombreProyecto
        //Si existe la clave (la variable de guardado) con ese nombre en el Editor del Registro
        if (PlayerPrefs.HasKey("intEjemploGuardado"))
            intEjemplo = PlayerPrefs.GetInt("intEjemploGuardado");
        if (PlayerPrefs.HasKey("floatEjemploGuardado"))
            floatEjemplo = PlayerPrefs.GetFloat("floatEjemploGuardado");
        if (PlayerPrefs.HasKey("stringEjemploGuardado"))
            stringEjemplo = PlayerPrefs.GetString("stringEjemploGuardado");
        if (PlayerPrefs.HasKey("boolEjemploGuardado"))
        {
            if (PlayerPrefs.GetInt("boolEjemploGuardado") == 1)
                boolEjemplo = true;
            else if (PlayerPrefs.GetInt("boolEjemploGuardado") == 0)
                boolEjemplo = false;
        }   
    }
}
