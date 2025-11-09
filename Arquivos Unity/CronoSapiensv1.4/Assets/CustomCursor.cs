using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [Header("Configurações do Cursor")]
    public Texture2D cursorTexture; // sua imagem
    public Vector2 hotspot = Vector2.zero; // ponto ativo do cursor
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        // Define o cursor personalizado
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
    }

    void OnDisable()
    {
        // Restaura o cursor padrão quando o objeto for desativado
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
