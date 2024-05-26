using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCamera : MonoBehaviour
{
    //Icon in map
    public GameObject[] iconWorldMap;


    void Start()
    {
        AudioManager.Instance.PlayMusic("DayTheme");
    }
    //All Function
    #region Function

    #region Set camera delay
    //Set camera on
    public void CameraOn(GameObject camera)
    {
        foreach (GameObject obj in iconWorldMap)
        {
            obj.GetComponent<Image>().enabled = false;
        }

        /*(for (int i = 0; i < iconImageWorldMap.Length; i++)
        {
            iconImageWorldMap[i].enabled = false;
        }*/

        StartCoroutine(DelayCameraOn(camera));
    }

    public void CameraOff(GameObject camera)
    {
        StartCoroutine(DelayCameraOff(camera));
    }
    #endregion

    #region Set on/off gameobject with delay
    public void SetOnWithDelay(GameObject obj)
    {
        StartCoroutine(DelaySetOn(obj));
    }

    public void SetOffWithDelay(GameObject obj)
    {
        StartCoroutine(DelaySetOff(obj));
    }
    #endregion

    #region Set on/off gameobject without delay
    public void SetOnWithoutDelay(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void SetOffWihtoutDelay(GameObject obj)
    {
        obj.SetActive(false);
    }
    #endregion

    #endregion

    #region DelayCamera
    //Make camera delay when transition cam
    IEnumerator DelayCameraOn(GameObject camera)
    {
        
        yield return new WaitForSeconds(0.5f);

        camera.SetActive(true);

        //Make all member in array active with "foreach"
        foreach (GameObject obj in iconWorldMap)
        {
            obj.gameObject.SetActive(false);
        }

        //Make all member in array active with "for loop"
        /*for (int i = 0; i < iconWorldMap.Length; i++)
        {
           iconWorldMap[i].gameObject.SetActive(false);
        }*/

    }

    IEnumerator DelayCameraOff(GameObject camera)
    {
        yield return new WaitForSeconds(0.5f);
        camera.SetActive(false);

        //Make all member in array active with "foreach"
        foreach (GameObject obj in iconWorldMap)
        {
            obj.gameObject.SetActive(true);
        }

        foreach (GameObject obj in iconWorldMap)
        {
            obj.GetComponent<Image>().enabled = true;
        }

        //Make all member in array active with "for loop"
        /*for(int i = 0; i < iconWorldMap.Length; i++)
        {
            iconWorldMap[i].gameObject.SetActive(true);
        }*/
    }
    #endregion

    #region Delay set object
    //Delay set on
    IEnumerator DelaySetOn(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        obj.SetActive(true);
    }

    //Delay set Off
    IEnumerator DelaySetOff(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        obj.SetActive(false);
    }
    #endregion

    public void GoToNight()
    {
        GameManagerPor.Instance.timeCycle.isPause = false;
        GameManagerPor.Instance.timeCycle.isTimeSkip = false;
        GameManagerPor.Instance.timeCycle.mins += 1;
        GameManagerPor.Instance.timeCycle.round += 1;
    }

    #region Change State camera
    public void CameraLockOnShop()
    {
        GameManagerPor.Instance.stateCamera = StateInGame.ShopArea;
    }

    public void CameraLockOnFishing()
    {
        GameManagerPor.Instance.stateCamera = StateInGame.FishingArea;
    }

    public void CameraLockOnFarm()
    {
        GameManagerPor.Instance.stateCamera = StateInGame.FarmingArea;
    }

    public void CameraLockOnWorld()
    {
        GameManagerPor.Instance.stateCamera = StateInGame.WorldArea;
    }

    public void CameraLockOnCombat()
    {
        GameManagerPor.Instance.stateCamera = StateInGame.CombatArea;
    }

    public void CameraLockOnHouse()
    {
        GameManagerPor.Instance.stateCamera = StateInGame.HouseArea;
    }
    #endregion
}
