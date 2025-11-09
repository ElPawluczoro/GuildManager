using UnityEngine;

public class Raycast2DTester : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero);

            Debug.DrawRay(origin, Vector2.zero, Color.red, 1f);
            
            if (hit.collider != null)
                Debug.Log("Hit: " + hit.collider.name);
            else
                Debug.Log("No hit");
        }
    }
}