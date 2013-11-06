using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Hours
{
	public class RoomHoursEntries:IReturn<List<RoomHoursEntry>>
	{
	}
}