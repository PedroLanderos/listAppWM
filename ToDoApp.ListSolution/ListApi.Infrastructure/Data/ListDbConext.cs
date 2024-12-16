using ListApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListApi.Infrastructure.Data
{
    public class ListDbConext : DbContext
    {

        public ListDbConext(DbContextOptions<ListDbConext> options) : base(options)
        {}

        public DbSet<ListEntity> Lists { get; set; }
    }
}
