using System;
using UnityEngine;
using ZXing;

public class QRCodeScanner
{
    private IBarcodeReader _codeReader;

    public QRCodeScanner()
    {
        _codeReader = new BarcodeReader();
    }

    public string TryScan(WebCamTexture texture)
    {
        try
        {
            Result result = null;
            result = _codeReader.Decode(texture.GetPixels32(), texture.width, texture.height);

            if (result != null)
            {
                return result.Text;
            }
        }
        catch (Exception exception)
        {
            Debug.LogError(exception.Message);
        }

        return "";
    }
}
