using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;

    private SpriteRenderer sr;

    // Warna tile (pakai transparansi supaya tidak nutup background)
    private Color baseColor = new Color(1f, 1f, 1f, 0f); // transparan default
    private Color hoverColor = new Color(0.6f, 0.9f, 1f, 0.6f); // biru muda transparan saat hover
    private Color movableColor = new Color(0.6f, 1f, 0.6f, 0.6f); // hijau muda transparan saat bisa digerak
    private Color invalidColor = new Color(1f, 0.3f, 0.3f, 0.7f); // merah untuk invalid klik

    private GameManager gm;
    public bool isMovable = false;
    private bool isFlashing = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

#if UNITY_EDITOR
        if (!Application.isPlaying)
            sr.color = new Color(1f, 1f, 1f, 0.25f); // kelihatan di editor
        else
            sr.color = baseColor; // saat Play, tile transparan
#else
        sr.color = baseColor; // build game
#endif

        // pastikan sorting layer benar
        sr.sortingLayerName = "Tiles";
        sr.sortingOrder = 1;

        gm = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject == gameObject)
            {
                if (gm != null)
                    gm.OnTileClicked(this);
                else
                    Debug.LogWarning("GameManager not found in scene!");
            }
        }
    }

    void OnMouseEnter()
    {
        if (isFlashing) return;
        sr.color = isMovable ? movableColor : hoverColor;
    }

    void OnMouseExit()
    {
        if (isFlashing) return;
        sr.color = isMovable ? movableColor : baseColor;
    }

    public void SetMovable(bool state)
    {
        isMovable = state;
        if (!isFlashing)
            sr.color = state ? movableColor : baseColor;
    }

    public void ResetColor()
    {
        sr.color = baseColor;
        isMovable = false;
    }

    private IEnumerator FlashInvalid()
    {
        isFlashing = true;
        sr.color = invalidColor;
        yield return new WaitForSeconds(0.2f);
        sr.color = isMovable ? movableColor : baseColor;
        isFlashing = false;
    }

    public void TriggerInvalidClickFeedback()
    {
        if (!isFlashing)
            StartCoroutine(FlashInvalid());
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // bantu kelihatan grid tile di editor
        Gizmos.color = new Color(1f, 1f, 1f, 0.1f);
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
#endif
}
