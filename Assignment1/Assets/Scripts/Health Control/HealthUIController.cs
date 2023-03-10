using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    public PlayerMovement player;
    public Image[] hearts;

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.health)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
