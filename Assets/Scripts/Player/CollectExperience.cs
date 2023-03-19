using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
using DG.Tweening;

public class CollectExperience : MonoBehaviour
{
    public Slider experienceSlider; // assign the slider in the inspector
    public float moveSpeed = 3f; // adjust the speed at which the object moves towards the center
    public GameObject levelUpMenu;
    public GameObject experienceObject;
    [HideInInspector] public static CollectExperience Instance;
    private void Awake()
    {
        Instance = this;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Experience"))
    {
        // check if the experience object has already been collected
        if (col.gameObject.GetComponent<Experience>().isCollected == false)
        {
            // get the experience value of the collected object
            float experienceValue = col.GetComponent<Experience>().value;

            // increase the slider's value by the collected object's experience value
            experienceSlider.value += experienceValue;
            ControlLevelUp();

            // create a DOTween sequence to move the collected object towards the center of this object and then destroy it
            Sequence sequence = DOTween.Sequence();
            sequence.Append(col.transform.DOMove(transform.position, moveSpeed))
                    .AppendCallback(() => Destroy(col.gameObject));

            // mark the experience object as collected
            col.gameObject.GetComponent<Experience>().isCollected = true;
        }
    }
    }

    void ControlLevelUp()
    {
        if (experienceSlider.value >= experienceSlider.maxValue)
        {
            levelUpMenu.SetActive(true);
            Time.timeScale = 0f;
            experienceSlider.value = 0f;
            experienceSlider.maxValue += 20f;
        }
    }
}
