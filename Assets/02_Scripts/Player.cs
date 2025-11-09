using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioManager audioji;
    public Inventory inventory;

    [Header("MOVEMENT")]
    [SerializeField] public Transform cam;
    [SerializeField] public float movementSpeed, walkSpeed, runSpeed;
    [SerializeField] public float mouseSpeed;
    [SerializeField] public float horizontalRotation, verticalRotation;
    [SerializeField] public Rigidbody rb;

    [Header("RAYCAST")]
    public float rayDistance = 3f;
    public Transform pickedObject;
    public Transform playerHands;
    public LayerMask rayDetectoionLayer;

    [Header("UI")]
    public TextMeshProUGUI objectNameText;
    public TextMeshProUGUI objectTypeText;
    public TextMeshProUGUI objectPriceText;
    public GameObject infoBg;

    [Header("UI INTERACTION")]
    public GameObject interactPromptUI;
    private bool nearNPC = false;
    private NPC nearbyNPC;

    [Header("OPEN DOORS")]
    public GameObject nearNormalDoor;
    public Animator openNormalDoor;
    public bool nearNormal = false;
    private bool doorIsOpen = false;

    [Header("DIALOGUE")]
    [SerializeField] public DialogueManager dialoguee;
    [SerializeField] public RigidbodyConstraints ogRb;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>().transform;
        movementSpeed = walkSpeed;
        ogRb = rb.constraints;
    }

    private void Update()
    {
        if (dialoguee.isTalking)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            return;
        }
        else
        {
            rb.constraints = ogRb;
        }

        Movement();
        Raycast();

        if (inventory.canCheckout && Input.GetKeyDown(KeyCode.Q))
        {
            inventory.Boleta();
            inventory.purchaseMade = true;
            if (inventory.checkoutUIPrompt != null) inventory.checkoutUIPrompt.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            nearNormalDoor = other.gameObject;
            openNormalDoor = other.GetComponent<Animator>();
            nearNormal = true;
            interactPromptUI.SetActive(true);
        }

        if (other.CompareTag("NPC"))
        {
            nearbyNPC = other.GetComponent<NPC>();
            if (nearbyNPC != null)
            {
                nearNPC = true;
                interactPromptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            nearNormalDoor = null;
            openNormalDoor = null;
            nearNormal = false;
            if (!nearNPC)
            {
                interactPromptUI.SetActive(false);
            }
        }

        if (other.CompareTag("NPC"))
        {
            nearNPC = false;
            nearbyNPC = null;
            if (!nearNormal)
            {
                interactPromptUI.SetActive(false);
            }
        }
    }

    public void Movement()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSpeed;

        float keyboardX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        float keyboardY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        horizontalRotation += mouseX;
        verticalRotation -= mouseY;

        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, horizontalRotation, transform.localEulerAngles.z);
        cam.localEulerAngles = new Vector3(verticalRotation, cam.localEulerAngles.y, cam.localEulerAngles.z);

        transform.Translate(keyboardX, 0, keyboardY);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = runSpeed;
        }
        else
        {
            movementSpeed = walkSpeed;
        }

        if (Input.GetKeyDown(KeyCode.E) && nearNormal == true)
        {
            doorIsOpen = !doorIsOpen;
            openNormalDoor.SetBool("DoorOpen", doorIsOpen);
        }
    }

    public void Raycast()
    {
        Debug.DrawRay(cam.position, cam.forward * rayDistance, Color.blue);

        bool isHitting = Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, rayDistance, rayDetectoionLayer);
        bool isHittingItem = isHitting && hit.transform.CompareTag("Item");

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pickedObject != null)
            {
                DropItem();
            }
            else
            {
                if (isHittingItem)
                {
                    PickItem(hit.transform);
                    audioji.sfxSound.resource = audioji.pickupSound;
                    audioji.sfxSound.Play();
                }
            }
        }

        if (isHittingItem)
        {
            Item item = hit.transform.GetComponent<Item>();

            objectNameText.text = item.itemTemplate.itemName;
            objectTypeText.text = item.itemTemplate.itemType;
            objectPriceText.text = "$" + item.itemTemplate.itemPrice.ToString();
            infoBg.SetActive(true);
            if (interactPromptUI != null && !nearNormal && !nearNPC)
            {
                interactPromptUI.SetActive(true);
            }
        }
        else
        {
            objectNameText.text = null;
            objectTypeText.text = null;
            objectPriceText.text = null;
            infoBg.SetActive(false);
            if (interactPromptUI != null && !nearNormal && !nearNPC)
            {
                interactPromptUI.SetActive(false);
            }
        }
    }

    public void PickItem(Transform itemToPick)
    {
        pickedObject = itemToPick;
        pickedObject.GetComponent<Rigidbody>().isKinematic = true;
        pickedObject.SetParent(playerHands);
        pickedObject.localPosition = Vector3.zero;
    }

    public void DropItem()
    {
        pickedObject.SetParent(null);
        pickedObject.GetComponent<Rigidbody>().isKinematic = false;
        pickedObject = null;
    }
}
