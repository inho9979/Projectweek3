using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUImanager : MonoBehaviour
{
    private static LobbyUImanager instance;

    public static LobbyUImanager Instance
    {
        get { return instance; }
    }

    public GenericUI[] UIs;

    public Windows defaultUIId;
    private Windows currentUIId;

    public GenericUI GetUI(Windows id)
    {
        return UIs[(int)id];
    }
    private void Awake()
    {
        instance = this;
        Init();
    }
    private void Init()
    {
        for (var i = 0; i< UIs.Length; i++)
        {
            UIs[i].gameObject.SetActive(false);
        }
        currentUIId = defaultUIId;
        UIs[(int)defaultUIId].Open();
    }
    public GenericUI Open(Windows id)
    {
        UIs[(int)currentUIId].Close();
        currentUIId = id;
        UIs[(int)currentUIId].Open();

        return UIs[(int)currentUIId];
    }
}
