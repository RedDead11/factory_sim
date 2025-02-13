using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{

    public new string name;
    public string uIName;
    private Outline outline;

    public void Awake()
    {
        Initilized();
    }
    public void Initilized()
    {
        gameObject.tag = "Interactable";
        outline = transform.GetOrAddComponent<Outline>();
        DisableOutline();
    }

    virtual public void Interact()
    {
        UIHandler.Instance.gamePanel.pickText.text = $"You picked {this.uIName}";
        UIHandler.Instance.gamePanel.pickText.gameObject.SetActive(true);
    }

    virtual public void StopInteract()
    {
        UIHandler.Instance.gamePanel.pickText.gameObject.SetActive(false);
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }
    public void EnableOutline()
    {
        outline.enabled = true;
    }
}
