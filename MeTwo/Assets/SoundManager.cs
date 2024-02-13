using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();

                if (instance == null)
                {
                    instance = new GameObject("SoundManager").AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    AudioSource bgmSource;

    [SerializeField]
    AudioClip bgmClip;

    [SerializeField]
    AudioSource sfxSource;

    [SerializeField]
    int MAX_SFX_CACHE_SIZE = 10;

    List<(string, AudioClip)> sfxList;
    int sfxEraseIndex = 0;

    List<string> bgmNameCache = new List<string>();
    List<string> sfxNameCache = new List<string>();

    string BGM_PATH;
    string SFX_PATH;

    #region property for volumeScale and mute

    private float _bgmVolumeScale;
    public float bgmVolumeScale
    {
        get { return _bgmVolumeScale * masterVolumeScale; }
        set
        {
            _bgmVolumeScale = value;
            PlayerPrefs.SetFloat("BGMVolume", bgmVolumeScale);
            bgmSource.volume = _bgmVolumeScale * masterVolumeScale;
        }
    }

    private bool _isBGMMuted { get; set; }
    public bool isBGMMuted
    {
        get { return _isBGMMuted; }
        set
        {
            _isBGMMuted = value;
            PlayerPrefs.SetInt("SFXMute", _isBGMMuted ? 1 : 0);
            bgmSource.mute = _isBGMMuted | _isMasterMuted;
            OnBGMMuted?.Invoke();
        }
    }

    private float _sfxVolumeScale;
    public float sfxVolumeScale
    {
        get { return _sfxVolumeScale * masterVolumeScale; }
        set
        {
            _sfxVolumeScale = value;
            PlayerPrefs.SetFloat("SFXVolume", _sfxVolumeScale);
            sfxSource.volume = _sfxVolumeScale * masterVolumeScale;
        }
    }

    private bool _isSFXMuted { get; set; }
    public bool isSFXMuted
    {
        get { return _isSFXMuted; }
        set
        {
            _isSFXMuted = value;
            PlayerPrefs.SetInt("SFXMute", _isSFXMuted ? 1 : 0);
            bgmSource.mute = _isSFXMuted | _isMasterMuted;
            OnSFXMuted?.Invoke();
        }
    }

    private float _masterVolumeScale;
    public float masterVolumeScale
    {
        get { return _masterVolumeScale; }
        set
        {
            _masterVolumeScale = value;
            PlayerPrefs.SetFloat("MasterVolume", _masterVolumeScale);
            bgmSource.volume = _bgmVolumeScale * _masterVolumeScale;
            sfxSource.volume = _sfxVolumeScale * _masterVolumeScale;
        }
    }

    private bool _isMasterMuted;
    public bool isMasterMuted { get { return _isMasterMuted; } 
        set 
        {
            _isMasterMuted = value;
            PlayerPrefs.SetInt("MasterMute", _isMasterMuted ? 1 : 0);
            isBGMMuted = value;
            isSFXMuted = value;
            OnMasterMuted?.Invoke();
        } 
    }

    #endregion

    #region event for volume mute

    public event Func<bool> OnBGMMuted;
    public event Func<bool> OnSFXMuted;
    public event Func<bool> OnMasterMuted;

    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        CachingNames();

        sfxList = new List<(string, AudioClip)>(MAX_SFX_CACHE_SIZE);

        if (GameObject.Find("BGMPlayer") == null)
        {
            bgmSource = new GameObject("BGMPlayer").AddComponent<AudioSource>();
            bgmSource.loop = true;
            bgmSource.transform.parent = transform;
        }

        if (GameObject.Find("SFXPlayer") == null)
        {
            sfxSource = new GameObject("SFXPlayer").AddComponent<AudioSource>();
            sfxSource.transform.parent = transform;
        }

        masterVolumeScale = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        bgmVolumeScale = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        sfxVolumeScale = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {

    }

    #region BGM

    public void PlayBGM(string name)
    {
        if (!bgmNameCache.Contains(name))
        {
            Debug.LogError("BGM name not found!");
            return;
        }

        bgmSource?.Stop();
        bgmClip = Resources.Load<AudioClip>(BGM_PATH + name);
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }

    public void PauseBGM()
    {
        bgmSource?.Pause();
    }

    public void ResumeBGM()
    {
        bgmSource.UnPause();
    }

    #endregion

    #region SFX
    public void PlaySFX(string name, Vector3 position)
    {
        if (!sfxNameCache.Contains(name))
        {
            Debug.LogError("SFX name not found!");
            return;
        }

        if (!IsSFXListContains(name))
        {
            LoadSFXOnList(name);
        }

        AudioSource.PlayClipAtPoint(GetAudioClipFromSFXList(name), position, sfxVolumeScale);
    }

    public void PlaySFX(string name) 
    {
        if (!sfxNameCache.Contains(name))
        {
            Debug.LogError("SFX name not found!");
            return;
        }

        if (!IsSFXListContains(name))
        {
            LoadSFXOnList(name);
        }

        sfxSource.PlayOneShot(GetAudioClipFromSFXList(name));
    }

    #endregion

    #region SFX Caching and Data loading

    void LoadSFXOnList(string name)
    {
        if (sfxList.Count == MAX_SFX_CACHE_SIZE)
        {
            sfxList.RemoveAt(sfxEraseIndex++);
            sfxEraseIndex %= MAX_SFX_CACHE_SIZE;
        }
        sfxList.Add((name, Resources.Load<AudioClip>(SFX_PATH + name)));
    }

    AudioClip GetAudioClipFromSFXList(string name)
    {
        foreach (var tuple in sfxList)
        {
            if (tuple.Item1 == name)
                return tuple.Item2;
        }
        return null;
    }

    bool IsSFXListContains(string name)
    {
        foreach (var tuple in sfxList)
        {
            if (tuple.Item1 == name)
                return true;
        }
        return false;
    }

    void CachingNames()
    {
        foreach (string s in AssetDatabase.GetAssetPathsFromAssetBundle("bgm"))
        {
            if (BGM_PATH == null) 
            {
                BGM_PATH = s.Substring(0, s.LastIndexOf("/"));
                Debug.Log(BGM_PATH);
            }
            string name = s.Substring(s.LastIndexOf("/") + 1, s.LastIndexOf(".") - (s.LastIndexOf("/") + 1));
            bgmNameCache.Add(name);
            Debug.Log("Add BGM : " + name);
        }

        foreach (string s in AssetDatabase.GetAssetPathsFromAssetBundle("sfx"))
        {
            if (SFX_PATH == null)
            {
                SFX_PATH = s.Substring(0, s.LastIndexOf("/"));
                Debug.Log(SFX_PATH);
            }
            string name = s.Substring(s.LastIndexOf("/") + 1, s.LastIndexOf(".") - (s.LastIndexOf("/") + 1));
            sfxNameCache.Add(name);
            Debug.Log("Add SFX : " + name);
        }
    }

    public void ClearSFXList()
    {
        sfxList.Clear();
    }

    #endregion

}
