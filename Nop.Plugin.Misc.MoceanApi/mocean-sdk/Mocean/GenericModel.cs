using System.Xml.Serialization;

namespace Mocean
{
    [XmlRoot("result")]
    public class GenericModel
    {
        [XmlElement("status")]
        public string Status { get; set; }
    }
}
