using BikeDistributor.Domain.Models;
using BikeDistributor.Infrastructure.interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace BikeDistributor.Domain.Entities
{
    /// <summary>
    /// This class seems necessary to correctly bind a blazor EditForm
    /// </summary>
    public class GenericBike : IBike
    {
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        public int BasePrice { get; set; } = 0;
        public string Description { get; set; }
        public bool isStandard { get; set; }

        public int Price { get; set; }

        public List<BikeOption> SelectedOptions { get; set; } = new List<BikeOption>();

        public GenericBike()
        {
            
        }
    }

    /// <summary>
    /// This will set some properties to be ignored during serialization. See jsonutils for usage
    /// https://stackoverflow.com/questions/10169648/how-to-exclude-property-from-json-serialization
    /// JsonConvert.SerializeObject(YourObject, new JsonSerializerSettings(){ ContractResolver = new IgnorePropertiesResolver(new[] { "Prop1", "Prop2" }) });
    /// </summary>
    public class IgnorePropertiesResolver : DefaultContractResolver
    {
        private readonly HashSet<string> ignoreProps;
        public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
        {
            this.ignoreProps = new HashSet<string>(propNamesToIgnore);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            if (this.ignoreProps.Contains(property.PropertyName))
            {
                property.ShouldSerialize = _ => false;
            }
            return property;
        }
    }
}
