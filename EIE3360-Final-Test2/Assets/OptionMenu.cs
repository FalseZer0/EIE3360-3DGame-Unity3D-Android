using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public Slider volSlider;
    private AudioSource sound;
    public GameObject toggleGroup1;
    private int Quality;
    public GameObject BGM;
    // Start is called before the first frame update
    void Start()
    {
        
        Quality = QualitySettings.GetQualityLevel();
        sound = BGM.GetComponent<AudioSource>();

        volSlider.value = PlayerPrefs.GetFloat("volume", 1); //this is for load volume and make default volume is 1
        if (Quality == 1)
        {
            toggleGroup1.transform.Find("Low").gameObject.GetComponent<Toggle>().isOn = true;
        }
        else if (Quality == 3)
        {
            toggleGroup1.transform.Find("Medium").gameObject.GetComponent<Toggle>().isOn = true;
        }
        else if (Quality == 5)
        {
            toggleGroup1.transform.Find("High").gameObject.GetComponent<Toggle>().isOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        sound.volume = volSlider.value;
    }

    public void low()
    {
        //This is for low quality graphic setting
        QualitySettings.SetQualityLevel(1);
    }

    public void medium()
    {
        //This is for low quality graphic setting
        QualitySettings.SetQualityLevel(3);
    }

    public void high()
    {
        //This is for low quality graphic setting
        QualitySettings.SetQualityLevel(5);
    }

    public void saveButton()
    {
        //this is called when save button is saved
        PlayerPrefs.SetFloat("volume", volSlider.value); //save volume
        PlayerPrefs.SetInt("quality", QualitySettings.GetQualityLevel()); //save the quality
    }
}
