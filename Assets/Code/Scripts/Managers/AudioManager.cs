using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Creamos un array donde guardamos los sonidos a reproducir
    public AudioSource[] soundEffects;
    //Referencias a la m�sica del juego
    public AudioSource bgm, levelEndMusic, bossMusic;
     
    //Hacemos el Singleton de este script
    public static AudioManager audioMReference;

    private void Awake()
    {
        //Si la referencia del Singleton esta vac�a
        if (audioMReference == null)
            //La rrellenamos con todo el contenido de este c�digo (para que todo sea accesible)
            audioMReference = this;
    }

    //M�todo para reproducir los sonidos
    public void PlaySFX(int soundToPlay) //soundToPlay = sera el sonido n�mero X del array que queremos reproducir
    {
        //Si ya estaba reproduciendo el sonido, lo paramos
        soundEffects[soundToPlay].Stop();
        //Alteramos un poco el sonido cada vez que se vaya a reproducir
        soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f);
        //Reproducir el sonido pasado por par�metro
        soundEffects[soundToPlay].Play();
    }

    //M�todo para reproducir la m�sica del Boss Final
    public void PlayBossMusic()
    {
        //Paramos la m�sica de fondo
        bgm.Stop();
        //Reproducimos la m�sica del jefe
        bossMusic.Play();
    }
}
