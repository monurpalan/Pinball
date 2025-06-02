using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D leftArm;
    [SerializeField] private Rigidbody2D rightArm;
    [SerializeField] private float power = 5f;

    private bool IsPressKeyLeft => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    private bool IsPressKeyRight => Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

    private void Update()
    {
        HandleKeyboardInput();
        HandleMouseInput();
    }

    private void HandleKeyboardInput()
    {
        if (IsPressKeyLeft)
        {
            Hit(leftArm);
        }

        if (IsPressKeyRight)
        {
            Hit(rightArm);
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            // Mouse'un ekrandaki yatay konumunu 0-1 aralığında alır
            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            // Ekranın sol yarısında tıklanırsa sol kolu, sağ yarısında tıklanırsa sağ kolu hareket ettirir
            if (mousePos.x < 0.5f)
            {
                Hit(leftArm);
            }
            if (mousePos.x > 0.5f)
            {
                Hit(rightArm);
            }
        }
    }

    private void Hit(Rigidbody2D current)
    {
        current.AddForce(Vector2.up * power);
    }
}