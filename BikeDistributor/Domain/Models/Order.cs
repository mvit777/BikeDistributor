using BikeDistributor.Infrastructure.factories;
using MV.Framework.interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Domain.Models
{
    public class Order : IRendable
    {
        protected double _TaxRate = .0725d;//maybe retrieve from config
        protected CultureInfo _CurrentCulture = null;
        protected List<Discount> _availableDiscounts = new List<Discount>();
        protected string _ReceiptFormat { get; set; }

        public virtual List<Order> Orders { get; set; }


        public string Company { get; }

        public IList<OrderLine> Lines { get; }

        public int OrderId { get; }

        public string ReceiptFormat
        {
            get
            {
                return _ReceiptFormat;
            }
        }
        public Dictionary<string, object> OrderAspects { get; } = new Dictionary<string, object>();


        public Order(int orderId, string company, string format = "Text", string culture = "en_us", double taxRate = -1)
        {
            Company = company;
            OrderId = orderId;
            Lines = new List<OrderLine>();
            _CurrentCulture = CultureInfo.GetCultureInfo(culture);
            _ReceiptFormat = format;
            if (taxRate >= 0)
            {
                _TaxRate = taxRate;
            }
            OrderAspects.Add("TaxRate", _TaxRate);
            OrderAspects.Add("CurrentCulture", _CurrentCulture);
        }

        public void AddLine(OrderLine line)
        {
            Lines.Add(line);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="availableDiscounts"></param>
        /// <returns></returns>
        public string Receipt(List<Discount> availableDiscounts)
        {
            _availableDiscounts = availableDiscounts;

            ITemplateRenderer renderer = TemplateFactory.Create(this, _ReceiptFormat);

            return renderer.RenderTemplate();
        }
        public List<Discount> GetDiscounts()
        {
            return _availableDiscounts;
        }
        public CultureInfo GetCulture(string culture)
        {
            return CultureInfo.GetCultureInfo(culture);
        }

        /// <summary>
        /// Search for a matching discount within the passed list
        /// </summary>
        /// <param name="unitprice"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Dictionary<string, object> CheckForAvailableDiscount(int unitprice, int quantity)
        {
            var result = new Dictionary<string, object>();
            double thisLineAmount = 0d;
            double discountPerc = .0d;

            Discount discount = (Discount)_availableDiscounts.FirstOrDefault(
                                    x => unitprice >= x.UnitPriceThreeShold && quantity >= x.QuantityThreeShold);
            if (discount != null)
            {
                thisLineAmount = quantity * unitprice * discount.Percentage;
                discountPerc = discount.Percentage;
            }
            else
            {
                thisLineAmount = quantity * unitprice;
            }
            result.Add("thisLineAmount", thisLineAmount);
            result.Add("discountPerc", discountPerc);

            return result;
        }


        public Dictionary<string, object> CalculateLineTotal(OrderLine line)
        {

            int unitprice = line.Bike.Price;
            if (line.Bike.isStandard == false)
            {
                var bv = (BikeVariant)line.Bike;
                unitprice = bv.GetBasePrice();
            }
            int quantity = line.Quantity;
            Dictionary<string, object> DiscountDictionary = this.CheckForAvailableDiscount(unitprice, quantity);

            return DiscountDictionary;
        }
    }
}
