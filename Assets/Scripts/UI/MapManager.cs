using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject mapUI;
    public Image firstFloorMap, secondFloorMap, undergroundMap;
    private int currentFloor = 1;

    void Start()
    {
        mapUI.SetActive(false);

        //  Asegúrate que todos los mapas estén desactivados al inicio
        firstFloorMap.gameObject.SetActive(false);
        secondFloorMap.gameObject.SetActive(false);
        undergroundMap.gameObject.SetActive(false);

        UpdateMap(); // Se activa el mapa correcto si es necesario
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapUI.SetActive(!mapUI.activeSelf);
        }
    }
// IMPORTANTE: Asegurar que ChangeFloor sea público
    public void ChangeFloor(int newFloor)
    {
        if (currentFloor != newFloor)
        {
            currentFloor = newFloor;
            UpdateMap();
        }
    }

    void UpdateMap()
    {
        firstFloorMap.gameObject.SetActive(false);
        secondFloorMap.gameObject.SetActive(false);
        undergroundMap.gameObject.SetActive(false);

        if (currentFloor == 1)
        {
            firstFloorMap.gameObject.SetActive(true);
        }
        else if (currentFloor == 2)
        {
            secondFloorMap.gameObject.SetActive(true);
        }
        else if (currentFloor == 0)
        {
            undergroundMap.gameObject.SetActive(true);
        }
    }
}
