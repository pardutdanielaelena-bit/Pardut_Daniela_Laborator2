using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pardut_Daniela_Laborator2.Models;

namespace Pardut_Daniela_Laborator2.Data
{
    public class Pardut_Daniela_Laborator2Context : DbContext
    {
        public Pardut_Daniela_Laborator2Context (DbContextOptions<Pardut_Daniela_Laborator2Context> options)
            : base(options)
        {
        }

        public DbSet<Pardut_Daniela_Laborator2.Models.Book> Book { get; set; } = default!;
        public DbSet<Pardut_Daniela_Laborator2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Pardut_Daniela_Laborator2.Models.Author> Author { get; set; } = default!;

    }
}
