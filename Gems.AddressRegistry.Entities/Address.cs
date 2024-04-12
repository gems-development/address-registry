using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Gems.AddressRegistry.OsmDataParser.Interfaces;

namespace Gems.AddressRegistry.Entities
{
    public class Address : BaseGeoEntity, INormalizable
    {
        public virtual ICollection<AddressDataSource> DataSources { get; set; } = new List<AddressDataSource>(0);
        
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
            ClearNames();
            StringBuilder builder = new StringBuilder("");
            builder.Append($"{Region.Name}#");
            if (MunicipalArea != null)
                builder.Append($"{MunicipalArea.Name}#");
            if (City != null)
                builder.Append($"{City.Name}#");
            if (Settlement != null)
                builder.Append($"{Settlement.Name}#");
            if (RoadNetworkElement != null)
                builder.Append($"{RoadNetworkElement.Name}#");
            if (Building != null)
                builder.Append($"{Building.Number}");
            return builder.ToString().ToUpper();
        }

        public bool IsCorrect()
        {
            if ((City != null || Settlement != null) && RoadNetworkElement != null && MunicipalArea != null && Building != null )
            {
                return true;
            } else
            {
                return false; 
            }
        }

        public void ClearNames()
        {
            if(MunicipalArea != null)
            {
            MunicipalArea.Name = MunicipalArea.Name.ToUpper().Replace("ГОРОД ", "");
            }
        }
    }
}