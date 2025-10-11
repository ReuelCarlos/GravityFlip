using UnityEngine;
using System.Collections;


public class Laser : MonoBehaviour
{
    //Laser
    [SerializeField] private GameObject _laser;
    [SerializeField] private bool _laserActive = true;
    [SerializeField] private Animator laserAni;
    void Start()
    {
        StartCoroutine(LaserActive());
    }

    
    //Laser
    IEnumerator LaserActive(){
        while(_laserActive == true){
            laserAni.SetBool("LaserOn", true);
            _laser.SetActive(true);
            yield return new WaitForSeconds (3.0f);

            laserAni.SetBool("LaserOn", false);
            _laser.SetActive(false);
            yield return new WaitForSeconds (3.0f);
        }
    }

}
