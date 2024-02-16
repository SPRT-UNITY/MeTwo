using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UI_SettingsOnGame : UI_Popup
{
    enum Buttons
    {
        CloseBtn,
    }
    enum Sliders
    {
        MasterSlider,
        BGMSlider,
        SFXSlider,
        DPISlider,
    }
    enum Images
    {
        MasterIcon,
        BGMIcon,
        SFXIcon,
    }

    public Sprite MuteSprite;
    public Sprite UnmuteSprite;

    Image MasterIcon;
    Image BGMIcon;
    Image SFXIcon;

    Slider masterSlider;
    Slider bgmSlider;
    Slider sfxSlider;
    Slider dpiSlider;

    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<Slider>(typeof(Sliders));

        MasterIcon = GetImage((int)Images.MasterIcon);
        BGMIcon = GetImage((int)Images.BGMIcon);
        SFXIcon = GetImage((int)Images.SFXIcon);

        MuteSprite = Resources.Load<Sprite>("Icons/mute_white");
        UnmuteSprite = Resources.Load<Sprite>("Icons/soundPlus_white");

        UpdateIcon(MasterIcon, SoundManager.Instance.isMasterMuted == true);
        UpdateIcon(BGMIcon, SoundManager.Instance.isBGMMuted == true);
        UpdateIcon(SFXIcon, SoundManager.Instance.isSFXMuted == true);

        MasterIcon.GetComponent<Button>().onClick.AddListener(() => ToggleMasterMute());
        BGMIcon.GetComponent<Button>().onClick.AddListener(() => ToggleBGMMute());
        SFXIcon.GetComponent<Button>().onClick.AddListener(() => ToggleSFXMute());

        masterSlider = GetSlider((int)Sliders.MasterSlider);
        bgmSlider = GetSlider((int)Sliders.BGMSlider);
        sfxSlider = GetSlider((int)Sliders.SFXSlider);
        dpiSlider = GetSlider((int)Sliders.DPISlider);

        masterSlider.AddComponent<SliderEventHandler>();
        bgmSlider.AddComponent<SliderEventHandler>();
        sfxSlider.AddComponent<SliderEventHandler>();
        dpiSlider.AddComponent<SliderEventHandler>();

        foreach (var handler in masterSlider.GetComponentsInChildren<SliderEventHandler>()) handler.OnDragEnd += SaveSliderValue;
        foreach (var handler in bgmSlider.GetComponentsInChildren<SliderEventHandler>()) handler.OnDragEnd += SaveSliderValue;
        foreach (var handler in sfxSlider.GetComponentsInChildren<SliderEventHandler>()) handler.OnDragEnd += SaveSliderValue;
        foreach (var handler in dpiSlider.GetComponentsInChildren<SliderEventHandler>()) handler.OnDragEnd += SaveSliderValue;

        masterSlider.value = SoundManager.Instance.masterVolumeScale;
        bgmSlider.value = SoundManager.Instance.bgmVolumeScale;
        sfxSlider.value = SoundManager.Instance.sfxVolumeScale;
        dpiSlider.value = PlayerPrefs.GetFloat("DPI", 0.5f); // DPI 임시구현

        GetButton((int)Buttons.CloseBtn).onClick.AddListener(OnClickClose); // 닫기 버튼 이벤트
    }
    void SaveSliderValue()
    {
        // 슬라이더 값 가져오기
        float masterVolume = masterSlider.value;
        float bgmVolume = bgmSlider.value;
        float sfxVolume = sfxSlider.value;
        float dpiValue = dpiSlider.value;

        // SoundManager에 볼륨 값 업데이트
        SoundManager.Instance.masterVolumeScale = masterVolume;
        SoundManager.Instance.bgmVolumeScale = bgmVolume;
        SoundManager.Instance.sfxVolumeScale = sfxVolume;
        PlayerPrefs.SetFloat("DPI", dpiValue); // DPI 임시구현

        Debug.Log($"saved: Master Volume = {masterVolume}, BGM Volume = {bgmVolume}, SFX Volume = {sfxVolume}, DPI = {dpiValue}");
    }
    void OnClickClose()
    {
        Managers.UI.ClosePopupUI();
    }
    void UpdateIcon(Image icon, bool isMuted)
    {
        icon.sprite = isMuted ? MuteSprite : UnmuteSprite;
    }
    void ToggleMasterMute()
    {
        bool isMuted = SoundManager.Instance.isMasterMuted;
        SoundManager.Instance.isMasterMuted = !isMuted;
        UpdateIcon(MasterIcon, !isMuted);

        SoundManager.Instance.PlaySFX("UISelect");
    }
    void ToggleBGMMute()
    {
        bool isMuted = SoundManager.Instance.isBGMMuted;
        SoundManager.Instance.isBGMMuted = !isMuted;
        UpdateIcon(BGMIcon, !isMuted);

        SoundManager.Instance.PlaySFX("UISelect");
    }
    void ToggleSFXMute()
    {
        bool isMuted = SoundManager.Instance.isSFXMuted;
        SoundManager.Instance.isSFXMuted = !isMuted;
        UpdateIcon(SFXIcon, !isMuted);

        SoundManager.Instance.PlaySFX("UISelect");
    }
}
