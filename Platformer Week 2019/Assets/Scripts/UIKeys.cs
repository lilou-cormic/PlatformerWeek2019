using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIKeys : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI KeysText = null;

    private void Update()
    {
        KeysText.text = Key.Keys.ToString();
    }
}
