using Dominio.Interface;
using Dominio.Servicos.Interfaces;
using Entidades;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class CalendarService : ICalendarService
    {
        private ICalendar _calendar;
        private readonly IMemoryCache _cache;
        public CalendarService(ICalendar calendar, IMemoryCache cache)
        {
            _calendar = calendar;
            _cache = cache;
        }
        public async Task<CalendarModel> BuscarCalendario()
        {
            if (_cache.TryGetValue("calendar", out CalendarModel calendar))
            {
                if (calendar == null) {
                    return null;
                }

                return  calendar; // 🔥 veio da memória
            }
            
            calendar = await _calendar.BuscarCalendar();

            var expiracao = DateTime.UtcNow.Date.AddDays(1);

            _cache.Set("calendar", calendar, expiracao);
            
            return calendar;
            }
    }
}
