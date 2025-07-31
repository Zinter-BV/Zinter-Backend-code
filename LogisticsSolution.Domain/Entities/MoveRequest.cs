using System;
using System.Collections.Generic;
using LogisticsSolution.Domain.Enums;

namespace LogisticsSolution.Domain.Entities
{
    public class MoveRequest
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public MoveStatusEnum MoveStatus { get; set; } = MoveStatusEnum.NewRequest;
        public string PickUpAddress { get; set; }
        public string PickUpAddressNumber { get; set; }
        public string DropOffAddressNumber { get; set; }
        public string DropOffAddress { get; set; }
        public DateTime MoveTime { get; set; }
        public DateTime PickUpTime { get; set; }
        public int FromNumberOfFloors { get; set; }
        public int ToNumberOfFloors { get; set; }
        public string? FromLongCarry { get; set; }
        public string? ToLongCarry { get; set; }
        public string? FromRemark { get; set; }
        public string? ToRemark { get; set; }
        public string PickUpLongitude { get; set; }
        public string PickUpLatitude { get; set; }
        public string DropOffLongitude { get; set; }
        public string DropOffLatitude { get; set; }
        public string MoveCode  { get; set; }
        public bool FromHasElevator { get; set; }
        public bool ToHasElevator { get; set; }
        public bool FromNeedShuttle { get; set; }
        public bool ToNeedShuttle { get; set; }
        public bool FromHasBuildingInsurance { get; set; }
        public bool ToHasBuildingInsurance { get; set; }
        public bool FromNeedHelpPacking { get; set; }
        public bool ToNeedHelpPacking { get; set; }
        public DateTime CreatedOn { get; set; }
        public MoveRequest()
        {
            this.CreatedOn = DateTime.UtcNow;
        }
        public List<MoveItem> MoveItems { get; set; }
        public List<MoveHistory> MoveHistories { get; set; }
        public List<Quote> Quotes { get; set; }
    }
}
