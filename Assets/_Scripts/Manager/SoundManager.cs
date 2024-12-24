using UnityEngine;

public class SoundManager : Singleton<SoundManager> {
    private AudioSource audioSource;

    public AudioClip popSound;
    public AudioClip destroySound;

    public enum SoundType {
        Pop,
        Destroy
    }

    private void Awake() {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(SoundType soundType, float volume = 1f) {
        AudioClip clipToPlay = null;
        
        switch (soundType) {
            case SoundType.Pop:
                clipToPlay = popSound;
                break;
            case SoundType.Destroy:
                clipToPlay = destroySound;
                break;
        }

        if (clipToPlay != null) {
            audioSource.PlayOneShot(clipToPlay, volume);
        }
    }
}
