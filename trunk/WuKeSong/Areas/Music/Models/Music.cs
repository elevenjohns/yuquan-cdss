using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WuKeSong.Areas.Music.Models
{
    /// <summary>
    /// The Music-Genre association is not perfect.
    /// The auto created db tables don't support many-to-many associtaion.
    /// </summary>
    public class Music
    {
        [Key]
        public int ID{ get; set; }
        [Required] 
        // [Range(1,100)]
        // [StringLength(100)] 
        // [DataType(DataType.Currency/Date)]
        // [DisplayFormat(DataFormatString="{0:d}"]
        public String Title { get; set; }
        public String Author { get; set; }
        public String Genre { get; set; } //Pop, New Age, Rock
        public String Comment { get; set; }
        public String SheetMusic { get; set; }
        public List<Genre> Genres { get; set; }

        public Music() 
        {
            Genres=new List<Genre>();
        }
    }

    public class Genre
    {
        [Key]
        public int ID{ get; set; }
        [Required]
        public String Title { get; set; }
        public String Description { get; set; }
    }

    public class MusicDBContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Music> Music { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}