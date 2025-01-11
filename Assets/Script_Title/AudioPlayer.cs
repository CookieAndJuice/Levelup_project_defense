using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip MainBGM;
    public AudioClip PlayBGM;

    AudioSource AS;
    
    [Header("Button")]
    [SerializeField] AudioClip buttonClip;
    // [Range(0f, 1f)] : shootingVolume을 0f와 1f 사이로 제한하는 속성 
    [SerializeField] [Range(0f, 1f)] float buttonVolume = 1f;

    [Header("Hit")]
    [SerializeField] AudioClip hitClip;
    [SerializeField] [Range(0f, 1f)] float hitVolume = 1f;

    [Header("Sword1")]
    [SerializeField] AudioClip sword1Clip;
    [SerializeField][Range(0f, 1f)] float sword1Volume = 1f;

    [Header("Sword2")]
    [SerializeField] AudioClip sword2Clip;
    [SerializeField][Range(0f, 1f)] float sword2Volume = 1f;

    [Header("Sword3")]
    [SerializeField] AudioClip sword3Clip;
    [SerializeField][Range(0f, 1f)] float sword3Volume = 1f;

    [Header("Gunner1")]
    [SerializeField] AudioClip gunner1Clip;
    [SerializeField][Range(0f, 1f)] float gunner1Volume = 1f;

    [Header("Gunner2")]
    [SerializeField] AudioClip gunner2Clip;
    [SerializeField][Range(0f, 1f)] float gunner2Volume = 1f;

    [Header("Gunner3")]
    [SerializeField] AudioClip gunner3Clip;
    [SerializeField][Range(0f, 1f)] float gunner3Volume = 1f;

    [Header("Magician1")]
    [SerializeField] AudioClip magician1Clip;
    [SerializeField][Range(0f, 1f)] float magician1Volume = 1f;

    [Header("Magician2")]
    [SerializeField] AudioClip magician2Clip;
    [SerializeField][Range(0f, 1f)] float magician2Volume = 1f;
    
    [Header("Magician3")]
    [SerializeField] AudioClip magician3Clip;
    [SerializeField][Range(0f, 1f)] float magician3Volume = 1f;


    Scene scene;
    bool isSceneChanged = false;

    //볼륨값 저장
    public float bgm_volume;
    public float se_volume;

    //싱글톤//
    public static AudioPlayer instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { 
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        AS = gameObject.GetComponent<AudioSource>();
        bgm_volume = 0.0805f;
        se_volume = 0.161f;
    }

    private void Update()
    {
        if (isSceneChanged)
        {
            if (SceneManager.GetActiveScene().name == "StartScene")
            {
                AS.clip = MainBGM;
            }
            else if (SceneManager.GetActiveScene().name == "IngameScene")
            {
                AS.clip = PlayBGM;
            }
            AS.Play();
            isSceneChanged = false;
        }

        if(SceneManager.sceneCount == 2)
        {
            isSceneChanged = true;
        }
    }
    public void PlayButtonClip()
    {
        if (buttonClip != null)
        {
            AudioSource.PlayClipAtPoint(buttonClip, Camera.main.transform.position, se_volume);
        }
    }
    public void PlayHitClip()
    {
        if (hitClip != null)
        {
            AudioSource.PlayClipAtPoint(hitClip, Camera.main.transform.position, se_volume);
        }
    }
    public void PlaySword1Clip()
    {
        if (sword1Clip != null)
        {
            AudioSource.PlayClipAtPoint(sword1Clip, Camera.main.transform.position, se_volume);
        }
    }
    public void PlaySword2Clip()
    {
        if (sword1Clip != null)
        {
            AudioSource.PlayClipAtPoint(sword2Clip, Camera.main.transform.position, se_volume);
        }
    }
    public void PlaySword3Clip()
    {
        if (sword1Clip != null)
        {
            AudioSource.PlayClipAtPoint(sword3Clip, Camera.main.transform.position, se_volume);
        }
    }
    public void PlayGunner1Clip()
    {
        if (hitClip != null)
        {
            AudioSource.PlayClipAtPoint(gunner1Clip, Camera.main.transform.position, se_volume);
        }
    }
    public void PlayGunner2Clip()
    {
        if (hitClip != null)
        {
            AudioSource.PlayClipAtPoint(gunner2Clip, Camera.main.transform.position, se_volume);
        }
    }
    public void PlayGunner3Clip()
    {
        if (hitClip != null)
        {
            AudioSource.PlayClipAtPoint(gunner3Clip, Camera.main.transform.position, se_volume);
        }
    }
    public void PlayMagician1Clip()
    {
        if (hitClip != null)
        {
            AudioSource.PlayClipAtPoint(magician1Clip, Camera.main.transform.position, se_volume);
        }
    }
    public void PlayMagician2Clip()
    {
        if (hitClip != null)
        {
            AudioSource.PlayClipAtPoint(magician2Clip, Camera.main.transform.position, se_volume);
        }
    }
    public void PlayMagician3Clip()
    {
        if (hitClip != null)
        {
            AudioSource.PlayClipAtPoint(magician3Clip, Camera.main.transform.position, se_volume);
        }
    }
}
