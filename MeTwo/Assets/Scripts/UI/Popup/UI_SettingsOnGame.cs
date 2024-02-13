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
        BGMSlider,
        SFXSlider,
        DPISlider,
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));

        GetButton((int)Buttons.CloseBtn).onClick.AddListener(OnClickClose); // 닫기 버튼 이벤트

        Slider bgmSlider = GetSlider((int)Sliders.BGMSlider);
        Slider sfxSlider = GetSlider((int)Sliders.SFXSlider);
        Slider dpiSlider = GetSlider((int)Sliders.DPISlider);

        bgmSlider.value = 0.5f;// 에시 값 사용, 이후 게임 설정에서 현재 값을 가져오도록 수정.
        sfxSlider.value = 0.5f;
        dpiSlider.value = 0.5f;

        bgmSlider.onValueChanged.AddListener(OnBGMSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);
        dpiSlider.onValueChanged.AddListener(OnDPISliderChanged);
    }
    void OnClickClose()
    {
        TempManagers.UI.ClosePopupUI();
    }
    void OnBGMSliderChanged(float value)
    {
        // 배경음 설정 변경
        // 예: AudioManager.Instance.SetBGMVolume(value);
    }
    void OnSFXSliderChanged(float value)
    {
        // 효과음 설정 변경
        // 예: AudioManager.Instance.SetSFXVolume(value);
    }
    void OnDPISliderChanged(float value)
    {
        // 민감도 설정 변경
        // 예: SettingsManager.Instance.SetDPI(value);
    }
}
