using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeController : MonoBehaviour
{

    [SerializeField] string volumeValueMaster;
    [SerializeField] string volumeValueChase;
    //    [SerializeField] AudioMixer mixer;
    [SerializeField] private Text volumePercentageMaster;
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Text volumePercentageChase;
    [SerializeField] private Slider sliderChase;
    public AudioMixer mixer;

    private string volString;
    private int volPerc1;
    private int volPerc2;
    private void Awake()
    {
        sliderMaster.onValueChanged.AddListener(VolumeChangeMaster);
        sliderChase.onValueChanged.AddListener(VolumeChangeChase);
        sliderMaster.minValue = 0.001f;
        sliderChase.minValue = 0.001f;
        mixer.SetFloat("MasterVolume", value: Mathf.Log10((float)0.2) * 20f);
        mixer.SetFloat("ChaseAmbience", value: Mathf.Log10((float)0.54) * 20f);
    }

    void Start()
    {
        sliderMaster.value = PlayerPrefs.GetFloat(volumeValueMaster, sliderMaster.value);
        sliderChase.value = PlayerPrefs.GetFloat(volumeValueChase, sliderChase.value);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeValueMaster, sliderMaster.value);
        PlayerPrefs.SetFloat(volumeValueChase, sliderChase.value);
    }
    private void VolumeChangeMaster(float value)
    {
        mixer.SetFloat("MasterVolume", value: Mathf.Log10(value) * 20f);
        volPerc1 = (int)(100 * value);
        volString = volPerc1 + " %";
        volumePercentageMaster.text = volString;
    }
    private void VolumeChangeChase(float value)
    {
        mixer.SetFloat("ChaseAmbience", value: Mathf.Log10(value) * 20f);
        volPerc2 = (int)(100 * value);
        volString = volPerc2 + " %";
        volumePercentageChase.text = volString;
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
