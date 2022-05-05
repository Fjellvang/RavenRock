using Assets.Scripts.Player;
using UnityEngine;
using Zenject;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip[] runningClips;

    public AudioClip exhaustedSound;
    public AudioSource audioSource;
    private PlayerStateDescriber playerState;

    [Inject]
    public void Construct(PlayerStateDescriber playerState)
    {
        this.playerState = playerState;
    }
    // Update is called once per frame
    void Update()
    {
        //TODO: Introduce statemachine
        if (playerState.IsRunning && playerState.IsGrounded)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = GetRandomRunningAudioClip();
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    //Source: https://www.sonigon.com/the-best-way-to-randomize-sounds-in-unity-3d-c/
    private int audioClipIndex;
    private int[] previousArray;
    private int previousArrayIndex;

    // The best random method
    public AudioClip GetRandomRunningAudioClip()
    {
        // Initialize
        if (previousArray == null)
        {
            // Sets the length to half of the number of AudioClips
            // This will round downwards
            // So it works with odd numbers like for example 3
            previousArray = new int[runningClips.Length / 2];
        }
        if (previousArray.Length == 0)
        {
            // If the the array length is 0 it returns null
            return null;
        }
        // Psuedo random remembering previous clips to avoid repetition
        do
        {
            audioClipIndex = Random.Range(0, runningClips.Length);
        } while (PreviousArrayContainsAudioClipIndex());
        // Adds the selected array index to the array
        previousArray[previousArrayIndex] = audioClipIndex;
        // Wrap the index
        previousArrayIndex++;
        if (previousArrayIndex >= previousArray.Length)
        {
            previousArrayIndex = 0;
        }

        // Returns the randomly selected clip
        return runningClips[audioClipIndex];
    }

    // Returns if the randomIndex is in the array
    private bool PreviousArrayContainsAudioClipIndex()
    {
        for (int i = 0; i < previousArray.Length; i++)
        {
            if (previousArray[i] == audioClipIndex)
            {
                return true;
            }
        }
        return false;
    }
}
