using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using System.Collections;

public class VCA_SliderController : MonoBehaviour
{
    [SerializeField] private string vcaPath;
    private Slider slider;

    private VCA vca;

    void Start()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForSeconds(0.1f);
        slider = GetComponent<Slider>();

        vca = RuntimeManager.GetVCA(vcaPath);

        float currentVolume;
        vca.getVolume(out currentVolume);

        slider.SetValueWithoutNotify(currentVolume);

        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        vca.setVolume(value);
    }
}
