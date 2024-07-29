using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button forwardButton;
    [SerializeField]
    private Button homeButton;
    [SerializeField]
    private List<GameObject> listOfSlides = new List<GameObject>();

    private int currentSlide = 0;

    public enum Directions
    {
        Left,
        Right
    }

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(() => ChangeSlide(Directions.Left));
        forwardButton.onClick.AddListener(() => ChangeSlide(Directions.Right));
    }

    private void ChangeSlide(Directions direction)
    {
        listOfSlides[currentSlide].SetActive(false);

        if(direction == Directions.Right)
        {
            currentSlide = Mathf.Min(currentSlide + 1, listOfSlides.Count - 1);
        }
        else if(direction == Directions.Left)
        {
            currentSlide = Mathf.Max(currentSlide - 1, 0);
        }

        listOfSlides[currentSlide].SetActive(true);
    }
}
