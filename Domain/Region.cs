using System;

namespace Domain
{

    public enum RegionName
    {
        Región_metropolitana,
        Región_Centro_Sur,
        Región_Este,   
        Región_Litoral_Norte,
        Región_Corredor_Pájaros_Pintados
    }

    public class Region
    {
        public int Id { get; set; }

        public RegionName Name { get; set; }
        public override bool Equals(object obj)
        {
            return obj is Region region &&
                   Name == region.Name;
        }
    }
}