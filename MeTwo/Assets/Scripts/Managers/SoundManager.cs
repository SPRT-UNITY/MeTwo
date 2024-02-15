using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = GameObject.Find("SoundManager");

                if (gameObject == null)
                {
                    gameObject = new GameObject("SoundManager");
                    instance = gameObject.AddComponent<SoundManager>();
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

    Dictionary<string, IResourceLocation> bgmNameCache = new Dictionary<string, IResourceLocation>();
    Dictionary<string, IResourceLocation> sfxNameCache = new Dictionary<string, IResourceLocation>();

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
            PlayerPrefs.SetInt("BGMMute", _isBGMMuted ? 1 : 0);
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
        isMasterMuted = Convert.ToBoolean(PlayerPrefs.GetInt("MasterMute", 0));
        isBGMMuted = Convert.ToBoolean(PlayerPrefs.GetInt("BGMMute", 0));
        isSFXMuted = Convert.ToBoolean(PlayerPrefs.GetInt("SFXMute", 0));
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
        if (!bgmNameCache.ContainsKey(name))
        {
            Debug.LogError("BGM name not found!");
            return;
        }

        bgmClip = Addressables.LoadAssetAsync<AudioClip>(bgmNameCache[name]).WaitForCompletion();
        bgmSource?.Stop();
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
        if (!sfxNameCache.ContainsKey(name))
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
        if (!sfxNameCache.ContainsKey(name))
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
        sfxList.Add((name, Addressables.LoadAssetAsync<AudioClip>(sfxNameCache[name]).WaitForCompletion()));
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
        var bgmNameLoader = Addressables.LoadResourceLocationsAsync("BGM");
        bgmNameLoader.Completed += (handle) =>
        {
            var locations = handle.Result;
            foreach (var location in locations) 
            {
                string path = location.ToString();
                string name = path.Substring(path.LastIndexOf("/") + 1, path.LastIndexOf(".") - (path.LastIndexOf("/") + 1));
                bgmNameCache.Add(name, location);
            }
        };
        bgmNameLoader.WaitForCompletion();

        var sfxNameLoader = Addressables.LoadResourceLocationsAsync("SFX");
        sfxNameLoader.Completed += (handle) =>
        {
            var locations = handle.Result;
            foreach (var location in locations)
            {
                string path = location.ToString();
                string name = path.Substring(path.LastIndexOf("/") + 1, path.LastIndexOf(".") - (path.LastIndexOf("/") + 1));
                sfxNameCache.Add(name, location);
            }
        };
        sfxNameLoader.WaitForCompletion();
    }

    public void ClearSFXList()
    {
        sfxList.Clear();
    }

    #endregion

}
