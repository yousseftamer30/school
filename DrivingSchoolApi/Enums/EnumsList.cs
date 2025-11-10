namespace DrivingSchoolApi.Enums
{
    public class EnumsList
    {

        public enum EnumGenderType
        {
            Male = 1,
            Female = 2,
            All = 3
        }
        public enum EnumReligionType
        {
            All = 1,
            Muslim = 2,
            Christian = 3
        }

        public enum SessionType
        {
            Theory = 1,
            Practical = 2
        }

        public enum ReservationStatus
        {
            Pending = 1,
            Active = 2,
            Completed = 3,
            Cancelled = 4
        }

        public enum AttendanceStatus
        {
            Present = 1,
            Absent = 2,
            Excused = 3
        }

    }
}
