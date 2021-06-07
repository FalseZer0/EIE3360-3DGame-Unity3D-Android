using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality", 3));  //load quality with default normal
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
