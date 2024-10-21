using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Show : MonoBehaviour
{
    public List<GameObject> Objects;

    public float ShowTime = 3f;
    
    public float StoppingTime = 2.5f;

    private int _index;

    private void Start()
    {
        CloseAll();
        StartCoroutine(ShowInOrder());
    }
    
    private void CloseAll()
    {
        foreach (GameObject obj in Objects)
        {
            obj.SetActive(false);
        }
    }
    
    private IEnumerator ShowInOrder()
    {
        foreach (GameObject obj in Objects)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(ShowTime);
            obj.GetComponent<IShowInOrder>().Stop(StoppingTime);
            yield return new WaitForSeconds(StoppingTime);
        }
    }
}
