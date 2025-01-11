using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    public AudioSource BGM_Audio;       // BGM 파일
    public AudioSource SE_Audio;        // SE 파일

    public static int BGM_value;        // BGM 값
    public static int SE_value;         // SE 값

    public Text BGM_label;      // BGM 토글의 라벨
    public Text SE_label;       // SE 토글의 라벨

    public Slider BGM_slider;
    public Slider SE_slider;




    private void Awake()
    {
        BGM_Audio = GameObject.Find("SoundManager").GetComponent<AudioSource>();
    }

    void Start()
    {
        BGM_slider.value = AudioPlayer.instance.bgm_volume;
        SE_slider.value = AudioPlayer.instance.se_volume;
    }

    void Update()
    {
        BGM_value = (int)(BGM_slider.value * 1000);
        SE_value = (int)(SE_slider.value * 500);

        BGM_label.text = BGM_value.ToString();   // BGM 토글의 라벨 값 = 슬라이더 값
        SE_label.text = SE_value.ToString();     // SE 토글의 라벨 값 = 슬라이더 값
    }

    public void BGM_VolumeManage(float volume)
    {
        BGM_Audio.volume = volume;
        AudioPlayer.instance.bgm_volume = volume;
    }
    public void SE_VolumeManage(float volume)
    {
        SE_Audio.volume = volume;
        AudioPlayer.instance.se_volume = volume;
    }
}