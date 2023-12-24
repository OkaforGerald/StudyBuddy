using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                    new IdentityRole
                    {
                        Id = "1eeb37db-b804-4aad-a378-bcf2ba93e82b",
                        Name = "User",
                        NormalizedName = "USER"
                    },

                    new IdentityRole
                    {
                        Id = "1bb823f0-2d92-47e8-be60-b2187df3555d",
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    }
                );
        }
    }
}
