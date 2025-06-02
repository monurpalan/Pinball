using UnityEngine;

public class PointCenterController : MonoBehaviour
{
    [SerializeField] private int score;

    private ColorController colorController;

    private void Awake()
    {
        colorController = GetComponent<ColorController>();
    }

    public void OnHit()
    {
        GameManager.instance.score += score;

        if (colorController != null)
        {
            colorController.ChangeToRandomColor();
        }
    }
}