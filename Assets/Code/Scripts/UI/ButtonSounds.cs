using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public void ClickSound()
    {
        AudioManager.instance.PlaySFX("Select");
    }
}
