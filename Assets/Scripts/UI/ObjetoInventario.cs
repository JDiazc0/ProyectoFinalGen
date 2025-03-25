using UnityEngine;

[System.Serializable]
public class ObjetoInventario
{
    public string nombre;
    public Sprite icono; // Imagen del objeto

    public ObjetoInventario(string nombre, Sprite icono)
    {
        this.nombre = nombre;
        this.icono = icono;
    }
}

