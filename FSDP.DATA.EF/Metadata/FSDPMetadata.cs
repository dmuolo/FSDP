using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FSDP.DATA.EF.Metadata
{
    #region Application Metadata

    public class ApplicationMetadata
    {
        public System.DateTime ApplicationDate { get; set; }
        public string ManagerNotes { get; set; }
        public int ApplicationStatus { get; set; }
        public string ResumeFilename { get; set; }
    }

    [MetadataType(typeof(ApplicationMetadata))]
    public partial class Application
    {

    }

    #endregion

    #region Application Status Metadata

    public class ApplicationStatuMetadata
    {
        public string StatusName { get; set; }
        public string StatusDescription { get; set; }
    }

    [MetadataType(typeof(ApplicationStatuMetadata))]
    public partial class ApplicationStatu
    {

    }

    #endregion

    #region Location Metadata

    public class LocationMetadata
    {
        public string City { get; set; }
        public string State { get; set; }
        public string ManagerId { get; set; }
    }

    [MetadataType(typeof(LocationMetadata))]
    public partial class Lcation
    {

    }

    #endregion

    #region Open Position Metadata

    public class OpenPositionMetadata
    {

    }

    [MetadataType(typeof(OpenPositionMetadata))]
    public partial class OpenPosition
    {

    }

    #endregion

    #region Position Metadata

    public class PositionMetadata
    {
        public string Title { get; set; }
        public string JobDescription { get; set; }
    }

    [MetadataType(typeof(PositionMetadata))]
    public partial class Position
    {

    }

    #endregion

    #region User Details Metadata

    public class UserDetailMetadata
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Instrument1 { get; set; }
        public string Instrument2 { get; set; }
        public string RelatedSkills { get; set; }
        public string ResumeFilename { get; set; }
    }

    [MetadataType(typeof(UserDetailMetadata))]
    public partial class UserDetail
    {

    }

    #endregion
}
