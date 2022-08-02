using System;
using System.ComponentModel.DataAnnotations;

namespace FluentScaffold.Tests.ApplicationUnderTest.Data;

public class Item
{
    [Key]
    public Guid Id { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }

    public int? AgeRestriction { get; set; }
}