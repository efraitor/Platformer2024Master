using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    //Variables para controlar la vida actual del jugador y el máximo de vida que puede tener
    //[HideInInspector] -> es un atributo de la variable que me permite que una variable no sea visible en el editor de Unity pero se mantenga pública
    [HideInInspector] public int currentHealth;
    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la vida del jugador
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para manejar el daño
    public void DealWithDamage()
    {
        //Restamos 1 de la vida que tengamos
        currentHealth--; //_currentHealth -= 1 <=> _currentHealth = _currentHealth - 1 <=> _currentHealth--
        
        //Si la vida está en 0 o por debajo (para asegurarnos de tener en cuenta solo valores positivos)
        if(currentHealth <= 0)
        {
            //Hacemos que la vida se ponga a cero si se queda en negativo
            currentHealth = 0;

            //Hacemos desaparecer de momento al jugador
            gameObject.SetActive(false);
        }
    }
}
