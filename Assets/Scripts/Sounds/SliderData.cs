public struct SliderData
{
    public SliderData(string sourceName, float value)
    {
        SourceName = sourceName;
        Value = value;
    }

    public string SourceName { get; }
    public float Value { get; }
}