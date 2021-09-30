using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Domain.Models
{
    public sealed class BikeVariant : Bike
    {
        private List<BikeOption> _SelectedOptions = new List<BikeOption>();
        public override int BasePrice { get; set; } = 0;
        private int _price = 0;
        private bool _priceNeedsUpdate = true; //a defensive variable to avoid (unlikely but possibile) multiple price updates when no option was added
        //we don't want Price to be set outside of SetTotalPrice call
        public override int Price
        {
            get
            {
                if (_priceNeedsUpdate)
                {
                    this.RecalculatePrice();
                }
                return _price;
            }
           
        }
        public override bool isStandard => false;

        public List<BikeOption> SelectedOptions
        {
            get
            {
                return _SelectedOptions;
            }
            set 
            {
                _SelectedOptions = value;
                this._priceNeedsUpdate = true;
                this.RecalculatePrice();
            }
        }


        /// <summary>
        /// Please use BikeFactory factory rather than 
        /// directly instantiate a bike variant
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="price"></param>
        public BikeVariant(string brand, string model, int price, List<BikeOption> options = null) : base(brand, model, price)
        {
            this.BasePrice = price;//keep in memory variant base price without options
            if (options != null)
            {
                this._SelectedOptions = options;
            }
            this.RecalculatePrice();//but make it available for the outside and options price if needed
        }


        /// <summary>
        /// should an option price change for some reason we can still use this method to update the List.
        /// Former option with the same name is removed and the new option takes its place
        /// </summary>
        public void SetTotalPrice(BikeOption option)
        {
            this._priceNeedsUpdate = true;
            var existingOption = _SelectedOptions.FirstOrDefault(x => x.Name == option.Name);
            if (existingOption == null)
            {
                _SelectedOptions.Add(option);
            }
            else
            {
                //actually we want to auto remove the existing option and add the new option
                //so we make sure we have no duplicate option
                _SelectedOptions.Remove(existingOption);
                _SelectedOptions.Add(option);
            }

            _price = RecalculatePrice();//probably to be removed as priceNeedsUpdate is already flagged
        }
        public void RemoveOption(string name)
        {
            BikeOption removeAbleOpt = _SelectedOptions.FirstOrDefault(x => x.Name == name);
            if (removeAbleOpt != null)
            {
                this._priceNeedsUpdate = true;
                this._SelectedOptions.Remove(removeAbleOpt);
                //this.RecalculatePrice();
            }
        }
        public void ClearOptions()
        {
            this._SelectedOptions = new List<BikeOption>();
            this._priceNeedsUpdate = true;
            //this.RecalculatePrice();
        }

        public BikeOption GetOption(string name)
        {
            var existingOption = _SelectedOptions.FirstOrDefault(x => x.Name == name);

            return existingOption; //may return null        
        }

        public List<BikeOption> GetOptions()
        {
            return this._SelectedOptions;
        }
        /// <summary>
        /// Returns the price without the options total (maybe useful for receipts)
        /// </summary>
        /// <returns></returns>
        public int GetBasePrice()
        {
            return this.BasePrice;
        }
        public int GetOptionsTotalPrice()
        {
            int optionPrice = 0;
            foreach (BikeOption o in _SelectedOptions)
            {
                optionPrice += o.Price;
            }
            return optionPrice;
        }
        /// <summary>
        /// this is where most of the shit happens
        /// we update total price taking into account options total price
        /// </summary>
        /// <returns>int total price for (bike)variant</returns>
        private int RecalculatePrice()
        {
            //in the event of a double unwanted call
            if (this._priceNeedsUpdate == false)
            {
                return _price;
            }
            //we reset back to the original price of the bike with no options 
            _price = this.BasePrice;
            //we add options price because some options may have been added or its price modified
            _price += this.GetOptionsTotalPrice();
            _priceNeedsUpdate = false;

            return _price;
        }
    }
}
