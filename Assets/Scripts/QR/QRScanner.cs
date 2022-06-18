using System;
using System.Collections;
using UnityEngine;
using ZXing;

public class QRScanner : MonoBehaviour
{
    [SerializeField] private ContentView _contentView;

    private IBarcodeReader _codeReader = new BarcodeReader();
    private string _decodedText = "";

    public string DecodeScreenshot()
    {
        StartCoroutine(TakeScreenshot((texture) => 
        {
            TryDecode(texture, out _decodedText);
        }));

        return _decodedText;
    }

    private IEnumerator TakeScreenshot(Action<Texture2D> callback)
    {
        _contentView.HideAll();
        yield return new WaitForEndOfFrame();
        callback(ScreenCapture.CaptureScreenshotAsTexture());
        yield return new WaitForEndOfFrame();
        _contentView.ShowAll();
    }

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
            Debug.LogWarning(exception);
        }

        result = "";
        return false;
    }
}
