using IKnowTechnology.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKnowTechnology.DAL.EntityConfigurations
{
    public class TaskListConfig : IEntityTypeConfiguration<CORE.Entities.TaskList>
    {
        public void Configure(EntityTypeBuilder<CORE.Entities.TaskList> builder)
        {
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.WorkTime).IsRequired();
        }
    }
}
