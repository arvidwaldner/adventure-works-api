﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Common.Domain.Products
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProductNumber { get; set; } = string.Empty;
        public bool MakeFlag { get; set; }
        public bool FinishedGoodsFlag { get; set; }
        public string? Color { get; set; } = string.Empty;
        public short SafetyStockLevel { get; set; }
        public short ReorderPoint { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string? Size { get; set; } = string.Empty;
        public string? SizeUnitMeasureCode { get; set; } = string.Empty;
        public string? WeightUnitMeasureCode { get; set; } = string.Empty;
        public decimal? Weight { get; set; }
        public int DaysToManufacture { get; set; }
        public string? ProductLine { get; set; } = string.Empty;
        public string? Class { get; set; } = string.Empty;
        public string? Style { get; set; } = string.Empty;
        public int? ProductSubcategoryId { get; set; }
        public int? ProductModelId { get; set; }
        public DateTime SellStartDate { get; set; }
        public DateTime? SellEndDate { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
