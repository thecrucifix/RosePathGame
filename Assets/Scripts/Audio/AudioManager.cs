using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Linq;

public class AudioManager : MonoBehaviour
{
 public static AudioManager instance { get; private set; }


    private List<EventInstance> eventInstances;

    private EventInstance ambienceEventInstance;


    private void Start()
    {
        InitializeAmbience(FMODEvents.instance.WindBase);
    }

    private void Awake()
    {
        if (instance != null) 
        {
            Debug.LogError("������� ������ ������ �����-��������� � �����");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void CleannUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }
    private void OnDestroy()
    {
        CleannUp();
    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    public void SetAmbientParmeter(string paramenterName, float parameterValue)
    {
        ambienceEventInstance.setParameterByName(paramenterName, parameterValue);
    }
}
