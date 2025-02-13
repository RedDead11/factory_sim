using System;
using UnityEngine;
using UnityEngine.UI;

public class Notes : MonoBehaviour
{
    public RectTransform imageTransform;
    public Animator animationCircle;
    public Button[] clickableAreas;

    public string choice;

    int currentInteractable = 0;

    private void Start()
    {
        choice = "";


        for (int i = 0; i < clickableAreas.Length; i++)
        {
            int ind = i;
            clickableAreas[i].onClick.AddListener(() =>
            {
                ChangeCirclePosition(ind); currentInteractable = ind;
                choice = clickableAreas[ind].name;
                ConfirmAllChecked();
            });
        }
    }

    void ChangeCirclePosition(int index)
    {
        AudioManager.instance.PlaySound("Select Option");
        animationCircle.Play("CauseOfDeathCircle", -1, 0);
        clickableAreas[currentInteractable].interactable = true;
        clickableAreas[index].interactable = false;
        currentInteractable = index;
        animationCircle.transform.position = clickableAreas[index].transform.position;


    }

    public void ConfirmAllChecked()
    {
        var i = 0;
        // var notepad = FindAnyObjectByType<Notepad>();
        // var notes = notepad.notes;

        // foreach (var note in notes)
        // {
        //     if (note.choice.Length == 0)
        //     {
        //         return;
        //     }
        //     i++;
        // }

        if (i >= 2)
            UIHandler.Instance.gamePanel.taskText.gameObject.SetActive(true);

    }

}



