﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DNATestServiceManager.Repositories.AnhTHQ.Models;

public partial class ServicePriceListAnhThq
{
    public int ServicePriceListAnhThqid { get; set; }

    public int ServiceAnhThqid { get; set; }

    public decimal? BasePrice { get; set; }

    public decimal? AdditionalFee { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual ServiceAnhThq ServiceAnhThq { get; set; }
}