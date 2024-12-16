using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dto
{
    public class GetAllProviderBySpecialisationIdDto
    {
        public int ProviderId { get; set; }
        public int SpecialisationId { get; set; }
    }
}
