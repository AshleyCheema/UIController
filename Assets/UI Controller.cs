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
    private Button returnButton;

    [SerializeField]
    private Button enterButton;

    [SerializeField] 
    private GameObject slideHolder;

    [SerializeField]
    private List<GameObject> listOfSlides = new List<GameObject>();

    private int currentSlide = 0;

    public bool returnToLastPage;

    public enum Directions
    {
        Left,
        Right
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialise();
    }

    protected void Initialise()
    {
        backButton.onClick.AddListener(() => ChangeSlide(Directions.Left));
        forwardButton.onClick.AddListener(() => ChangeSlide(Directions.Right));
        returnButton.onClick.AddListener(ReturnButton);
        enterButton.onClick.AddListener(EnterButton);
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

    private void EnterButton()
    {
        listOfSlides[currentSlide].SetActive(true);
        slideHolder.gameObject.SetActive(true);
    }

    private void ReturnButton()
    {
        slideHolder.gameObject.SetActive(false);
        listOfSlides[currentSlide].SetActive(false);

        if (!returnToLastPage)
        {
            currentSlide = 0;
        }

    }
}
