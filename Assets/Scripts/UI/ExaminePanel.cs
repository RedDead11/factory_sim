using Paroxe.PdfRenderer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ExaminePanel : MonoBehaviour
{
    public TMP_Text txtItemName;
    public GameObject txtExitPC;

    [Header("Buttons")]
    public GameObject btnExitMobile;
    public Button btnPdf;
    public Button btnVideo;

    [Header("Panels")]
    public GameObject pdfPanel;
    public GameObject videoPanel;

    private void Start()
    {
        btnPdf.onClick.AddListener(delegate { pdfPanel.SetActive(true); });
        btnVideo.onClick.AddListener(delegate { videoPanel.SetActive(true); });
    }



    private void OnEnable()
    {
        btnPdf.gameObject.SetActive(false);
        btnVideo.gameObject.SetActive(false);
        btnExitMobile.SetActive(false);
        txtExitPC.SetActive(false);
    }

    private void OnDisable()
    {
        btnExitMobile.SetActive(false);
        txtExitPC.SetActive(false);
    }

    public void ShowPanel(string itemName, bool hasPdf, bool hasVideo, PDFAsset pdfAsset = null, VideoClip clip = null)
    {
        txtItemName.text = itemName;
        gameObject.SetActive(true);

        if (hasPdf)
        {
            btnPdf.gameObject.SetActive(hasPdf);
            pdfPanel.GetComponentInChildren<PDFViewer>().PDFAsset = pdfAsset;
        }

        if (hasVideo)
        {
            btnVideo.gameObject.SetActive(hasVideo);
            videoPanel.GetComponentInChildren<VideoPlayer>().clip = clip;
        }


        // UNITY_EDITOR
#if UNITY_IOS || UNITY_ANDROID
        btnExitMobile.SetActive(true);
#else
        txtExitPC.SetActive(true);
#endif

    }

}
