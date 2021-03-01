using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoScene2 : MonoBehaviour {
    public List<EasyOutlineSystem> systems;
    public EasyOutlineSystem specialSystem;
    private int index = 0;
    public Text fpsText;
    private float deltatime = 0.0f;
    private float smoothing = 0.01f;

    void Start() {
        systems = new List<EasyOutlineSystem>();
        EasyOutlineSystem[] tempSystems = FindObjectsOfType<EasyOutlineSystem>();
        for (int i = 0; i < tempSystems.Length; i++) {
            if (i == 0) {
                specialSystem = tempSystems[i];
                tempSystems[i].enabled = false;
            }
            else {
                systems.Add(tempSystems[i]);
            }
        }
        StartCoroutine(Toggle());
    }

    void Update() {
        if (deltatime == 0.0f) { deltatime = Time.deltaTime; }
        deltatime = (Time.deltaTime * smoothing) + (deltatime * (1.0f - smoothing));
        fpsText.text = Mathf.FloorToInt(1.0f / deltatime).ToString();
    }

    IEnumerator Toggle() {
        yield return new WaitForSeconds(1f);
        foreach (var item in systems) {
            item.enabled = false;
        }
        while (true) {
            if (index == systems.Count) {
                index = 0;
                specialSystem.enabled = true;
                foreach (var item in systems) {
                    item.enabled = false;
                }
                yield return new WaitForSeconds(2f);
                specialSystem.enabled = false;
            }
            else {
                systems[index].enabled = true;
                index++;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
