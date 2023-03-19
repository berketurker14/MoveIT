using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

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

                // move the collected object towards the center of this object
                Vector3 targetPosition = transform.position;
                col.transform.position = Vector3.MoveTowards(col.transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // check if the collected object has reached the target position
                if (col.transform.position == targetPosition)
                {
                    // mark the experience object as collected
                    col.gameObject.GetComponent<Experience>().isCollected = true;

                    // destroy the collected object
                    Destroy(col.gameObject);
                }
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
