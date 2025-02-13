using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

// public enum Device
// {
//     editor,
//     window,
//     android,
//     macOS,
//     ios
// }

public class MainMenu : MonoBehaviour
{
    private string instructionsForPC =
              "1. WALK: W, A, S, D\n" +
              "2. RUN: HOLD SHIFT\n" +
              "3. JUMP: SPACE\n" +
              "4. LOOK AROUND: MOVE MOUSE\n" +
              "5. ZOOM: RIGHT MOUSE BUTTON\n" +
              "6. INTERACT: LEFT MOUSE BUTTON\n" +
              "7. OPEN NOTEPAD: N\n" +
              "8. TOGGLE FLASHLIGHT: F\n" +
              "9. OPEN MENU (IN GAME): ESC\n\n" +
              "SUPPORT EMAIL: FILIP@UX-DESIGNER.SE";
    private string instructionsForMobile =
         "1. USE THE ON-SCREEN CONTROLS.\n" +
         "2. FIND CLUES ABOUT WHAT HAPPEND\n" +
         "3. CIRCLE THE ANSWERS IN YOUR NOTEPAD\n\n" +
         "SUPPORT EMAIL: FILIP@UX-DESIGNER.SE";
    // private Device currentDevice;

    [Header("MainMenu Ref")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] Button playBtn;
    [SerializeField] Button helpBtn;
    [SerializeField] Button exitBtn;

    [Header("help panel ref")]
    [SerializeField] GameObject helpPanel;
    [SerializeField] TMP_Text helpText;
    [SerializeField] Button backBtn;


    [Header("Loading Screen Ref")]
    [SerializeField] GameObject loadingPanel;
    [SerializeField] TMP_Text loadingText;

    [Header("Clock ref")]
    [SerializeField] GameObject clock;





    void Awake()
    {
        // currentDevice = GetDeviceType();

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        helpText.text = instructionsForPC;
#elif UNITY_IOS || UNITY_ANDROID
            helpText.text = instructionsForMobile;
#endif
    }

    void Start()
    {
        AudioManager.instance.PlayMusic("Menu Music");
        mainMenuPanel.SetActive(true);
        helpPanel.SetActive(false);
        loadingPanel.SetActive(false);

        playBtn.onClick.AddListener(() => { StartLoading(); AudioManager.instance.PlaySound("Button Click"); });
        helpBtn.onClick.AddListener(() => { AudioManager.instance.PlaySound("Button Click"); helpPanel.SetActive(true); mainMenuPanel.SetActive(false); });
        exitBtn.onClick.AddListener(() => { AudioManager.instance.PlaySound("Button Click"); Application.Quit(); });
        backBtn.onClick.AddListener(() => { AudioManager.instance.PlaySound("Button Click"); helpPanel.SetActive(false); mainMenuPanel.SetActive(true); });
    }


    public void StartLoading()
    {
        mainMenuPanel.SetActive(false);
        clock.SetActive(false);
        StartCoroutine(LoadSceneAsync(1));
    }

    private IEnumerator LoadSceneAsync(int scene)
    {

        loadingPanel.SetActive(true);


        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);


        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingText.text = "Loading... " + (progress * 100f).ToString("F0") + "%";

            yield return null;
        }


        loadingPanel.SetActive(false);
    }


    //     Device GetDeviceType()
    //     {
    // #if UNITY_EDITOR
    //         return Device.editor;
    // #elif UNITY_STANDALONE_WIN
    //         return Device.window;
    // #elif UNITY_STANDALONE_OSX
    //         return Device.macOS;
    // #elif UNITY_IOS
    //         return Device.iOS;
    // #elif UNITY_ANDROID
    //         return Device.Android;
    // #else
    //         return Device.Unknown;
    // #endif
    //     }

}