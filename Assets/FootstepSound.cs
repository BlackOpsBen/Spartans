using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public int soundIndex = 0;

    public void PlayFootStepSound()
    {
        AudioManager.Instance.PlaySoundFromGroup(0, soundIndex, false);
    }
}
