using System;
using System.Diagnostics;
using System.IO;
using MonoDevelop.Core;
using MonoDevelop.Projects;

namespace VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings
{
    public class SolutionExtensionSettings : BaseExtensionSettings<SolutionExtensionSettings>
    {
        override protected string Filename => "SolutionTreeFilter.json";
        override protected string Folder { get; set; } = "";

        public void Init(Solution solution)
        {
            if (solution == null)
                return;

            var solutionPath = solution.BaseDirectory.ToAbsolute(new FilePath());
            Folder = Path.Combine(solutionPath, ".extensionsettings");

            FilePath = null;
            base.Init();
        }

        public override void Init()
        {
            if (string.IsNullOrEmpty(Folder))
            {
                throw new Exception("You must call Init(Solution solution)");
            }
        }

        public override T Get<T>(string key, T defaultValue)
        {
            if (this.FilePath is null)
            {
                Debug.WriteLine("Cannot call Get before Init(Solution solution)");
                return defaultValue;
        }
            return base.Get(key, defaultValue);
        }

        public override void Set<T>(string key, T value)
        {
            if (this.FilePath is null)
            {
                Debug.WriteLine("Cannot call Set before Init(Solution solution)");
                return;
            }
            base.Set(key, value);
        }
    }


    //public class PropertyService
    //{
    //    private const string FILE_NAME = "SolutionTreeFilter.json";

    //    private static PropertyService _instance;
    //    public static PropertyService Instance => _instance ?? (_instance = new PropertyService());

    //    private string _filePath;
    //    private readonly JsonSerializer _serializer;

    //    private Dictionary<string, string> _properties;

    //    public bool Initialized => _properties != null;

    //    PropertyService()
    //    {
    //        _serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented });
    //    }

    //    public void Init(Solution solution)
    //    {
    //        if (solution == null)
    //            return;

    //        EnsurePropertyFile(solution);
    //        ReadProperties();
    //    }

    //    public T Get<T>(string key, T defaultValue) where T : IConvertible
    //    {
    //        string value = Get(key, defaultValue.ToString());
    //        return (T)Convert.ChangeType(value, typeof(T));
    //    }

    //    public void Set<T>(string key, T value) where T : IConvertible
    //    {
    //        string strValue = (string)Convert.ChangeType(value, typeof(string));
    //        Set(key, strValue);
    //    }

    //    public string Get(string key, string defaultValue = "")
    //    {
    //        if (_properties == null)
    //            return defaultValue;

    //        return (_properties.ContainsKey(key) ? _properties[key] : defaultValue);
    //    }

    //    public void Set(string key, string value)
    //    {
    //        _properties[key] = value;
    //        WriteProperties();
    //    }

    //    private void ReadProperties()
    //    {
    //        using (StreamReader reader = File.OpenText(_filePath))
    //        {
    //            var data = (Dictionary<string, string>)_serializer.Deserialize(reader, typeof(Dictionary<string, string>));
    //            _properties = data ?? new Dictionary<string, string>();
    //        }
    //        LogProperties();
    //    }

    //    internal void WriteProperties()
    //    {
    //        using (StreamWriter writer = File.CreateText(_filePath))
    //        {
    //            _serializer.Serialize(writer, _properties);
    //        }
    //    }

    //    private void EnsurePropertyFile(Solution solution)
    //    {
    //        var solutionPath = solution.BaseDirectory.ToAbsolute(new FilePath());
    //        var settingsFolder = Path.Combine(solutionPath, ".extensionsettings");
    //        _filePath = Path.Combine(settingsFolder, FILE_NAME);

    //        if (!Directory.Exists(settingsFolder))
    //            Directory.CreateDirectory(settingsFolder);

    //        if (!File.Exists(_filePath))
    //            File.CreateText(_filePath).Close();
    //    }

    //    public List<string> GetAllKeys() => _properties.Keys.ToList();

    //    public void RemoveKey(string key)
    //    {
    //        if (_properties.ContainsKey(key))
    //            _properties.Remove(key);
    //    }

    //    internal void LogProperties()
    //    {
    //        foreach (var item in _properties)
    //        {
    //            Debug.WriteLine($"\t{item.Key,-50}={item.Value,-50}");
    //        }
    //    }
    //}
}