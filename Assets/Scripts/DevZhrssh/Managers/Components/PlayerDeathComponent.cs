using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevZhrssh.Managers.Components
{
    public class PlayerDeathComponent : MonoBehaviour
    {
        // Other scripts will subscribe to this delegate
        public delegate void PlayerDeathCallback();
        public event PlayerDeathCallback onPlayerDeathCallback;

        // This function is to be called by the player
        public void OnPlayerDeath()
        {
            // Calls all the function subscribed to player death
            if (onPlayerDeathCallback != null)
                onPlayerDeathCallback.Invoke();
        }
    }
}
