using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace YouthVoice.Data
{
    public class Image
    {
        public int ImageId { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }
        public byte[] ImageData { get; set; }
    }
}
