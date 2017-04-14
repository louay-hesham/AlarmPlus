using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using AlarmPlus.Backend.DataObjects;
using AlarmPlus.Backend.Models;
using System.Security.Claims;
using AlarmPlus.Backend.Extensions;
using System.Net;

namespace AlarmPlus.Backend.Controllers
{
    public class AlarmController : TableController<Alarm>
    {
        public string UserId
        {
            get
            {
                var principal = this.User as ClaimsPrincipal;
                return principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Alarm>(context, Request, enableSoftDelete: true);
        }

        // GET tables/Alarm
        public IQueryable<Alarm> GetAllAlarm()
        {
            return Query();//.PerUserFilter(UserId); 
        }

        // GET tables/Alarm/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Alarm> GetAlarm(string id)
        {
            return new SingleResult<Alarm>(Lookup(id).Queryable.PerUserFilter(UserId));
        }

        // PATCH tables/Alarm/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Alarm> PatchAlarm(string id, Delta<Alarm> patch)
        {
            //ValidateOwner(id);
            return UpdateAsync(id, patch);
        }

        // POST tables/Alarm
        public async Task<IHttpActionResult> PostAlarm(Alarm item)
        {
            item.UserId = UserId;
            Alarm current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Alarm/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAlarm(string id)
        {
            //ValidateOwner(id);
            return DeleteAsync(id);
        }

        public void ValidateOwner(string id)
        {
            var result = Lookup(id).Queryable.PerUserFilter(UserId).FirstOrDefault<Alarm>();
            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
