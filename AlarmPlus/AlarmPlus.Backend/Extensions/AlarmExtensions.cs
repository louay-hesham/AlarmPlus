using AlarmPlus.Backend.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlarmPlus.Backend.Extensions
{
    public static class AlarmExtensions
    {
        public static IQueryable<Alarm> PerUserFilter(this IQueryable<Alarm> query, string userid)
        {
            return query.Where(item => item.UserId.Equals(userid));
        }
    }
}