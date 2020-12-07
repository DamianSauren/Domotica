using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domotica.Data
{
    public class Device
    {
        [Key]
        [Column(TypeName = "nvarchar(100)")]
        public string Id { get; set; }

        [Column(TypeName = "nvarchar(100)")] 
        public string DeviceName { get; set; }

        [Column(TypeName = "nvarchar(100)")] 
        public string DeviceCategory { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string UserId { get; set; }
    }
}
