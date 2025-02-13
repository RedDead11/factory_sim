using DG.Tweening;
using Paroxe.PdfRenderer;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;


public class ExamineItem : Item
{
    private Vector3 originalPosition, originalRotation, originalScale;
    [SerializeField] Vector3 examineScale = Vector3.one;

    [Header("PDF")]
    [SerializeField] bool hasPdf;
    [SerializeField] PDFAsset pDFAsset;

    [Header("Video")]
    [SerializeField] public bool hasVideo;
    [SerializeField] VideoClip videoAsset;

    private void Start()
    {
        Initilized();
        transform.GetOrAddComponent<RotateItem>();
        transform.GetOrAddComponent<RotateItem>().enabled = false;
        originalPosition = transform.position;
        originalRotation = transform.rotation.eulerAngles;
        originalScale = transform.localScale;


    }

    public override void Interact()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
        this.gameObject.transform.localScale = examineScale;
        foreach (MeshRenderer item in gameObject.GetComponentsInChildren<MeshRenderer>())
            item.gameObject.layer = LayerMask.NameToLayer("Interactable");

        StartExamine();
    }

    public override void StopInteract()
    {
        foreach (MeshRenderer item in gameObject.GetComponentsInChildren<MeshRenderer>())
            item.gameObject.layer = LayerMask.NameToLayer("Default");

        base.StopInteract();
        StopExamine();
    }

    public void StartExamine()
    {
        DisableOutline();

        UIHandler.Instance.examinePanel.ShowPanel(uIName, hasPdf, hasVideo, pDFAsset, videoAsset);
        transform.DOMove(Interactions.Instance.examinePoint.position, 1).SetEase(Ease.OutSine).OnComplete(() =>
        {
            transform.GetOrAddComponent<RotateItem>().enabled = true;
        });
        transform.DORotate(Interactions.Instance.examinePoint.rotation.eulerAngles, 1).SetEase(Ease.OutSine);
        transform.DOScale(examineScale, 1).SetEase(Ease.OutSine);
    }

    public void StopExamine()
    {
        UIHandler.Instance.examinePanel.gameObject.SetActive(false);
        transform.DOMove(originalPosition, 1).SetEase(Ease.OutSine).OnComplete(() =>
        {
            transform.GetOrAddComponent<RotateItem>().enabled = false;
        });
        transform.DORotate(originalRotation, 1).SetEase(Ease.OutSine);
        transform.DOScale(originalScale, 1).SetEase(Ease.OutSine);

    }
}
