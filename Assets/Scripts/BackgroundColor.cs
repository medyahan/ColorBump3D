using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    [SerializeField] private List<Color> _colors = new List<Color>();

    private void Start()
    {
        int random = Random.Range(0, _colors.Count);
        GetComponent<SpriteRenderer>().color = _colors[random];
    }
    
}
