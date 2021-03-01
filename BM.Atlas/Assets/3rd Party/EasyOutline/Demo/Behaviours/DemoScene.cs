using UnityEngine;

public class DemoScene : MonoBehaviour {
    public EasyOutlineSystem easyOutlineSystem;
    public GameObject blocker;
    public GameObject ui;
    public Animator animator;
	public float cameraHeight = 2.5f;
	public float cameraDistance = 2.8f;
    public bool moveBlocker = false;
    void Update() {
        if (moveBlocker) {
            Vector3 pos = blocker.transform.position;
            float min = 0.73f;
            float max = 2.47f;
            pos.y = min + (Mathf.Sin(Time.time * 1.25f) + 1f) * 0.5f * (max - min);
            blocker.transform.position = pos;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            ui.SetActive(!ui.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            animator.enabled = !animator.enabled;
        }
    }
    public void UpdateOutlineThickness(float value) {
        easyOutlineSystem.outlineThickness = value;
    }
    public void UpdateOutlineColorR(float value) {
        easyOutlineSystem.outlineColor.r = value;
    }
    public void UpdateOutlineColorG(float value) {
        easyOutlineSystem.outlineColor.g = value;
    }
    public void UpdateOutlineColorB(float value) {
        easyOutlineSystem.outlineColor.b = value;
    }
    public void UpdateOutlineColorA(float value) {
        easyOutlineSystem.outlineColor.a = value;
    }
    public void UpdateFillColorR(float value) {
        easyOutlineSystem.fillColor.r = value;
    }
    public void UpdateFillColorG(float value) {
        easyOutlineSystem.fillColor.g = value;
    }
    public void UpdateFillColorB(float value) {
        easyOutlineSystem.fillColor.b = value;
    }
    public void UpdateFillColorA(float value) {
        easyOutlineSystem.fillColor.a = value;
    }
    public void UpdateOutlineMode(int value) {
        easyOutlineSystem.outlineMode = (Mode)value;
    }
    public void UpdateFillMode(int value) {
        easyOutlineSystem.fillMode = (Mode)value;
    }
}