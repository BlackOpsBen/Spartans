using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public void PlayFootStepSound()
    {
        AudioManager.Instance.PlaySoundFromGroup(0, AudioManager.SFX_FOOTSTEPS, false);
    }
}
