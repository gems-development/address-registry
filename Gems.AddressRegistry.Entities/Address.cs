using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gems.AddressRegistry.Entities
{
    public class Address : BaseGeoEntity
    {
        public virtual ICollection<AddressDataSource> DataSources { get; set; } = new List<AddressDataSource>(0);
        public virtual Country Country { get; set; } = null!;
        public virtual Region Region { get; set; } = null!;
        public virtual MunicipalArea? MunicipalArea { get; set; }
        public virtual AdministrativeArea? AdministrativeArea { get; set; }
        public virtual Territory? Territory { get; set; }
        public virtual City? City { get; set; }
        public virtual Settlement? Settlement { get; set; }
        public virtual PlaningStructureElement? PlaningStructureElement { get; set; }
        public virtual RoadNetworkElement? RoadNetworkElement { get; set; }
        public virtual Building? Building { get; set; }

        public virtual String GetNormalizedAddress()
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append($"{Region.Name}#");
            if (Territory != null)
                builder.Append($"{Territory.Name}#");
            if (City != null)
                builder.Append($"{City.Name}#");
            if (Settlement != null)
                builder.Append($"{Settlement.Name}#");
            if (RoadNetworkElement != null)
                builder.Append($"{RoadNetworkElement.Name}#");
            if (Building != null)
                builder.Append($"{Building.Number}");
            return builder.ToString();
        }

    }
}