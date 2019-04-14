using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Lesson_Blockchain
{
    class BlockchainContext : DbContext
    {
        public BlockchainContext()
                        : base("MyConStr")
        { }

        public DbSet<Block> Blocks { get; set; }
    }
}




