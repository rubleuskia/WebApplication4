using System.Xml.Serialization;

namespace Core.Currencies.Apis.Rub.Entities
{
    [XmlRoot("Valuta")]
    public class RubCurrenciesResponse
    {
        [XmlElement("Item")]
        public RubCurrency[] Items { get; set; }
    }
}
