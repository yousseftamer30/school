using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DrivingSchoolApi.Features.CallCenterReservation
{
    public class SchoolDayWorkingHourDto
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; } = "";
        public int DayOfWeek { get; set; }
        public string DayName { get; set; } = "";
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsWorkingDay { get; set; }
    }
    public record GetSchoolWorkingHourByDayQuery(int SchoolId, int DayOfWeek)
        : IRequest<SchoolDayWorkingHourDto>;

    public class GetSchoolWorkingHourByDayHandler
    : IRequestHandler<GetSchoolWorkingHourByDayQuery, SchoolDayWorkingHourDto>
    {
        private readonly DrivingSchoolDbContext _context;

        public GetSchoolWorkingHourByDayHandler(DrivingSchoolDbContext context)
        {
            _context = context;
        }

        public async Task<SchoolDayWorkingHourDto> Handle(GetSchoolWorkingHourByDayQuery request, CancellationToken ct)
        {
            if (request.DayOfWeek < 0 || request.DayOfWeek > 6)
                throw new Exception("Invalid DayOfWeek value. Must be between 0 and 6.");

            var data = await _context.TbSchoolOperatingHours
                .Include(o => o.School)
                .Where(o => o.SchoolId == request.SchoolId && o.DayOfWeek == request.DayOfWeek)
                .FirstOrDefaultAsync(ct);

            if (data == null)
                throw new Exception("No operating hours configured for this day.");

            return new SchoolDayWorkingHourDto
            {
                SchoolId = data.SchoolId,
                SchoolName = data.School.SchoolName,
                DayOfWeek = data.DayOfWeek,
                DayName = CultureInfo.GetCultureInfo("en-US")
                                     .DateTimeFormat.DayNames[data.DayOfWeek],
                StartTime = data.StartTime,
                EndTime = data.EndTime,
                IsWorkingDay = data.IsWorkingDay
            };
        }
    }
}
