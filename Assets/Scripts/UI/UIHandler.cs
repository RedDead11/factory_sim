using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{

    public ExaminePanel examinePanel;
    public GamePanel gamePanel;
    public PausePanel pausePanel;

    #region Singleton
    public static UIHandler Instance { get; private set; }

    private void Awake()
    {
        Time.timeScale = 1;
        // AudioManager.instance.PlayMusic("Outdoor Sound");
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    #endregion


    public void LoadPanel()
    {

    }

    public void DisableAllPanels()
    {
        examinePanel.gameObject.SetActive(false);
        gamePanel.gameObject.SetActive(false);
    }

}

