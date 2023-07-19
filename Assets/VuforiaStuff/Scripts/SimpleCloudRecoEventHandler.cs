using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Vuforia;

public class SimpleCloudRecoEventHandler : MonoBehaviour
{
    CloudRecoBehaviour mCloudRecoBehaviour;
    bool mIsScanning = false;
    string mTargetMetadata = "";

    [Header("Vuforia")]
    public ImageTargetBehaviour ImageTargetTemplate;

    [Header("MetaData")]
    [SerializeField] private int _contentIndex;
    [SerializeField] private string _contentTitle;
    [SerializeField] private string _contentDescription;
    [Space]
    [SerializeField] private Transform _contentParent;
    [SerializeField] private MetaDataSo[] _metaDataSO;

    [Header("UI")]
    [SerializeField] private UnityEngine.UI.Image _scanningImg;
    [SerializeField] private Color _scanningColorTrue;
    [SerializeField] private Color _scanningColorFalse;
    [Space]
    [SerializeField] private TextMeshProUGUI _titleTxt;
    [SerializeField] private TextMeshProUGUI _descriptionTxt;
    [Space]
    [SerializeField] private Button _resetBtn;

    private GameObject _content;

    // Register cloud reco callbacks
    void Awake()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }
    //Unregister cloud reco callbacks when the handler is destroyed
    void OnDestroy()
    {
        mCloudRecoBehaviour.UnregisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.UnregisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.UnregisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.UnregisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }

    private void Start()
    {
        ResetMetaData();
    }

     public void OnInitialized(CloudRecoBehaviour cloudRecoBehaviour)
    {
        Debug.Log("Cloud Reco initialized");
    }

    public void OnInitError(CloudRecoBehaviour.InitError initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }

    public void OnUpdateError(CloudRecoBehaviour.QueryError updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());

    }

    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;

        _scanningImg.color = mIsScanning ? _scanningColorTrue : _scanningColorFalse;

        if (scanning)
        {
            // Clear all known targets
        }else
        {
            _resetBtn.gameObject.SetActive(true);
        }
    }

    // Here we handle a cloud target recognition event
    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult )
    {
        // Store the target metadata
        mTargetMetadata = cloudRecoSearchResult.MetaData;

        // Stop the scanning by disabling the behaviour
        mCloudRecoBehaviour.enabled = false;

        Debug.LogFormat("<color=green>METADATA:</color> {0}", mTargetMetadata);

        ParseMetaData(mTargetMetadata);
        ChangeContent(true);

        mCloudRecoBehaviour.EnableObservers(cloudRecoSearchResult, ImageTargetTemplate.gameObject);
    }

//      void OnGUI() {
//       // Display current 'scanning' status
//       GUI.Box (new Rect(100,100,200,50), mIsScanning ? "Scanning" : "Not scanning");
//       // Display metadata of latest detected cloud-target
//       GUI.Box (new Rect(100,200,200,50), "Metadata: " + mTargetMetadata);
//       // If not scanning, show button
//       // so that user can restart cloud scanning
//       if (!mIsScanning) {
//           if (GUI.Button(new Rect(100,300,200,50), "Restart Scanning")) {
//           // Reset Behaviour
//           mCloudRecoBehaviour.enabled = true;
//           mTargetMetadata="";
//           }
//       }
//   }

    public void ResetMetaData()
    {
        mCloudRecoBehaviour.enabled = true;
        mTargetMetadata = "";

        _scanningImg.color = _scanningColorFalse;
        _titleTxt.text = "-";
        _descriptionTxt.text = "-";
        _resetBtn.gameObject.SetActive(false);

        ChangeContent(false);
    }

    private void ParseMetaData(string metaData) 
    {
        string[] tempMetadata = metaData.Split('_');

        _contentIndex = int.Parse(tempMetadata[0]);
        _contentTitle = tempMetadata[1];
        _contentDescription = tempMetadata[2];

        _titleTxt.text = _contentTitle;
        _descriptionTxt.text = _contentDescription;
    }

    private void ChangeContent(bool show)
    {
        if (show)
        {
            _content = Instantiate(_metaDataSO[_contentIndex].content, _contentParent);
        }
        else
        {
            Destroy(_content);
        }
    }


}
