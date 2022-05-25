using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings
{
    public class BaseExtensionSettings<TDerivedClass> : BaseExtensionSettings
       where TDerivedClass : BaseExtensionSettings, new()
    {
        private static TDerivedClass _instance;
        public static TDerivedClass Instance => _instance ?? (_instance = new TDerivedClass());
    }

    public class BaseExtensionSettings
    {
        protected virtual string Filename { get; } = "";
        protected virtual string Folder { get; set; } = "";
        protected string FilePath;

        private readonly JsonSerializer _serializer;

        private Dictionary<string, string> _properties;

        public bool Initialized => _properties != null && FilePath != null;

        public BaseExtensionSettings()
        {
            _serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }

        public virtual void Init()
        {
            if (FilePath is null)
            {
                EnsurePropertyFile();
                ReadProperties();
            }
        }

        private void EnsurePropertyFile()
        {
            FilePath = Path.Combine(Folder, Filename);

            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            if (!File.Exists(FilePath))
                File.CreateText(FilePath).Close();
        }

        virtual public T Get<T>(string key, T defaultValue) where T : IConvertible
        {
            Init();

            string value = Get(key, defaultValue.ToString());
            return (T)Convert.ChangeType(value, typeof(T));
        }

        virtual public void Set<T>(string key, T value) where T : IConvertible
        {
            Init();

            string strValue = (string)Convert.ChangeType(value, typeof(string));
            Set(key, strValue);
        }

        private string Get(string key, string defaultValue = "")
        {
            if (_properties == null)
                return defaultValue;

            return (_properties.ContainsKey(key) ? _properties[key] : defaultValue);
        }

        private void Set(string key, string value)
        {
            _properties[key] = value;
            WriteProperties();
        }

        private void ReadProperties()
        {
            if (FilePath is null)
                return;

            using (StreamReader reader = File.OpenText(FilePath))
            {
                var data = (Dictionary<string, string>)_serializer.Deserialize(reader, typeof(Dictionary<string, string>));
                _properties = data ?? new Dictionary<string, string>();
            }
            LogProperties();
        }

        internal void WriteProperties()
        {
            if (FilePath is null)
                return;

            using (StreamWriter writer = File.CreateText(FilePath))
            {
                _serializer.Serialize(writer, _properties);
            }
        }

        public List<string> GetAllKeys() => _properties?.Keys.ToList() ?? new List<string>();

        public void RemoveKey(string key)
        {
            if (_properties is null)
                return;

            if (_properties.ContainsKey(key))
                _properties.Remove(key);
        }

        internal void LogProperties()
        {
            if (_properties is null)
                return;

            foreach (var item in _properties)
            {
                Debug.WriteLine($"\t{item.Key,-50}={item.Value,-50}");
            }
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