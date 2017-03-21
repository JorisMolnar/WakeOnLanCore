namespace WakeOnLanCore.Models
{
    public class Device
    {
        public int ID { get; }
        public string Name { get; }
        public string MacAddress { get; }

        public Device(int id, string name, string macAddress)
        {
            ID = id;
            Name = name;
            MacAddress = macAddress;
        }

        // Constructor for serialization
        public Device() { }
    }
}
