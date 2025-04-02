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

        // --- MODIFICACIÓN: Priorizamos agarrar del mundo si hay un objeto frente al jugador ---
        if (!isHolding && Input.GetMouseButtonDown(0))
        {
            // Verificamos si hay un objeto en frente que podamos agarrar
            if (CanGrabObjectInFront(out RaycastHit hitInfo))
            {
                grabbedObject = hitInfo.collider.gameObject;
                Debug.Log("Objeto detectado: " + grabbedObject.name);
                GrabObject();
            }
            else
            {
                // Si NO hay objeto para agarrar, revisamos si el slot actual tiene algo
                if (inventoryItems[currentSlot] != null)
                {
                    GrabFromInventory(currentSlot);
                }
            }
        }
        // -------------------------------------------------------------------------------------

        // Soltar objeto con clic derecho
        if (isHolding && Input.GetMouseButtonDown(1))
        {
            Release();
        }

        // Guardar objeto en el inventario con tecla E
        if (isHolding && Input.GetKeyDown(KeyCode.E))
        {
            StoreToInventory();
        }
    }

    private void HandleSlotSelection()
    {
        int previousSlot = currentSlot;
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentSlot = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) currentSlot = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) currentSlot = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) currentSlot = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha5)) currentSlot = 4;

        if (previousSlot != currentSlot)
        {
            Debug.Log("Slot cambiado a: " + currentSlot);
            UpdateInventoryUI();
        }
    }

    void CheckInteractableObject()
    {
        // Se podría usar CanGrabObjectInFront() para la detección de la UI, pero aquí
        // mantenemos la lógica original.
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

    /// <summary>
    /// Comprueba si hay un objeto "grabbable" en frente del jugador,
    /// devolviendo true y el RaycastHit correspondiente si se detecta.
    /// </summary>
    private bool CanGrabObjectInFront(out RaycastHit hitInfo)
    {
        return Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out hitInfo,
            grabDistance,
            grabbableLayer
        );
    }

    void GrabObject()
    {
        if (grabbedObject == null)
            return;

        Debug.Log("Agarrando objeto: " + grabbedObject.name);

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
        if (grabbedObject == null)
            return;

        Debug.Log("Soltando objeto: " + grabbedObject.name);

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

    void GrabFromInventory(int slot)
    {
        if (isHolding)
        {
            Debug.LogWarning("Ya tienes un objeto en la mano");
            return;
        }

        ItemSO itemSO = inventoryItems[slot];
        if (itemSO == null || itemSO.prefab == null)
        {
            Debug.LogWarning("Slot vacío o prefab faltante en slot " + slot);
            return;
        }

        Debug.Log("Instanciando objeto desde inventario - Slot " + slot + " con item: " + itemSO.itemName);
        GameObject invObject = Instantiate(
            itemSO.prefab,
            holdPosition.position,
            itemSO.prefab.transform.rotation
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
            Debug.LogWarning("No hay objeto para guardar");
            return;
        }

        Item itemComponent = grabbedObject.GetComponent<Item>();
        if (itemComponent == null || itemComponent.itemSO == null)
        {
            Debug.LogWarning("El objeto no es guardable o no tiene ItemSO");
            return;
        }

        int emptySlot = System.Array.FindIndex(inventoryItems, item => item == null);
        if (emptySlot == -1)
        {
            Debug.LogWarning("Inventario lleno");
            return;
        }

        Debug.Log("Guardando: " + itemComponent.itemSO.itemName + " en slot " + emptySlot);
        inventoryItems[emptySlot] = itemComponent.itemSO;

        Destroy(grabbedObject);
        grabbedObject = null;
        isHolding = false;

        UpdateInventoryUI();
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



