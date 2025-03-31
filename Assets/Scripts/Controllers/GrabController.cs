using UnityEngine;
using UnityEngine.UI;


public class GrabController : MonoBehaviour
{
    [Header("Ray Settings")]
    public LayerMask grabbableLayer;
    public float grabDistance = 3f;
    public Transform holdPosition;

    [Header("UI Settings")]
    public GameObject GrabbableItemUI;
    public GameObject mousePanel;
    public GameObject actionPanel;
    public GameObject inventoryPanel;
    public GameObject hotBar;
    public GameObject actionInvetoryPanel;


    [Header("Physics Settings")]
    public float throwForce = 5f;


    [Header("Inventory Settings")]
    [SerializeField] private int inventorySize = 5;
    [SerializeField] private Transform[] inventorySlots;
    private ItemSO[] inventoryItems;

    [Header("Debug Settings")]
    private GameObject grabbedObject;
    private bool isHolding;
    private Collider playerCollider;
    private int currentSlot = 0;

    void Start()
    {
        playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
        GrabbableItemUI.SetActive(false);
        inventoryItems = new ItemSO[inventorySize];
        UpdateInventoryUI(); // Inicializa la UI
    }

    void Update()
    {
        CheckInteractableObject();
        HandleSlotSelection();

        if (!isHolding && Input.GetMouseButtonDown(0))
        {
            if (inventoryItems[currentSlot] != null)
            {
                GrabFromInventory(currentSlot);
            }
            else
            {
                TryGrab();
            }
        }

        if (isHolding && Input.GetMouseButtonDown(1))
        {
            Release();
        }

        if (isHolding && Input.GetKeyDown(KeyCode.E))
        {
            StoreToInventory();
        }
    }

    private void HandleSlotSelection()
    {
        int previousSlot = currentSlot;

        if (Input.GetKeyDown(KeyCode.Alpha1)) currentSlot = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentSlot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentSlot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) currentSlot = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) currentSlot = 4;

        if (previousSlot != currentSlot)
        {
            UpdateInventoryUI();
        }
    }

    void CheckInteractableObject()
    {
        RaycastHit hit;
        bool canGrab = Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out hit,
            grabDistance,
            grabbableLayer
        );

        if (canGrab && !isHolding)
        {
            GrabbableItemUI.SetActive(true);
            mousePanel.SetActive(true);
            actionPanel.SetActive(false);
        }
        else if (isHolding)
        {
            GrabbableItemUI.SetActive(true);
            mousePanel.SetActive(false);
            actionPanel.SetActive(true);
        }
        else
        {
            GrabbableItemUI.SetActive(false);
        }
    }

    void GrabFromInventory(int slot)
    {
        if (isHolding)
        {
            Debug.Log("Ya tienes un objeto en la mano");
            return;
        }

        ItemSO itemSO = inventoryItems[slot];
        if (itemSO == null || itemSO.prefab == null)
        {
            Debug.Log("Slot vacío o prefab faltante");
            return;
        }

        GameObject invObject = Instantiate(
            itemSO.prefab,
            holdPosition.position,
            holdPosition.rotation
        );
        invObject.transform.SetParent(holdPosition);

        Rigidbody rb = invObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        Collider objCollider = invObject.GetComponent<Collider>();
        if (objCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, objCollider, true);
        }

        grabbedObject = invObject;
        isHolding = true;
        inventoryItems[slot] = null;

        UpdateInventoryUI();
    }

    private void StoreToInventory()
    {
        if (!isHolding)
        {
            Debug.Log("No hay objeto para guardar");
            return;
        }

        Item itemComponent = grabbedObject.GetComponent<Item>();
        if (itemComponent == null || itemComponent.itemSO == null)
        {
            Debug.Log("El objeto no es guardable o no tiene itemSo");
            return;
        }

        int emptySlot = System.Array.FindIndex(inventoryItems, item => item == null);
        if (emptySlot == -1)
        {
            Debug.Log("Inventario lleno");
            return;
        }

        // Crear y guardar los datos del item
        inventoryItems[emptySlot] = itemComponent.itemSO;

        Destroy(grabbedObject);
        grabbedObject = null;
        isHolding = false;

        UpdateInventoryUI();
    }

    void TryGrab()
    {
        RaycastHit hit;
        if (Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out hit,
            grabDistance,
            grabbableLayer))
        {
            grabbedObject = hit.collider.gameObject;
            GrabObject();
        }
    }

    void GrabObject()
    {
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        Collider objCollider = grabbedObject.GetComponent<Collider>();
        if (objCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, objCollider, true);
        }

        grabbedObject.transform.SetParent(holdPosition);
        grabbedObject.transform.localPosition = Vector3.zero;
        isHolding = true;

        CheckInteractableObject();
    }

    void Release()
    {
        if (grabbedObject == null) return;

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
        }

        Collider objCollider = grabbedObject.GetComponent<Collider>();
        if (objCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, objCollider, false);
        }

        grabbedObject.transform.SetParent(null);
        grabbedObject = null;
        isHolding = false;

        CheckInteractableObject();
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            Transform slotTransform = hotBar.transform.GetChild(0).GetChild(i);
            if (slotTransform != null)
            {
                Transform itemImageTransform = slotTransform.Find("ItemImage");
                Image slotImage = slotTransform.GetComponent<Image>();

                if (itemImageTransform != null && slotImage != null)
                {
                    Color slotColor = slotImage.color;
                    slotColor.a = (i == currentSlot) ? 1.0f : 0.31f;
                    slotImage.color = slotColor;

                    Image itemImage = itemImageTransform.GetComponent<Image>();
                    if (itemImage != null)
                    {
                        if (inventoryItems[i] != null)
                        {
                            itemImage.sprite = inventoryItems[i].icon;
                            itemImageTransform.gameObject.SetActive(true);
                        }
                        else
                        {
                            itemImageTransform.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }

        if (actionInvetoryPanel != null)
        {
            actionInvetoryPanel.SetActive(inventoryItems[currentSlot] != null);
        }
    }
}