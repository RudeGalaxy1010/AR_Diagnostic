using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using ZXing;

public class QRScanner : MonoBehaviour
{
    [SerializeField] private ARCameraBackground _cameraBackground;
    [SerializeField] private RenderTexture _renderTexture;

    private IBarcodeReader _codeReader = new BarcodeReader();
    private string _decodedText = "";

    public string DecodeScreenshot()
    {
        Graphics.Blit(null, _renderTexture, _cameraBackground.material);
        Texture2D texture = _renderTexture.ToTexture2D();
        TryDecode(texture, out _decodedText);
        DestroyImmediate(texture);

        return _decodedText;
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
            
        }

        result = "";
        return false;
    }
}
