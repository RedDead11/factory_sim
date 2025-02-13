using System.Collections;
using System.Collections.Generic;
using ControlFreak2;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Interactions : MonoBehaviour
{
    [SerializeField] private float playerReach = 1.5f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private KeyCode CancelKey = KeyCode.Escape;

    public Transform examinePoint;

    private Item selectedItem;
    private bool canInteract = true;

    #region Singleton
    public static Interactions Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    private void Update()
    {
        if (canInteract)
        {
            if (CheckInteraction() && ControlFreak2.CF2Input.GetKeyDown(interactionKey))
            {
                
                DisableCharacter();
                selectedItem.Interact();
            }
        }

        if (ControlFreak2.CF2Input.GetKeyDown(CancelKey) && selectedItem != null)
        {
            selectedItem.StopInteract();
            EnableCharacter();

        }


    }

    public void DisableCharacter()
    {
        UIHandler.Instance.gamePanel.playerDot.SetActive(false);
        ToggleMessage(false);
        canInteract = false;

        this.gameObject.GetComponent<FirstPersonController>().enabled = false;
        ControlFreak2.CFCursor.lockState = CursorLockMode.Confined;
        ControlFreak2.CFCursor.visible = true;
    }

    public void EnableCharacter()
    {
        UIHandler.Instance.gamePanel.playerDot.SetActive(true);
        canInteract = true;

        this.gameObject.GetComponent<FirstPersonController>().enabled = true;
        ControlFreak2.CFCursor.lockState = CursorLockMode.Locked;
        ControlFreak2.CFCursor.visible = false;

    }


    private bool CheckInteraction()
    {
        bool check = false;
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.tag == "Interactable")
            {
                if (hit.collider.TryGetComponent<Item>(out selectedItem))
                {
                    selectedItem.EnableOutline();
                    ToggleMessage(true);
                    check = true;
                }
            }
            else
            {
                if (selectedItem != null)
                {
                    check = false;
                    selectedItem.DisableOutline();
                    selectedItem = null;
                }
            }

        }
        else
        {
            if (selectedItem != null)
            {
                check = false;
                selectedItem.DisableOutline();
                selectedItem = null;
            }
        }



        ToggleMessage(check);

        return check;
    }


    public void ToggleMessage(bool val = false)
    {
        UIHandler.Instance.gamePanel.messageText.gameObject.SetActive(val);
        if (val)
        {
            if (!CF2Input.IsInMobileMode())
            {
                UIHandler.Instance.gamePanel.messageText.text = $"{selectedItem.name}\n(Left Mouse Button)";
                return;
            }

            UIHandler.Instance.gamePanel.messageText.text = $"{selectedItem.name}";

        }
        UIHandler.Instance.gamePanel.btnUse.SetActive(val);

    }
}
