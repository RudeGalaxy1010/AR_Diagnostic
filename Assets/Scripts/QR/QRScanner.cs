using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using ZXing;

// Class for decoding QRs
public class QRScanner : MonoBehaviour
{
    private const float ScanningSessionsDelay = 0.5f;

    public event UnityAction<string> QRDecoded;

    [SerializeField] private ARCameraBackground _cameraBackground;
    [SerializeField] private RenderTexture _renderTexture;

    private IBarcodeReader _codeReader = new BarcodeReader();
    private string _decodedText = "";

    private void Start()
    {
        StartCoroutine(Scan());
    }

    // Decoding camera view constantly
    private IEnumerator Scan()
    {
        while (true)
        {
            string text = TryDecode();
            
            if (string.IsNullOrEmpty(text) == false)
            {
                QRDecoded.Invoke(text);
            }

            yield return new WaitForSeconds(ScanningSessionsDelay);
        }
    }

    // Copying camera view to a Texture2D and return decoded string
    public string TryDecode()
    {
        Graphics.Blit(null, _renderTexture, _cameraBackground.material);
        Texture2D texture = _renderTexture.ToTexture2D();
        TryDecode(texture, out _decodedText);
        DestroyImmediate(texture);

        return _decodedText;
    }

    // Method for decoding texture into string, return true if string is not empty
    private bool TryDecode(Texture2D texture, out string result)
    {
        try
        {
            var decodeResult = _codeReader.Decode(texture.GetPixels32(), texture.width, texture.height);
            result = decodeResult.Text;
            return true;
        }
        catch (Exception exception)
        {
            
        }

        result = "";
        return false;
    }
}
