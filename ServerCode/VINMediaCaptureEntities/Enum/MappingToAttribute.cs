using System;

namespace VINMediaCaptureEntities.Enum
{

    public class MappingToAttribute : Attribute
    {
        public MappingToAttribute(string mapping)
        {
            Mapping = mapping;
        }
        public string Mapping { get; set; }
    }
    public class ValueXMLAttribute : Attribute
    {
        public ValueXMLAttribute(string valueXML)
        {
            ValueXML = valueXML;
        }
        public string ValueXML { get; set; }
    }
}
