using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeWaveControl : MonoBehaviour
{
    [SerializeField] List<GameObject> spike = new List<GameObject>();
    [SerializeField] float min_speedUp;
    [SerializeField] float max_speedUp;
    [SerializeField] float min_speedDown;
    [SerializeField] float max_speedDown;
    void Start()
    {
        SpikeUp();
    }
    public void SpikeUp()
    {
        int countSpike = transform.childCount;
        for (int i = 0; i < countSpike; i++)
        {
            spike.Add(transform.GetChild(i).gameObject);
            spike[i].GetComponent<Animator>().SetFloat("SpeedUp", Random.Range(min_speedUp, max_speedUp));
            spike[i].GetComponent<Animator>().SetFloat("SpeedDown", Random.Range(min_speedDown, max_speedDown));
        }
    }
    public void RetrunSpike(GameObject _spike)
    {
        spike.Remove(_spike);
        if (spike.Count == 0)
        {
            Destroy(this.gameObject);
        }
    }

}
