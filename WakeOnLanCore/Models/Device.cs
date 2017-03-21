using System;

namespace WakeOnLanCore.Models
{
    [Serializable]
    public class Device
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string MacAddress { get; set; }
    }
}
