using System;

namespace Interprit_Exam.DTO.UserList
{
    public class UpdateUser
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public long Id { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
