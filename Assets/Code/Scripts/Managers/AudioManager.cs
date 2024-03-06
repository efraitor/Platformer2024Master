using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Creamos un array donde guardamos los sonidos a reproducir
    public AudioSource[] soundEffects;

    //Hacemos el Singleton de este script
    public static AudioManager audioMReference;

    private void Awake()
    {
        //Si la referencia del Singleton esta vacía
        if (audioMReference == null)
            //La rrellenamos con todo el contenido de este código (para que todo sea accesible)
            audioMReference = this;
    }

    //Método para reproducir los sonidos
    public void PlaySFX(int soundToPlay) //soundToPlay = sera el sonido número X del array que queremos reproducir
    {
        //Si ya estaba reproduciendo el sonido, lo paramos
        soundEffects[soundToPlay].Stop();
        //Reproducir el sonido pasado por parámetro
        soundEffects[soundToPlay].Play();
    }
}
