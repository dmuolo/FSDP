using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FSDP.DATA.EF/*.Metadata*/
{
    #region Application Metadata

    public class ApplicationMetadata
    {
        [Required(ErrorMessage = "* Day/time applied is required *")]
        [Display(Name = "Applied")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        public System.DateTime ApplicationDate { get; set; }

        [Display(Name = "Notes")]
        [StringLength(2000, ErrorMessage = "* Notes cannot exceed 2000 characters *")]
        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "None")]
        public string ManagerNotes { get; set; }

        [Required(ErrorMessage = "* Resume is required for an application *")]
        [Display(Name = "Resume")]
        [StringLength(75, ErrorMessage = "* Resume cannot exceed 75 characters *")]
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
        [Required(ErrorMessage = "* Status is required *")]
        [Display(Name = "Status")]
        [StringLength(50, ErrorMessage = "* Status cannot exceed 50 characters *")]
        public string StatusName { get; set; }

        [Display(Name = "Description")]
        [StringLength(250, ErrorMessage = "* Status cannot exceed 250 characters *")]
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
        [Required(ErrorMessage = "* City is required *")]
        [StringLength(50, ErrorMessage = "* City cannot exceed 50 characters *")]
        public string City { get; set; }

        [Required(ErrorMessage = "* State is required *")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = " State cannot exceed 50 characters *")]
        public string State { get; set; }

        [Display(Name = "Manager ID")]
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
        [Display(Name = "Position ID")]
        public int PositionId { get; set; }

        [Display(Name = "Location ID")]
        public int LocationId { get; set; }
    }

    [MetadataType(typeof(OpenPositionMetadata))]
    public partial class OpenPosition
    {

    }

    #endregion

    #region Position Metadata

    public class PositionMetadata
    {
        [Required(ErrorMessage = "* Position title is required *")]
        [StringLength(50, ErrorMessage = "* Position title cannot exceed 50 characters *")]
        public string Title { get; set; }

        [UIHint("MultilineText")]
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
        [Required(ErrorMessage = "*First Name is required *")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "*First Name cannot exceed 50 characters *")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Last Name is required *")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "*Last Name cannot exceed 50 characters *")]
        public string LastName { get; set; }

        //Validation done manually inside the controller
        public string Image { get; set; }

        [Display(Name = "Instrument 1")]
        [DisplayFormat(NullDisplayText = "[Not Given]")]
        [StringLength(50, ErrorMessage = "* Instrument 1 cannot exceed 50 characters *")]
        public string Instrument1 { get; set; }

        [Display(Name = "Instrument 2")]
        [DisplayFormat(NullDisplayText = "[Not Given]")]
        [StringLength(50, ErrorMessage = "* Instrument 2 cannot exceed 50 characters *")]
        public string Instrument2 { get; set; }

        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "[None Given]")]
        [StringLength(500, ErrorMessage = "* Related skills cannot exceed 500 characters *")]
        public string RelatedSkills { get; set; }

        [Display(Name = "Upload Resume")]
        [DisplayFormat(NullDisplayText = "[No Resume on File]")]
        [StringLength(75, ErrorMessage = "* Resume file name cannot exceed 75 characters *")]
        public string ResumeFilename { get; set; }
    }

    [MetadataType(typeof(UserDetailMetadata))]
    public partial class UserDetail
    {
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }

    #endregion
}
