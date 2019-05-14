using System.ComponentModel.DataAnnotations;

namespace OM.Entities.EntityClass
{
    public class Configration
    {
        [Key]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
