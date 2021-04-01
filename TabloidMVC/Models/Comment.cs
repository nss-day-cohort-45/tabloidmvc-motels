using System;

namespace TabloidMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserProfileId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public struct DateTime : IComparable, IFormattable,
    IConvertible, ISerializable, IComparable<DateTime>, IEquatable<DateTime>
    }
}
