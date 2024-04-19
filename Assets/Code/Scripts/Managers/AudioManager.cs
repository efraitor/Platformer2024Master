using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Creamos un array donde guardamos los sonidos a reproducir
    public AudioSource[] soundEffects;
    //Referencias a la música del juego
    public AudioSource bgm, levelEndMusic, bossMusic;
     
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
        //Alteramos un poco el sonido cada vez que se vaya a reproducir
        soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f);
        //Reproducir el sonido pasado por parámetro
        soundEffects[soundToPlay].Play();
    }

    //Método para reproducir la música del Boss Final
    public void PlayBossMusic()
    {
        //Paramos la música de fondo
        bgm.Stop();
        //Reproducimos la música del jefe
        bossMusic.Play();
    }
}
