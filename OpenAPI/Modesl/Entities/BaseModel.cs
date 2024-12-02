using OpenAPI.Enums;

namespace OpenAPI.Modesl.Entities
{
    public class BaseModel
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public int Status { get; set; } = (int)CommonEnum.ActiveStatus.Active;
    }
}
