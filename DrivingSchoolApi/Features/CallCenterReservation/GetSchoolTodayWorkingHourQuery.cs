using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DrivingSchoolApi.Features.CallCenterReservation
{

    public class SchoolTodayWorkingHourDto
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; } = "";
        public int DayOfWeek { get; set; }
        public string DayName { get; set; } = "";
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsWorkingDay { get; set; }
    }
    public record GetSchoolTodayWorkingHourQuery(int SchoolId)
        : IRequest<SchoolTodayWorkingHourDto>;
    public class GetSchoolTodayWorkingHourHandler
        : IRequestHandler<GetSchoolTodayWorkingHourQuery, SchoolTodayWorkingHourDto>
    {
        private readonly DrivingSchoolDbContext _context;

        public GetSchoolTodayWorkingHourHandler(DrivingSchoolDbContext context)
        {
            _context = context;
        }

        public async Task<SchoolTodayWorkingHourDto> Handle(GetSchoolTodayWorkingHourQuery request, CancellationToken ct)
        {
            // اليوم الحالي (0=Sunday)
            int today = (int)DateTime.Now.DayOfWeek;

            var data = await _context.TbSchoolOperatingHours
                .Include(o => o.School)
                .Where(o => o.SchoolId == request.SchoolId && o.DayOfWeek == today)
                .FirstOrDefaultAsync(ct);

            if (data == null)
                throw new Exception("No operating hours configured for today.");

            return new SchoolTodayWorkingHourDto
            {
                SchoolId = data.SchoolId,
                SchoolName = data.School.SchoolName,
                DayOfWeek = data.DayOfWeek,
                DayName = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.DayNames[data.DayOfWeek],
                StartTime = data.StartTime,
                EndTime = data.EndTime,
                IsWorkingDay = data.IsWorkingDay
            };
        }
    }

}


