using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _textObj;
    [SerializeField] private float _spawnSeparation;
    [SerializeField] private int _textObjNum;

    private void Start()
    {
        GameObject prevText = null;
        for (int i = 0; i < _textObjNum; i++)
        {
            GameObject newTextObj;

            if (prevText == null)
            {
                newTextObj = Instantiate(_textObj, transform.position, Quaternion.identity);
            }
            else
            {
                newTextObj = Instantiate(_textObj, prevText.transform.position, Quaternion.identity);
            }
            prevText = newTextObj;
        }
    }
}
