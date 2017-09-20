using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Models
{
	class SubjectMetaData
	{
		static char separator = ' ';

		public string Title { get; private set; }
		public string Id { get; private set; }
		public DateTime Date { get; private set; }
		public string Guid { get; private set; }
		public string MacroCompany { get; private set; }
		public string Company { get; private set; }
		public ulong Agency { get; private set; }
		public uint? SubAgency { get; private set; }
		public string AgencyName { get; private set; }

		public SubjectMetaData(string formattedSubject) {

			// eg Request 2017/795785 - 18/09/2017 [fe5755eb-a7a3-4801-b26b-4031be4094ca] ALLIANZ RAS 010413000000 RAVENNA
			Queue<string> tokenized = new Queue<string>(formattedSubject.Substring(formattedSubject.IndexOf("Request")).Split(separator));
			int tokenNumber = 0;

			while (tokenized.Any()) {
				string token = tokenized.Dequeue().Trim();
				if (token.ToLowerInvariant().Equals("request")) {
					continue;
				}

				if (token.Equals("-"))
					continue;
				
				switch (tokenNumber) {
					case 0:
						Id = token;
						break;
					case 1:
						DateTime _Date;
						if (DateTime.TryParse(token, out _Date))
							Date = _Date;
						else
							Date = DateTime.Now.Date;
						break;
					case 2:
						Guid = token;
						break;
					case 3:
						MacroCompany = token;
						break;
					case 4:
						Company = token;
						if (tokenized.Peek().ToLowerInvariant().Equals("SUB"))
							Company += " " + tokenized.Dequeue().Trim();
						
						break;
					case 5:
						ParseAgency(token.PadRight(12, '0'));
						break;
					case 6:
						AgencyName = token;
						while (tokenized.Any()) 
							AgencyName += " " + tokenized.Dequeue();

						break;
				}
								
				tokenNumber++;
			}
			
			Title = "Request " + Id + " - " + Date.ToShortDateString();
			
		}

		/// <summary>
		/// ALLIANZ RAS			010413000000
		///	ALLIANZ MILANO		080002122000
		///	ALLIANZ SASA		070000915000 
		///	ALLIANZ SUBALPINA	020005660000
		///	ALLIANZ LLOYD		000485000000
		///	ALLIANZ LLOYD SUB	000777003000
		/// </summary>
		private void ParseAgency(string token) {
			string tokenCompany = token.Substring(0, 2);
			string tokenAgency	= string.Empty;

			int codAgeStart = 2;
			int codAgeLength = tokenCompany.Equals("00") ? 4 : 7;

			ulong _Agency;
			if( ulong.TryParse(token.Substring(codAgeStart, codAgeLength), out _Agency ))
				Agency = _Agency;

			uint _SubAge;
			if (uint.TryParse(token.Substring(codAgeStart + codAgeLength), out _SubAge))
				SubAgency = _SubAge;

		}

	}
}
