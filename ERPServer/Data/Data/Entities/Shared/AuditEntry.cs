using Data.Entities.UserManagement;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities.Shared
{

    [Table(nameof(AuditEntry))]
    public class AuditEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string EntityName { get; set; }
        public string ActionType { get; set; }
        public string Username { get; set; }
        public long UserProfileId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string EntityId { get; set; }
        public Dictionary<string, object> OldData { get; set; }
        public Dictionary<string, object> NewData { get; set; }

        [NotMapped]
        // TempProperties are used for properties that are only generated on save, e.g. ID's
        public List<PropertyEntry> TempProperties { get; set; }
    }
}
