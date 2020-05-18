using UnityEngine;

public class DefiningIndecatorColor : MonoBehaviour
{
    public GameObject[] litmusIndecators;
    public GameObject[] phenolphthaleinIndecators;
    public GameObject[] methylorangeIndecators;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            litmusIndecators[i].SetActive(false);
            phenolphthaleinIndecators[i].SetActive(false);
            methylorangeIndecators[i].SetActive(false);
        }
    }

    public void LitmusButton()
    {
        for (int i = 0; i < litmusIndecators.Length; i++)
        {
            litmusIndecators[i].SetActive(true);
            phenolphthaleinIndecators[i].SetActive(false);
            methylorangeIndecators[i].SetActive(false);
        }
    }

    public void PhenolphthaleinButton()
    {
        for (int i = 0; i < phenolphthaleinIndecators.Length; i++)
        {
            litmusIndecators[i].SetActive(false);
            phenolphthaleinIndecators[i].SetActive(true);
            methylorangeIndecators[i].SetActive(false);
        }
    }

    public void MethylOrangeButton()
    {
        for (int i = 0; i < methylorangeIndecators.Length; i++)
        {
            litmusIndecators[i].SetActive(false);
            phenolphthaleinIndecators[i].SetActive(false);
            methylorangeIndecators[i].SetActive(true);
        }
    }
}
