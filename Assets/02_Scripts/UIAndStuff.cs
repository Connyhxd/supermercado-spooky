using TMPro;
using UnityEngine;

public class UIAndStuff : MonoBehaviour
{
    public Animator listAnim;
    public GameObject listObj;

    public GameObject pauseMenu;
    public GameObject pauseButton;
    bool isMenuOpen;

    public TextMeshProUGUI funFactText;
    private string[] halloweenFacts;

    bool listActive;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            if(!listActive)
            {
                listAnim.SetTrigger("Up");
                listAnim.SetBool("PorQueNoEstaFuncionando", true);
                listAnim.SetBool("ListaNoMove", false);
                listActive = true;

                int randomIndex = Random.Range(0, halloweenFacts.Length);
                funFactText.text = halloweenFacts[randomIndex];

            }
            else
            {
                listAnim.SetTrigger("Down");
                listAnim.SetBool("ListaNoMove", true);
                listAnim.SetBool("PorQueNoEstaFuncionando", false);
                listActive = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }

    }


    private void PauseMenu()
    {
        isMenuOpen = !isMenuOpen;

        if (isMenuOpen)
        {
            pauseMenu.SetActive(true);
            pauseButton.SetActive(false);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            pauseMenu.SetActive(false);
            pauseButton.SetActive(true);
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
