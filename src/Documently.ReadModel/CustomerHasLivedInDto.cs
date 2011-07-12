using System.Collections.Generic;

namespace Documently.ReadModel
{
	public class CustomerHasLivedInDto : Dto
	{
		public CustomerHasLivedInDto()
		{
			Cities = new List<string>();
		}

		public IList<string> Cities { get; set; }

		public void AddCity(string c)
		{
			Cities.Add(c);
		}
	}
}