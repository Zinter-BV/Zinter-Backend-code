using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LogisticsSolution.Application.Dtos.Response
{
    public class KvkCompanyProfile
    {
        [JsonPropertyName("kvkNummer")]
        public string KvkNumber { get; set; }

        [JsonPropertyName("indNonMailing")]
        public string NonMailingIndicator { get; set; }

        [JsonPropertyName("naam")]
        public string Name { get; set; }

        [JsonPropertyName("formeleRegistratiedatum")]
        public string FormalRegistrationDate { get; set; }

        [JsonPropertyName("materieleRegistratie")]
        public MaterialRegistration MaterialRegistration { get; set; }

        [JsonPropertyName("totaalWerkzamePersonen")]
        public int TotalEmployees { get; set; }

        [JsonPropertyName("handelsnamen")]
        public List<TradeName> TradeNames { get; set; }

        [JsonPropertyName("sbiActiviteiten")]
        public List<SbiActivity> SbiActivities { get; set; }

        [JsonPropertyName("links")]
        public List<ApiLink> Links { get; set; }

        [JsonPropertyName("_embedded")]
        public EmbeddedData Embedded { get; set; }
    }

    public class MaterialRegistration
    {
        [JsonPropertyName("datumAanvang")]
        public string StartDate { get; set; }
    }

    public class TradeName
    {
        [JsonPropertyName("naam")]
        public string Name { get; set; }

        [JsonPropertyName("volgorde")]
        public int Order { get; set; }
    }

    public class SbiActivity
    {
        [JsonPropertyName("sbiCode")]
        public string SbiCode { get; set; }

        [JsonPropertyName("sbiOmschrijving")]
        public string Description { get; set; }

        [JsonPropertyName("indHoofdactiviteit")]
        public string IsMainActivity { get; set; }
    }

    public class ApiLink
    {
        [JsonPropertyName("rel")]
        public string Relation { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }
    }

    public class EmbeddedData
    {
        [JsonPropertyName("hoofdvestiging")]
        public MainBranch MainBranch { get; set; }

        [JsonPropertyName("eigenaar")]
        public Owner Owner { get; set; }
    }

    public class MainBranch
    {
        [JsonPropertyName("vestigingsnummer")]
        public string BranchNumber { get; set; }

        [JsonPropertyName("kvkNummer")]
        public string KvkNumber { get; set; }

        [JsonPropertyName("formeleRegistratiedatum")]
        public string FormalRegistrationDate { get; set; }

        [JsonPropertyName("materieleRegistratie")]
        public MaterialRegistration MaterialRegistration { get; set; }

        [JsonPropertyName("eersteHandelsnaam")]
        public string PrimaryTradeName { get; set; }

        [JsonPropertyName("indHoofdvestiging")]
        public string IsMainBranch { get; set; }

        [JsonPropertyName("indCommercieleVestiging")]
        public string IsCommercialBranch { get; set; }

        [JsonPropertyName("totaalWerkzamePersonen")]
        public int TotalEmployees { get; set; }

        [JsonPropertyName("adressen")]
        public List<Address> Addresses { get; set; }

        [JsonPropertyName("links")]
        public List<ApiLink> Links { get; set; }
    }

    public class Address
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("indAfgeschermd")]
        public string IsRestricted { get; set; }

        [JsonPropertyName("volledigAdres")]
        public string FullAddress { get; set; }

        [JsonPropertyName("straatnaam")]
        public string Street { get; set; }

        [JsonPropertyName("huisnummer")]
        public int HouseNumber { get; set; }

        [JsonPropertyName("huisletter")]
        public string HouseLetter { get; set; }

        [JsonPropertyName("postcode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("plaats")]
        public string City { get; set; }

        [JsonPropertyName("land")]
        public string Country { get; set; }
    }

    public class Owner
    {
        [JsonPropertyName("rechtsvorm")]
        public string LegalForm { get; set; }

        [JsonPropertyName("uitgebreideRechtsvorm")]
        public string ExtendedLegalForm { get; set; }

        [JsonPropertyName("links")]
        public List<ApiLink> Links { get; set; }
    }


}
