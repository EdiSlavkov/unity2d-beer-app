using UnityEngine;

public class Toggle : MonoBehaviour
{
    [SerializeField] private GameObject toggle;
    [SerializeField] private Animator toggleAnimator;
    private bool isOn;

    public void ChangeToggleStatus()
    {
        isOn = !isOn;
        toggleAnimator.SetBool("isOn", isOn);
    }
}