using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configuration
{
    internal class VwCitiesConfiguration : IEntityTypeConfiguration<VwCitiy>
    {
        public void Configure(EntityTypeBuilder<VwCitiy> builder)
        {
             builder.ToView("VwCities");
        }
    }
    
    
}
