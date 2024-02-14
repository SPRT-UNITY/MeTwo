using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        MuteSprite = Resources.Load<Sprite>("Icons/mute_black");
        UnmuteSprite = Resources.Load<Sprite>("Icons/soundPlus_black");

        UpdateIcon(MasterIcon, PlayerPrefs.GetInt("MasterMute", 0) == 1);
        UpdateIcon(BGMIcon, PlayerPrefs.GetInt("BGMMute", 0) == 1);
        UpdateIcon(SFXIcon, PlayerPrefs.GetInt("SFXMute", 0) == 1);

        MasterIcon.GetComponent<Button>().onClick.AddListener(() => ToggleMute("Master"));
        BGMIcon.GetComponent<Button>().onClick.AddListener(() => ToggleMute("BGM"));
        SFXIcon.GetComponent<Button>().onClick.AddListener(() => ToggleMute("SFX"));

        Slider masterSlider = GetSlider((int)Sliders.MasterSlider);
        Slider bgmSlider = GetSlider((int)Sliders.BGMSlider);
        Slider sfxSlider = GetSlider((int)Sliders.SFXSlider);
        Slider dpiSlider = GetSlider((int)Sliders.DPISlider);

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        dpiSlider.value = PlayerPrefs.GetFloat("DPI", 0.5f);

        masterSlider.onValueChanged.AddListener(OnMasterSliderChanged);
        bgmSlider.onValueChanged.AddListener(OnBGMSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);
        dpiSlider.onValueChanged.AddListener(OnDPISliderChanged);

        GetButton((int)Buttons.CloseBtn).onClick.AddListener(OnClickClose); // 닫기 버튼 이벤트
    }
    void OnClickClose()
    {
        TempManagers.UI.ClosePopupUI();
    }
    void OnMasterSliderChanged(float value)
    {
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
    void OnBGMSliderChanged(float value)
    {
        PlayerPrefs.SetFloat("BGMVolume", value);
    }
    void OnSFXSliderChanged(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
    void OnDPISliderChanged(float value)
    {
        PlayerPrefs.SetFloat("DPI", value);
    }

    void UpdateIcon(Image icon, bool isMuted)
    {
        icon.sprite = isMuted ? MuteSprite : UnmuteSprite;
    }
    void ToggleMute(string category)
    {
        // SoundManager의 Mute 상태를 토글하고 PlayerPrefs를 업데이트하는 로직을 구현합니다.
        // 예: SoundManager.Instance.ToggleMute(category);

        // 아이콘 업데이트
        bool isMuted = PlayerPrefs.GetInt(category + "Mute", 0) == 1;
        PlayerPrefs.SetInt(category + "Mute", isMuted ? 0 : 1);

        // 아이콘 이미지를 변경합니다.
        Image icon = category == "Master" ? MasterIcon : category == "BGM" ? BGMIcon : SFXIcon;
        UpdateIcon(icon, !isMuted);
    }
}
