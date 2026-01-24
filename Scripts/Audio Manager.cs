using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager Instance { get; private set; } // To make sure only one Instance of this Class exists 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
         
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Audio Clips
    // Audio clips

    [Header("Audio Clips For Game Music")]
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip winMusic;
    [SerializeField] AudioClip loseMusic;

    [Header("Audio Clips For Game SFX")]
    [SerializeField] AudioClip spaceShipSFX;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] AudioClip missileSFX;
    [SerializeField] AudioClip shipHitSFX;
    [SerializeField] AudioClip humanVoiceSFX;
    [SerializeField] AudioClip pointGainSFX;
    [SerializeField] AudioClip spaceObjectImpactSFX;

    [Header("Audio Clips For UI SFX")]
    [SerializeField] AudioClip keyboardButtonPressSFX;
    [SerializeField] AudioClip buttonClickSFX;

    [SerializeField] AudioClip levelCompleteSFX;  

    #endregion
    #region Audio Sources

    // Audio sources
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource audioSource;

    #endregion
    #region Audio Variables
    [SerializeField] Toggle audioMuteToggle; 

    #endregion
    public enum AudioType // A Public Enum for the Audio Clips so that, it can be refernced to the audio clip and iterated 
    {
        GameMusic,
        WinMusic,
        LoseMusic,
        SpaceShipSFX,
        SpaceObjecImpactSFX,
        ExplosionSFX,
        MissileSFX,
        ShipHitSFX,
        KeyboardButtonPressSFX,
        ButtonClickSFX,
        HumanVoiceSFX,
        PointGainSFX,
        LevelCompleteSFX
    }
    
    public void PlayAudio(AudioType audioType) 
    {
        if(audioSource != null)
        {

            AudioClip audioClip = null;  // initialy AudioClip is Null, in the Swtich case, it would assigned the AudioClip to the desired Audio that needs to be played
            float volume = 0.5f; 

            switch (audioType) // So if we want to play a specific Audio, we call it by putting into the arugment of the function and the switch case would change the audi clip we want to playu 
            {
            case AudioType.SpaceShipSFX:
                audioClip = spaceShipSFX;
                volume = 0.05f; 
                break;
            case AudioType.ExplosionSFX:
                audioClip = explosionSFX;   
                volume = 0.1f;          
                break;
            case AudioType.MissileSFX:
                audioClip = missileSFX;               
                break;
            case AudioType.ShipHitSFX:
                audioClip = shipHitSFX;              
                break;
            case AudioType.KeyboardButtonPressSFX:
                audioClip = keyboardButtonPressSFX;
                volume = 0.1f; 
                break;
            case AudioType.ButtonClickSFX:
                audioClip = buttonClickSFX;
                break;
            case AudioType.HumanVoiceSFX:
                audioClip = humanVoiceSFX;
                volume = 0.1f;
                break;
            case AudioType.PointGainSFX:
                audioClip = pointGainSFX;
                volume = 0.1f;
                break;
            case AudioType.SpaceObjecImpactSFX:
                audioClip = spaceObjectImpactSFX;
                volume = 1f;
                break;
            case AudioType.LevelCompleteSFX:
                audioClip = levelCompleteSFX;
                volume = 1f; 
                break;

            }

            audioSource.PlayOneShot(audioClip, volume); 
        }
    }
    public void PlayMusic(AudioType musicType) 
    {
        if(musicSource != null)
        {
            AudioClip music = null;
            float volume = 0.5f; 
            switch (musicType)
            {
            case AudioType.GameMusic:
                music = gameMusic;
                volume = 0.2f;
                break;
            case AudioType.WinMusic:
                music = winMusic;
                volume = 0.2f; // Volume for the Win Music is set to 0.2f
                break;
            case AudioType.LoseMusic:
                music = loseMusic;
                volume = 0.2f; // Volume for the Lose Music is set to 0.2f
                break;

            }
        
            musicSource.Stop();
            musicSource.clip = music;
            musicSource.volume = volume;
            musicSource.Play();
        }

        

    }

   public void PauseORUnPauseMusic() // For Ui Buttons to pause and unpause the music 
    {
        if (audioMuteToggle.isOn) // if the toggle is on
        {
            musicSource.volume = 0; // Mute the Music
        }
        else
        {
            musicSource.volume = 1; // UnPause the Music
        }
    }

    public void PlayButtonClickSFX()
    {
       PlayAudio(AudioType.ButtonClickSFX);
    }
   
}
    
