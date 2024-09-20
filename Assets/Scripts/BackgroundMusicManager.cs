using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioSource backgroundMusic;  
    public AudioClip backgroundClip;     

    private void Start()
    {
        backgroundMusic.volume = 0.4f; 
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusic.clip = backgroundClip;
        backgroundMusic.loop = true; 
        backgroundMusic.Play();
    }

    public void RestartBackgroundMusic()
    {
        backgroundMusic.Stop(); 
        PlayBackgroundMusic();  
    }
}
