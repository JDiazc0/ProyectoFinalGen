using UnityEngine;
using UnityEngine.UI; // Necesario para UI

public class GrabController : MonoBehaviour
{
    [Header("Ray Settings")]
    public LayerMask grabbableLayer;
    public float grabDistance = 3f;
    public Transform holdPosition;

    [Header("UI Settings")]
    public GameObject interactionPanel;
    public GameObject mousePanel;
    public GameObject actionPanel;

    [Header("Physics Settings")]
    public float throwForce = 5f;

    private GameObject grabbedObject;
    private bool isHolding;
    private Collider playerCollider;

    void Start()
    {
        playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
        interactionPanel.SetActive(false);
    }

    void Update()
    {
        CheckInteractableObject();

        if (!isHolding && Input.GetMouseButtonDown(0))
        {
            TryGrab();
        }

        if (isHolding && Input.GetMouseButtonUp(0))
        {
            Release();
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
            interactionPanel.SetActive(true);
            mousePanel.SetActive(true);
            actionPanel.SetActive(false);
        }
        else if (isHolding)
        {
            interactionPanel.SetActive(true);
            mousePanel.SetActive(false);
            actionPanel.SetActive(true);
        }
        else
        {
            interactionPanel.SetActive(false);
        }
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
        grabbedObject.transform.localRotation = Quaternion.identity;

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
}
