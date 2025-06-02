using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    [SerializeField][Range(0f, 1f)] private float volume = 0.1f;

    private void Start()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.volume = volume;
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.Play();
    }
}