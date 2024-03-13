using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    //Declaramos referencias a los MapPoints adyacentes
    public MapPoint up, right, down, left;
    //Variable para conocer si este MapPoint es un nivel
    public bool isLevel;
    //Variable para conocer el nivel que queremos cargar
    public string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
