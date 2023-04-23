using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using YamlDotNet.Serialization;

namespace InterC
{
    public class ProblemData
    {
        public static string ROOT_URL = "https://raw.githubusercontent.com/P-ro-VL/InterC/main/";
        public static string DATA_FILE_URL = ROOT_URL + "config/config.yml";

        public static Dictionary<string, Problem> problems = new Dictionary<string, Problem>();

        public static List<Problem> GetProblems()
        {
            return problems.Values.ToList<Problem>();
        }

        public static void load()
        {
            using (var client = new WebClient())
            {
                client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

                string content = client.DownloadString(DATA_FILE_URL);

                var deserializer = new DeserializerBuilder().Build();
                List<string> entries = deserializer.Deserialize<List<string>>(content);
                foreach (var entry in entries)
                {
                    string problemUrl = client.DownloadString(ROOT_URL + "problems/" + entry + "/data.yml");
                    problems.Add(entry, deserializer.Deserialize<Problem>(problemUrl));
                }
            }
        }

    }

    public class Problem
    {
        public string ID { get; set; }
        public List<String> Description { get; set; }
        public List<String> Input { get; set; }
        public List<String> Output { get; set; }
        public string Name { get; set; }
        public int Difficulty { get; set; }
        public Dictionary<string, Test> Tests { get; set; }
        public Test ExampleTest { get; set; }
        public double TimeLimit { get; set; }
        public string Subject { get; set; }
        public List<String> Note { get; set; }
    }

    public class Test
    {
        public List<string> Input { get; set; }
        public List<string> Output { get; set; }

        public string toStringInput()
        {
            string s = "";
            for (int i = 0; i < Input.Count; i++)
            {
                string input = Input[i];
                if (i != Input.Count - 1)
                    s += input + "\n";
                else
                    s += input;
            }
            return s;
        }

        public string toStringOutput()
        {
            string s = "";
            for (int i = 0; i < Output.Count; i++)
            {
                string output = Output[i];
                if (i != Output.Count - 1)
                    s += output + "\n";
                else
                    s += output;
            }
            return s;
        }
    }
}
