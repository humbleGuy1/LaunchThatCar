using TMPro;
using UnityEngine;

public class SpeedView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CarMover _carMover;

    private void Update()
    {
        _text.text = $"Speed: {(int)_carMover.RBVelocity}";
    }
}
