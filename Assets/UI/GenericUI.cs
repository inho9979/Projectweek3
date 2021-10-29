using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericUI : MonoBehaviour
{
    public LobbyUImanager manager;
    public Windows nextWindow;
    public Windows previousWindow;

    void Start()
    {

    }
    void Update()
    {

    }

    public virtual void Display(bool value)
    {
        gameObject.SetActive(value);
    }

    public virtual void Open()
    {
        Display(true);
    }
    public virtual void Close()
    {
        Display(false);
    }
}
