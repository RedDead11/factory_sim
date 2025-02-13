using System.Collections;

using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    public TMP_Text txtTitle;
    public Button btnStay;
    public Button btnLeave;

    public GameObject pnlBlack;

    private void OnEnable()
    {
        FindObjectOfType<Interactions>().DisableCharacter();

    }

    private void Start()
    {
        btnStay.onClick.AddListener(() =>
        {
            FindObjectOfType<Interactions>().EnableCharacter();
            this.gameObject.SetActive(false);

        });

        btnLeave.onClick.AddListener(() =>
        {
            btnLeave.enabled = false;
            FindObjectOfType<Interactions>().enabled = false;
            LeavFlow();
        });
    }

    private void LeavFlow()
    {
        SceneManager.LoadScene(0);
    }
}
