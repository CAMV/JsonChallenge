using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [SerializeField]
    private Text _title = null;
    [SerializeField]
    private Transform _dataContainer = null;
    [SerializeField]
    private GameObject _columnPrefab = null;

    private GUIContent _content = null;

    // Start is called before the first frame update
    void Start()
    {
        _content = new GUIContent();
        UpdateContent();
    }

    public void UpdateContent()
    {
        if (_dataContainer.childCount > 0)
        {
            Debug.Log("Reload");
            for (int i = 0; i < _dataContainer.childCount; i++)
                Destroy(_dataContainer.GetChild(i).gameObject);
        }

        _content.LoadFromJSON();
        _title.text = _content.Title;

        string[] headers = _content.ColumnHeaders;

        for (int i = 0; i < headers.Length; i++)
        {
            GameObject headerGO = Instantiate(_columnPrefab);
            headerGO.name = headers[i];
            headerGO.transform.SetParent(_dataContainer);
            headerGO.GetComponent<Text>().text = headers[i];

            for (int j = 0; j < _content.Data.GetLength(1)-1; j++)
            {
                GameObject entryGO = Instantiate(headerGO.transform.GetChild(0).gameObject);
                entryGO.name = "Entry " + (j+2); 
                entryGO.transform.SetParent(headerGO.transform);
            }

            for (int j = 0; j < _content.Data.GetLength(1); j++)
            {
                headerGO.transform.GetChild(j).GetComponent<Text>().text = _content.Data[i, j];
            }
        }

    }
    
}
