using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;

    private SpriteRenderer sr;
    private Color baseColor;
    private Color hoverColor = new Color(0.6f, 0.9f, 1f, 1f);    // biru muda
    private Color selectedColor = new Color(1f, 1f, 0.5f, 1f);   // kuning muda

    private GameManager gm;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit != null && hit.gameObject == gameObject)
        {
            GameManager gm = FindObjectOfType<GameManager>();

            // hanya bisa klik tile kalau karakter sedang dipilih
            if (gm != null)
            {
                gm.MovePlayer(new Vector3(x, y, 0));
            }
        }
    }
}


    // efek hover
    void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    void OnMouseExit()
    {
        sr.color = baseColor;
    }

    public void ResetColor()
    {
        sr.color = baseColor;
    }
}
