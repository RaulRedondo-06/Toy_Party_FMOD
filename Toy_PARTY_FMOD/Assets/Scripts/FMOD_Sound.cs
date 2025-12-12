using FMODUnity;
using UnityEngine;

public class FMOD_Sound : MonoBehaviour
{
    public EventReference sound;

    public void Play(bool isBGM)
    {
        if (!isBGM) { AudioManager.instance.PlayEvent(sound); }
        else { AudioManager.instance.PlayMusic(sound); }
    }
}
