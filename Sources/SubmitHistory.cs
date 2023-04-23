using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace InterC
{
    public class SubmitHistory
    {
        public string StartDate { get; set; }
        public string ProblemName { get; set; }
        public string Status { get; set; }
        public string Detail { get; set; }
    }

    public class SubmitHistoryManager
    {
        public static List<SubmitHistory> histories = new List<SubmitHistory>();

        public static void loadHistory()
        {
            if (!File.Exists("history.yml"))
                File.Create("history.yml");
            var deserializer = new DeserializerBuilder().Build();
            histories = deserializer.Deserialize<List<SubmitHistory>>(File.ReadAllText("history.yml"));
        }

        public static void saveHistory()
        {
            var serializer = new SerializerBuilder().Build();
            File.WriteAllText("history.yml", serializer.Serialize(histories));
        }
    }
}
