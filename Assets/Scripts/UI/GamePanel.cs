using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public GameObject playerDot;

    [Header("UI Text Message")]
    public TMP_Text taskText;
    public TMP_Text messageText;
    public TMP_Text pickText;


    [Header("UI Buttons")]
    public Button btnControls;
    public GameObject btnUse;
    public GameObject btnExit;

    private void Start()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        // if (PlayerPrefs.GetInt("isFirstTime", 1) == 1)
        // {
            FindAnyObjectByType<Interactions>().DisableCharacter();
            btnControls.gameObject.SetActive(true);

            btnControls.onClick.AddListener(() =>
            {
                FindAnyObjectByType<Interactions>().EnableCharacter();
                btnControls.gameObject.SetActive(false);

            });

        //     PlayerPrefs.SetInt("isFirstTime", 0);
        // }
        // else
        // {
        //     btnControls.gameObject.SetActive(false);
        //     FindAnyObjectByType<Interactions>().EnableCharacter();
        // }
#elif UNITY_IOS || UNITY_ANDROID
        FindAnyObjectByType<Interactions>().EnableCharacter();
        btnControls.gameObject.SetActive(false);
#endif
    }



}
