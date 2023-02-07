using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip soundEffect;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        EventTrigger trigger = GetComponent<EventTrigger>();

        if (trigger == null)
        {
            trigger = gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { PlaySound(); });
        trigger.triggers.Add(entry);
    }

    private void PlaySound()
    {
        audioSource.PlayOneShot(soundEffect);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound();
    }
}
