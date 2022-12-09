using System;
using System.Collections.Generic;

#nullable disable

namespace ParsKyanCrm.Domain.Entities
{
    public partial class AboutUs
    {
        public int AboutUsId { get; set; }
        public string CompanyName { get; set; }
        public string Subject { get; set; }
        public string AboutUscontent { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Tel3 { get; set; }
        public string Tel4 { get; set; }
        public string Tel5 { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string FaxNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Moto1 { get; set; }
        public string Moto2 { get; set; }
        public string Moto3 { get; set; }
        public string Moto4 { get; set; }
        public string Moto5 { get; set; }
        public string Instagram { get; set; }
        public string Whatsapp { get; set; }
        public string Facebook { get; set; }
        public string Telegram { get; set; }
        public string VisionAndMission { get; set; }
        public string OrganazationChart { get; set; }
        public int? Userid { get; set; }
        public DateTime? DateOfSaveorEdit { get; set; }

        public virtual Users User { get; set; }
    }
}
