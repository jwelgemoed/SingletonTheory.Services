using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonTheory.Services.AuthServices.Interfaces
{
	public interface INameLabel
	{
		string Name { get; set; }
		string Label { get; set; }
	}
}
