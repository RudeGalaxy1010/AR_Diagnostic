using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QRTracker : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private QRScanner _QRScanner;

    private void Start()
    {
        StartCoroutine(Scan());
    }

    private IEnumerator Scan()
    {
        while (true)
        {
            _text.text = _QRScanner.DecodeScreenshot();
            yield return new WaitForSeconds(1);
        }
    }
}
