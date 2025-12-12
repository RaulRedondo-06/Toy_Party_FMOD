using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using System.Collections;

public class VariableSlider_Script : MonoBehaviour
{
    private Slider slider;

    [SerializeField] private EventReference eventRef;
    [SerializeField] private string parameterName;
    private EventInstance evt;

    void Start()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        slider = GetComponent<Slider>();
        yield return new WaitForSeconds(0.1f);

        slider.onValueChanged.AddListener(val =>
        {
            evt = AudioManager.instance.bgmMusic;
            if (evt.isValid())
            {
                evt.setParameterByName(parameterName, val);
            }
        });
    }
}
