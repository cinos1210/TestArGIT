using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileButton : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField]private ProfileDataSO ProfileData;

    [Header("References")]
    [SerializeField]private TextMeshProUGUI ProfileTxt;
    [SerializeField]private Image ProfileImg;

    private void Start()
    {
        ProfileImg.sprite = ProfileData.ProfileSprite;
        if(ProfileData.useProfileText && ProfileTxt != null)
        {
            ProfileTxt.text = ProfileData.profileText;
        }
        //Consume
    }

    public void Execute()
    {
        Application.OpenURL(ProfileData.getURL());
    }

}
