using UnityEngine;

public class StartMusic : MonoBehaviour
{
    public AudioClip sceneMusic;

    private void Start()
    {
        if (AudioManager.Instance != null && sceneMusic != null)
        {
            AudioManager.Instance.PlayBGM(sceneMusic);
        }
    }
}