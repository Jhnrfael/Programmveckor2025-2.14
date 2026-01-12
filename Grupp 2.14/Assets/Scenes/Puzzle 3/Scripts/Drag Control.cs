using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class DragControl : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject wordBox;
    [SerializeField] GameObject correctBlankSpace;

    private Vector3 ogWorldBoxPos;
    private bool isPlaced = false;
    private bool isCorrectlyPlaced = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ogWorldBoxPos = wordBox.transform.position;
    }

    void OnMouseDrag()
    {
        if (isPlaced) return;

        rb.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == correctBlankSpace)
        {
            isCorrectlyPlaced = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject != correctBlankSpace)
        {
            isCorrectlyPlaced = false;
        }
    }

    void OnMouseUp()
    {
        if (isPlaced) return;

        if (isCorrectlyPlaced)
        {
            rb.position = correctBlankSpace.transform.position;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.isKinematic = true;
            isPlaced = true;
        }
        else
        {
            rb.position = ogWorldBoxPos;
        }
    }
}
