using BikeDistributor.Domain;
using BikeDistributor.Infrastructure.core;
using BikeDistributor.Infrastructure.interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeDistributor.Domain.Models;

namespace BikeDistributor.Infrastructure.factories
{
    
        public class BikeFactory
        {
            //private string bikeConfig;
            private Bike bike;
            private BikeVariant variant;
            private bool isStandard = true;
            

            /// <summary>
            /// This class and this static constructor are the recommended way
            /// to create new Bike/BikeVariant. Please see ProductTests.json for JObject structure
            /// </summary>
            /// <param name="ibike">A JObject configuration for the desidered bike/variant</param>
            /// <returns></returns>
            public static BikeFactory Create(JObject ibike)
            {
                return new BikeFactory(ibike);
            }
            /// <summary>
            /// Please see ProductTests.json for JObject structure
            /// </summary>
            /// <param name="ibike">A JObject configuration for the desidered bike/variant</param>
            private BikeFactory(JObject ibike)
            {
                isStandard = (bool)ibike["isStandard"];
                string model = (string)ibike["Model"];
                string brand = (string)ibike["Brand"];
                int price = (int)ibike["Price"];
                this.bike = new Bike(brand, model, price);
                if (isStandard == false)
                {
                    List<BikeOption> options = null;
                    if (ibike.ContainsKey("options"))
                    {
                        options = JsonUtils.GetListFromJArrayOption(ibike["options"].ToString());
                    }

                    this.variant = new BikeVariant(brand, model, price, options);

                }
            }
            public BikeFactory RemoveOption(string name)
            {
                if (this.variant != null)
                {
                    this.variant.RemoveOption(name);
                }
                return this;
            }
            public BikeFactory ClearOptions()
            {
                if (this.variant != null)
                {
                    this.variant.ClearOptions();
                }
                return this;
            }
            /// <summary>
            /// Add an option to the list of selected options 
            /// for current BikeVariant
            /// </summary>
            /// <param name="option"></param>
            /// <returns></returns>
            public BikeFactory AddOption(IBikeOption option)
            {
                if (this.variant == null)
                {
                    //this.variant = (BikeVariant)bike; cannot cast but I could copy the values from this.bike and start the options
                    //it is not a tech a constraint but we like it like this for commercial reason. Tests show we have other aways to achieve new variants from standad bikes
                    throw new Exception("cannot add options to a standard bike or no bike variant was created");
                }
                this.variant.SetTotalPrice((BikeOption)option);

                return this;
            }

            public IBike GetBike()
            {
                if (isStandard)
                {
                    return this.bike;
                }

                return this.variant;
            }
        }
}
