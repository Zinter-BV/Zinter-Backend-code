using LogisticsSolution.Domain.Entities;

namespace LogisticsSolution.Infrastructure
{
    public class InsertedData
    {

        public static List<Province> Provinces = new List<Province>
        {
            new Province{ Id = 1, Name = "Drenthe"},
            new Province{ Id = 2, Name = "Flevoland"},
            new Province{ Id = 3, Name = "Friesland (Fryslân)"},
            new Province{ Id = 4, Name = "Gelderland"},
            new Province{ Id = 5, Name = "Groningen"},
            new Province{ Id = 6, Name = "Limburg"},
            new Province{ Id = 7, Name = "Noord-Brabant"},
            new Province{ Id = 8, Name = "Noord-Holland"},
            new Province{ Id = 9, Name = "Overijssel"},
            new Province{ Id = 10, Name = "Utrecht"},
            new Province{ Id = 11, Name = "Zeeland"},
            new Province{ Id = 12, Name = "Zuid-Holland"},
            new Province{ Id = 13, Name = "Assen"},
            new Province{ Id = 14, Name = "Lelystad"},
            new Province{ Id = 15, Name = "Arnhem"},
            new Province{ Id = 16, Name = "Groningen"},
            new Province{ Id = 17, Name = "Maastricht"},
            new Province{ Id = 18, Name = "'s-Hertogenbosch (Den Bosch)"},
            new Province{ Id = 19, Name = "Haarlem"},
            new Province{ Id = 20, Name = "Zwolle"},
            new Province{ Id = 21, Name = "Middelburg"},
            new Province{ Id = 22, Name = "The Hague (Den Haag)"},
            new Province{ Id = 23, Name = "Leeuwarden"},
        };

        public static List<OfferedService> OfferedServices = new List<OfferedService>
        {
            new OfferedService{Id= 1, Service = "Residential Moving" },
            new OfferedService{ Id = 2 , Service = "Commercial/Office Moving"},
            new OfferedService{ Id = 3 , Service = "Packing and UnPacking"},
            new OfferedService{ Id = 4, Service = "Storage Solutions"},
            new OfferedService{ Id = 5, Service = "Specialty Item Handling"},


        };
    }
}
