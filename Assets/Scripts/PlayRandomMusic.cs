using UnityEngine;

public class PlayRandomMusic : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] musicClips;


	void Start()
	{
		PlayRandomClip();
	}

	void PlayRandomClip()
	{
		if (musicClips.Length == 0) return;

		AudioClip randomClip = musicClips[Random.Range(0, musicClips.Length)];
		audioSource.clip = randomClip;
		audioSource.Play();

		Invoke(nameof(PlayRandomClip), randomClip.length);
	}
}
