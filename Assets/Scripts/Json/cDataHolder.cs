using Newtonsoft.Json;
using System;

[Serializable]
public class cDataHolder
{
    public string dGPUName;
    public string dGPUStatus;
    public string dGPUDriverVersion;
    public string dGPUVideoProcessor;
    public string dCPUName;
    public string dCPUManufacturer;
    public string dSystemName;
    public string dSystemSerialNumber;
    public string dSystemDirectory;
    public string dSystemVersion;
    public double dGPURAM;
    public int dCPUNumberOfCores;
    public int dCPUNumberOfThreads;
    public int dCPUWidth;
    public long dRAMSize;
    public long dRAMFree;
    public long dCPUCurrentClockSpeed;
    public string dDiskParams;

    public static cDataHolder CreateFromString(string jsonString)
    {
        cDataHolder data = JsonConvert.DeserializeObject<cDataHolder>(jsonString);
        return data;
    }
}
