using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace MV.Framework
{

    public class TestConfig
    {
        protected string _configFile;
        protected string _rawJson;
        protected string _rootElement = "tests";
        private string _currentTest;

        public TestConfig(string configFile)
        {
            _configFile = configFile;
            _Load();
        }

        private void _Load()
        {
            _rawJson = File.ReadAllText(_configFile);
        }
        /// <summary>
        /// Candidate for removal
        /// not used but may come useful in later stages
        /// </summary>
        /// <returns></returns>
        public string GetRawJson()
        {
            return _rawJson;
        }

        /// <summary>
        /// Retrieves a specific config value
        /// Es. bike.Name
        /// </summary>
        /// <param name="itemsCollectionName"></param>
        /// <param name="itemIndex"></param>
        /// <param name="key"></param>
        /// <param name="testName"></param>
        /// <returns></returns>
        public string Read(string itemsCollectionName, int itemIndex, string key, string testName = "")
        {
            JObject o = JObject.Parse(_rawJson);
            if (testName == "")
            {
                testName = this._currentTest;
            }
            return (string)o[_rootElement][0][itemsCollectionName][itemIndex][key];
        }
        /// <summary>
        /// Retrieves the entire JObject config for an item
        /// Es. a Bike or an Order
        /// </summary>
        /// <param name="itemsCollectionName"></param>
        /// <param name="itemIndex"></param>
        /// <param name="testName"></param>
        /// <returns></returns>
        public JObject GetJObject(string itemsCollectionName, int itemIndex, string testName = "")
        {
            JObject o = JObject.Parse(_rawJson);
            if (testName == "")
            {
                testName = this._currentTest;
            }

            return (JObject)o[_rootElement][0][itemsCollectionName][itemIndex];
        }
        public JObject GetJObject(string itemsCollectionName)
        {
            JToken o = JObject.Parse(_rawJson);

            return (JObject)o[itemsCollectionName][0];
        }

        public JArray GetJArray(string itemsCollectionName)
        {
            var o = JObject.Parse(_rawJson);
            var items = (JArray)o[_rootElement][0][itemsCollectionName];

            return items;
        }
        public static T Deserialize<T>(string json)
        {
            Newtonsoft.Json.JsonSerializer s = new JsonSerializer();
            return s.Deserialize<T>(new JsonTextReader(new StringReader(json)));
        }

        public T GetClassObject<T>(string itemsCollectionName, int itemIndex)
        {
            var json = GetJObject(itemsCollectionName, itemIndex);
            return Deserialize<T>(json.ToString());
        }
        public T GetClassObject<T>(string itemsCollectionName)
        {
            var json = GetJObject(itemsCollectionName);
            return Deserialize<T>(json.ToString());
        }

        /// <summary>
        /// not really used atm
        /// </summary>
        /// <param name="name"></param>
        public void SetCurrentTest(string name)
        {
            _currentTest = name;
        }
        /// <summary>
        /// a dummy method not worth of much consideration
        /// </summary>
        /// <returns></returns>
        public string SayHello()
        {
            return "Hello from " + _currentTest;
        }
    }
}

