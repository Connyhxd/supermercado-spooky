using UnityEngine;

public class UIAndStuff : MonoBehaviour
{
    public Animator listAnim;
    public GameObject listObj;

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
            }
            else
            {
                listAnim.SetTrigger("Down");
                listAnim.SetBool("ListaNoMove", true);
                listAnim.SetBool("PorQueNoEstaFuncionando", false);
                listActive = false;
            }
        }
    }
}
