using System;
using System.ComponentModel.DataAnnotations;

namespace CBSetoLib.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        public Uri ProfileUri { get; set; }
        public Uri ImageUri { get; set; }
        public Info Info { get; set; }
        public FrontProfile FrontProfile { get; set; }
        public RearProfile? RearProfile { get; set; }
        public Stats? Stats { get; set; }
        public Special Special { get; set; }
        public Position Position { get; set; }
    }

    public struct Position
    {
        public double Top { get; set; }
        public double Bottom { get; set; }
    }

    public struct Special
    {
        public string Caption { get; set; }
        public string Description { get; set; }
    }

    public struct Info
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }

    public struct Stats
    {
        [Range(0, 5)]
        public int Stamina { get; set; }
        [Range(0, 5)]
        public int Power { get; set; }
        [Range(0, 5)]
        public int Skill { get; set; }
        [Range(0, 5)]
        public int Tolerance { get; set; }
        [Range(0, 5)]
        public int Libido { get; set; }
    }

    public struct RearProfile
    {
        public int Waist { get; set; }
        public int Hip { get; set; }
    }

    public struct FrontProfile
    {
        public double ErectLength { get; set; }
        public double FlaccidLength { get; set; }
        public double LengthGrowth => Math.Round(ErectLength / FlaccidLength, 2);
        public double ErectGirth { get; set; }
        public double FlaccidGirth { get; set; }
        public double GirthGrowth => Math.Round(ErectGirth / FlaccidGirth, 2);
        public double ErectWidth { get; set; }
        public double FlaccidWidth { get; set; }
    }
}