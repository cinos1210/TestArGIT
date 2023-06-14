using UnityEngine;
using UnityEngine.UI;

public class SimpleDetection : MonoBehaviour
{
    [Header("Setting:")]
    [SerializeField]private Image DetectionImg;
    [Space]
    [SerializeField] private Color colorOn;
    [SerializeField] private Color ColorOff;

    private void Start()
    {
        EnableDetection(false);
    }

    public void EnableDetection (bool trigger)
    {
        DetectionImg.color = trigger ? colorOn : ColorOff;
    }
}
