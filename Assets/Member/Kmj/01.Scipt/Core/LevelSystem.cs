using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelSystem : MonoBehaviour
{
    private RectTransform _rect;
    private GameObject[] itemList;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void Show()
    {
        RandomItem();
        _rect.localScale = Vector3.one;
        Time.timeScale = 0;
    }

    public void Hide()
    {
        _rect.localScale = Vector3.zero;
        Time.timeScale = 1;
        itemList.ToList().ForEach(UI => UI.SetActive(false));
    }

    public void RandomItem()
    {
        int maxCount = itemList.Length;

        int[] ran = new int[3];
        
        while (true)
        {
            ran[0] = Random.Range(0, maxCount);
            ran[1] = Random.Range(0, maxCount);
            ran[2] = Random.Range(0, maxCount);
            
            if(ran[0] != ran[1] && ran[1] != ran[2] && ran[2] != ran[0])
                break;
        }

        for (int i = 0; i < ran.Length; i++)
        {
            GameObject obj = itemList[ran[i]];
            
            obj.SetActive(true);
        }
    }
}
