using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _100ToMe.Helpers
{
    public class DateTimeBR
    {
        public static DateTime DataHoraAtual()
        {
            DateTime dhUTC = DateTime.UtcNow; //recupera a data hora atual com UTC
            TimeZoneInfo zonaBR = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"); //Brasilia
            DateTime dhBR = TimeZoneInfo.ConvertTimeFromUtc(dhUTC, zonaBR);
            return dhBR;
        }
    }
}
