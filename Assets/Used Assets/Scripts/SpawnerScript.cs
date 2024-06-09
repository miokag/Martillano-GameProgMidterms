using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject eggPrefab, chickPrefab, henPrefab, roosterPrefab;
    public int eggCounter, chickCounter, henCounter, roosterCounter;

    private UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene.");
        }
        StartCoroutine(FirstLifecycle());
    }

    IEnumerator FirstLifecycle()
    {
        GameObject egg = Instantiate(eggPrefab, transform.position, Quaternion.identity);
        eggCounter++;
        uiManager.UpdateEggText(eggCounter);
        yield return new WaitForSeconds(10);

        Transform eggTransform = egg.transform;
        Destroy(egg);
        eggCounter--;
        uiManager.UpdateEggText(eggCounter);

        GameObject chick = Instantiate(chickPrefab, eggTransform.position, Quaternion.identity);
        chickCounter++;
        uiManager.UpdateChickText(chickCounter);
        yield return new WaitForSeconds(10);

        Transform chickTransform = chick.transform;
        Destroy(chick);
        chickCounter--;
        uiManager.UpdateChickText(chickCounter);

        GameObject hen = Instantiate(henPrefab, chickTransform.position, Quaternion.identity);
        henCounter++;
        uiManager.UpdateHenText(henCounter);
        StartCoroutine(HenLifecycle(hen));
    }

    IEnumerator EggLifecycle(Vector3 position)
    {
        GameObject egg = Instantiate(eggPrefab, position, Quaternion.identity);
        eggCounter++;
        uiManager.UpdateEggText(eggCounter);
        yield return new WaitForSeconds(10);

        Transform eggTransform = egg.transform;
        Destroy(egg);
        eggCounter--;
        uiManager.UpdateEggText(eggCounter);

        GameObject chick = Instantiate(chickPrefab, eggTransform.position, Quaternion.identity);
        chickCounter++;
        uiManager.UpdateChickText(chickCounter);
        yield return new WaitForSeconds(10);

        Transform chickTransform = chick.transform;
        Destroy(chick);
        chickCounter--;
        uiManager.UpdateChickText(chickCounter);

        GameObject maturedBird = Random.Range(0, 2) == 0 ? henPrefab : roosterPrefab;
        GameObject bird = Instantiate(maturedBird, chickTransform.position, Quaternion.identity);

        if (maturedBird == henPrefab)
        {
            henCounter++;
            uiManager.UpdateHenText(henCounter);
            StartCoroutine(HenLifecycle(bird));
        }
        else
        {
            roosterCounter++;
            uiManager.UpdateRoosterText(roosterCounter);
            StartCoroutine(RoosterLifecycle(bird));
        }
    }

    IEnumerator HenLifecycle(GameObject hen)
    {
        yield return new WaitForSeconds(30);

        int newEggs = Random.Range(2, 11);
        for (int i = 0; i < newEggs; i++)
        {
            StartCoroutine(EggLifecycle(hen.transform.position + new Vector3(i * 0.5f, 0, 0)));
        }

        yield return new WaitForSeconds(10);  // Time for eggs to hatch

        yield return new WaitForSeconds(40 - 30 - 10);  // Remaining lifespan of the hen

        henCounter--;
        uiManager.UpdateHenText(henCounter);
        Destroy(hen);
    }

    IEnumerator RoosterLifecycle(GameObject rooster)
    {
        yield return new WaitForSeconds(40);
        roosterCounter--;
        uiManager.UpdateRoosterText(roosterCounter);
        Destroy(rooster);
    }
}
