
using BikeDistributor.Domain.core;

using MV.Framework.interfaces;
using MV.Framework.providers;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BikeDistributor.Domain.Models;


namespace BikeDistributor.renderers
{
    public class HtmlRenderer : BaseRenderer, ITemplateRenderer
    {
        
        public HtmlRenderer(Order order) : base(order)
        {
            
        }

        public string RenderTemplate(string outputFile = null)
        {
            var totalAmount = 0d;
            var result = new StringBuilder($"<html><body><h1>Order Receipt for {_order.Company}</h1>");
            if (_order.Lines.Count > 0)
            {
                result.Append("<ul>");
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
                        result.Append($"<li>{line.Quantity} x {line.Bike.Brand} {line.Bike.Model} = {totalBasePrice.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}</li>");
                        result.AppendLine($"<li>discount: {((bv.GetBasePrice() * line.Quantity) - (bv.GetBasePrice() * line.Quantity * thisDiscount)).ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}</li>");
                        result.AppendLine($"<li>line sub-total: {thisAmount.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}</li>");
                        //let's show the options
                        var options = bv.GetOptions();
                        foreach (BikeOption o in options)
                        {
                            var totalCurrentOption = o.Price * line.Quantity;
                            result.Append($"<li>{line.Quantity} x {o.Description} = {totalCurrentOption.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}</li>");
                        }
                    }
                    else
                    {
                        //we just show the line as it is with total price for the Bike
                        result.Append($"<li>{line.Quantity} x {line.Bike.Brand} {line.Bike.Model} = {thisAmount.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}</li>");
                        if (thisDiscount > 0)
                        {
                            result.AppendLine($"<li>{((line.Bike.Price * line.Quantity) - (line.Bike.Price * line.Quantity * thisDiscount)).ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}<li>");
                            result.AppendLine($"<li>{thisAmount.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}<li>");
                        }
                    }


                    totalAmount += thisAmount;

                }
                result.Append("</ul>");
            }
            result.Append($"<h3>Sub-Total: {totalAmount.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}</h3>");
            var tax = totalAmount * (double)_order.OrderAspects["TaxRate"];
            var totalAmountAfterTax = totalAmount + tax;
            result.Append($"<h3>Tax: {tax.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}</h3>");
            result.Append($"<h2>Total: {totalAmountAfterTax.ToString("C", (CultureInfo)_order.OrderAspects["CurrentCulture"])}</h2>");
            result.Append("</body></html>");

            return result.ToString().Trim();
        }
    }
}
