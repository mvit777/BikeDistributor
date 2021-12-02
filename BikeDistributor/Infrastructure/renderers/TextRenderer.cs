using BikeDistributor.Domain.Models;
using BikeDistributor.renderers;
using MV.Framework.interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BikeDistributor.renderers
{
    public class TextRenderer :BaseRenderer, ITemplateRenderer
    {
        public TextRenderer(Order order) : base(order)
        {
        }

        public string RenderTemplate(string outputFile = null)
        {
            var totalAmount = 0d;
            var result = new StringBuilder($"Order Receipt for {_order.Company}{Environment.NewLine}");
            foreach (var line in _order.Lines)
            {
                Dictionary<string, object> DD = _order.CalculateLineTotal(line);
                var thisAmount = (double)DD["thisLineAmount"];
                var thisDiscount = (double)DD["discountPerc"];
                
                if (line.Bike.isStandard == false)
                {
                    //we want the last line, just before sub-total to show bikevariant price without options
                    var bv = (BikeVariant)line.Bike;
                    double totalBasePrice = bv.GetBasePrice() * line.Quantity;
                    result.AppendLine($"\t{line.Quantity} x {line.Bike.Brand} {line.Bike.Model} = {totalBasePrice.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}");
                    if(thisDiscount > 0)
                    {
                        result.AppendLine($"discount:\t{((bv.GetBasePrice() * line.Quantity) - (bv.GetBasePrice() * line.Quantity * thisDiscount)).ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])} ");
                        result.AppendLine($"line sub-total: {thisAmount.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}");
                    }                 
                    
                    //let's show the options
                    var options = bv.GetOptions();
                    foreach (BikeOption o in options)
                    {
                        var totalCurrentOption = o.Price * line.Quantity;
                        result.AppendLine($"\t{line.Quantity} x {o.Description} = {totalCurrentOption.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}");
                        thisAmount += totalCurrentOption;//if we would have discount on options they should be added here
                    }
                    result.AppendLine($"line total: {thisAmount.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}");
                }
                else
                {
                    //we just show the line as it is with total price for the Bike
                    result.AppendLine($"\t{line.Quantity} x {line.Bike.Brand} {line.Bike.Model} = {(line.Bike.Price * line.Quantity).ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}");
                    if(thisDiscount > 0)
                    {
                        result.AppendLine($"discount:\t{((line.Bike.Price * line.Quantity) - (line.Bike.Price * line.Quantity * thisDiscount)).ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])} ");
                        result.AppendLine($"line total: {thisAmount.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}");
                    }
                    
                    
                    //result.AppendLine($"discount:\t{discountPerc} is standard {line.Bike.isStandard}");
                }

                totalAmount += thisAmount;      
                
            }
            result.AppendLine($"Sub-Total: {totalAmount.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}");
            //result.AppendLine($"Discount: {}");
            var tax = totalAmount * (double)_order.OrderAspects["TaxRate"];
            var totalAfterTax = totalAmount + tax;
            result.AppendLine($"Tax: {tax.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}");
            result.Append($"Total: {totalAfterTax.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}");

            return result.ToString();
        }
    }
}
