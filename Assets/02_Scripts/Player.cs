using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [Header("MOVEMENT")]
    [SerializeField] public Transform cam;
    [SerializeField] public float movementSpeed, walkSpeed, runSpeed;
    [SerializeField] public float mouseSpeed;
    [SerializeField] public float horizontalRotation, verticalRotation;

    [Header("JUMP")]
    [SerializeField] public Rigidbody rb;
    [SerializeField] public float jumpForce;
    [SerializeField] public bool canJump = true;

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

    [Header("OPEN DOORS")]
    public GameObject nearNormalDoor;
    public Animator openNormalDoor;
    public bool nearNormal = false;
    private bool doorIsOpen = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>().transform;
        movementSpeed = walkSpeed;
    }

    private void Update()
    {
        Movement();
        Raycast();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            nearNormalDoor = other.gameObject;
            openNormalDoor = other.GetComponent<Animator>();
            nearNormal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            nearNormalDoor = null;
            openNormalDoor = null;
            nearNormal = false;
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

        if (canJump == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
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
        }
        else
        {
            objectNameText.text = null;
            objectTypeText.text = null;
            objectPriceText.text = null;
            infoBg.SetActive(false);
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
