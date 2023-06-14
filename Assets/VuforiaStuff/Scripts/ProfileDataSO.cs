using UnityEngine;

[CreateAssetMenu(fileName = "Profile Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 0)]
public class ProfileDataSO : ScriptableObject
{
    public enum URLType
    {
        normal,
        phone,
        email
    }


    [Header("General")]
    public string URL;
    public URLType urlType = URLType.normal;
    public Sprite ProfileSprite;
    [Space]
    public bool useProfileText;
    public string profileText;
    [Header("Type - Email")]
    public string EmailDirection;
    public string EmailSubject;
    public string EmailBody;

    public string getURL()
    {
        switch(urlType)
        {
            case URLType.normal:
                return URL;

            case URLType.phone:
                return string.Format("Tel://{0}", URL);

            case URLType.email:
                return string.Format("mailto:Tel:{0}?subject={1}&body={2}", EmailDirection, EmailSubject, EmailBody);
        }

        return "";
    }
}
