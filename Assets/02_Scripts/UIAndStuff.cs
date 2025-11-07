using TMPro;
using UnityEngine;

public class UIAndStuff : MonoBehaviour
{
    public AudioManager audioji;

    public Animator listAnim;
    public GameObject listObj;

    public GameObject pauseMenu;
    public GameObject pauseButton;
    bool isMenuOpen;

    public TextMeshProUGUI funFactText;
    public string[] halloweenFacts;

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
                audioji.sfxSound.resource = audioji.listOpenSound;
                audioji.sfxSound.Play();


            }
            else
            {
                listAnim.SetTrigger("Down");
                listAnim.SetBool("ListaNoMove", true);
                listAnim.SetBool("PorQueNoEstaFuncionando", false);
                listActive = false;
                audioji.sfxSound.resource = audioji.listCloseSound;
                audioji.sfxSound.Play();
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

            int randomIndex = Random.Range(0, halloweenFacts.Length);
            funFactText.text = halloweenFacts[randomIndex];

            audioji.sfxSound.resource = audioji.pauseOpenSound;
            audioji.sfxSound.Play();

        }
        else
        {
            pauseMenu.SetActive(false);
            pauseButton.SetActive(true);
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            audioji.sfxSound.resource = audioji.pauseCloseSound;
            audioji.sfxSound.Play();
        }
    }
}
