using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DividendApi.Models
{
    [DataContract]
    public class Company
    {
        [DataMember(Name = "symbol")]
        public string Symbol { get; set; }

        [DataMember(Name = "companyName")]
        public string CompanyName { get; set; }

        //[DataMember(Name = "exchange")]
        //public string Exchange { get; set; }

        //[DataMember(Name = "industry")]
        //public string Industry { get; set; }

        //[DataMember(Name = "website")]
        //public string Website { get; set; }

        //[DataMember(Name = "description")]
        //public string Description { get; set; }

        //[DataMember(Name = "CEO")]
        //public string Ceo { get; set; }

        //[DataMember(Name = "securityName")]
        //public string SecurityName { get; set; }

        //[DataMember(Name = "issueType")]
        //public string IssueType { get; set; }

        [DataMember(Name = "sector")]
        public string Sector { get; set; }

        //[DataMember(Name = "employees")]
        //public int Employees { get; set; }

        //[DataMember(Name = "tags")]
        //public string[] Tags { get; set; }
    }

}
