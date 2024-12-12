public struct ToggleData
{
    public ToggleData(string sourceName, bool isEnabled)
    {
        SourceName = sourceName;
        IsEnabled = isEnabled;
    }

    public string SourceName { get; }
    public bool IsEnabled { get; }
}