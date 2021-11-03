using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SoundManager : MonoBehaviour
{

    private static SoundManager instance;
    public static SoundManager Instance
    {
        get => instance;
    }

    public AudioSource bgSound;

    public AudioClip mainBgm;
    public AudioClip inGameBgm;
    public AudioClip finishBgm;

    private GameObject curBgm;
    private int curScene = -1;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
            bgSound = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
    }
    void Update()
    {
        bgSound.volume = GameManager.Instance.BGMVolume;
        Debug.Log(GameManager.Instance.BGMVolume);

    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "GameScene")
        {
            BgSoundPlay(inGameBgm);
        }
        else if (arg0.name == "StageSelectScene")
        {
            BgSoundPlay(mainBgm);
        }
        else if (arg0.name == "LobbyScene")
        {
            BgSoundPlay(mainBgm);
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.volume = GameManager.Instance.EffectVolume;
        Debug.Log(GameManager.Instance.EffectVolume);
        audiosource.Play();

        Destroy(go, clip.length);
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.Play();
    }

    public void BgSoundStop()
    {
        bgSound.Stop();
    }
}


//if (GameManager.Instance.sceneNum != -1)
//{
//    if (curScene != GameManager.Instance.sceneNum)
//    {
//        Debug.Log(curScene);
//        if (GameManager.Instance.sceneNum == 2)
//        {
//            Destroy(curBgm);
//            BGMPlay(inGameBgm);
//        }
//        else
//        {
//            if (curScene == 2)
//            {
//                Destroy(curBgm);
//                BGMPlay(mainBgm);
//            }
//        }
//        curScene = GameManager.Instance.sceneNum;
//    }
//}

//public void BGMPlay(AudioClip clip)
//{
//    curBgm = new GameObject("BGMSound");
//    AudioSource audiosource = curBgm.AddComponent<AudioSource>();
//    audiosource.clip = clip;
//    audiosource.volume = GameManager.Instance.BGMVolume;
//    audiosource.loop = true;
//    audiosource.Play();
//}