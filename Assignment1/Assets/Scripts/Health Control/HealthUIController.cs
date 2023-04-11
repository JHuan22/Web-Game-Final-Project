using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour, IObserver
{
    public PlayerMovement player;
    public Image[] hearts;
    public Subject subject;

    private void OnEnable() {
        subject.AddObserver(this);
    }
    private void OnDisable() {
        subject.RemoveObserver(this);
    }
    public void OnNotify(int health)
    {
        Debug.Log(health);
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

    private void Update()
    {
    }
}
