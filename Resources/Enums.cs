using System.ComponentModel.DataAnnotations;

namespace FileRacks.Resources
{
    public enum AppMenu
    {
        [Display(Name = "Profile")]
        Profile = 0,
        [Display(Name = "Zone Selection")]
        ZoneSelection,
        [Display(Name = "Scan")]
        Scan,
        [Display(Name = "Batch Manager")]
        BatchManager,
        [Display(Name = "Batch Details")]
        BatchDetails,
        [Display(Name = "Log Out")]
        LogOut
    }
}
