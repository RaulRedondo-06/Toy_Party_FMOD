using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<EventInstance> eventInstances = new List<EventInstance>();
    public EventInstance bgmMusic;

    [SerializeField] private CanvasGroup vcaGroup;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        VCA_Controller(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isPaused = !vcaGroup.interactable;
            VCA_Controller(isPaused);

            if (isPaused)
            {
                PauseAllSFX();
                PauseBGM();
            }
            else
            {
                ResumeAllSFX();
                ResumeBGM();
            }
        }
    }

    public void PlayEvent(EventReference eventRef)
    {
        EventInstance eventInst = RuntimeManager.CreateInstance(eventRef);

        eventInst.start();
        eventInst.release();

        eventInstances.Add(eventInst);
    }

    public void PlayMusic(EventReference musicEvent)
    {
        if (bgmMusic.isValid())
        {
            ResumeBGM();
            return;
        }

        bgmMusic = RuntimeManager.CreateInstance(musicEvent);
        bgmMusic.start();
    }

    public void StopMusic()
    {
        if (bgmMusic.isValid())
        {
            bgmMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            bgmMusic.clearHandle();
        }
    }

    public void PauseAllSFX()
    {
        foreach(EventInstance evIns in eventInstances)
        {
            evIns.setPaused(true);
        }
    }

    public void PauseBGM()
    {
        bgmMusic.setPaused(true);
    }

    public void ResumeAllSFX()
    {
        foreach (EventInstance evIns in eventInstances)
        {
            evIns.setPaused(false);
        }
    }

    public void ResumeBGM()
    {
        bgmMusic.setPaused(false);
    }

    private void VCA_Controller(bool show)
    {
        vcaGroup.alpha = show ? 1 : 0;
        vcaGroup.interactable = show;
        vcaGroup.blocksRaycasts = show;
    }
}
